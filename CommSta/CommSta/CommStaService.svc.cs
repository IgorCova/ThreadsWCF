using System;
using VkNet;
using VkNet.Enums.Filters;
using VkNet.Model.RequestParams;
using VkNet.Enums;
using VkNet.Model;
using System.Threading;

namespace CommSta
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class CommStaService : IService
    {
        public WallFilter Owner { get; private set; }

        public void VKontakte_Sta(wsRequest req)
        {
            HubDataClassesDataContext dc = new HubDataClassesDataContext();
            long groupId = req.groupID;
            DateTime dateStart = DateTime.Now;

            DateTime dateFrom = DateTime.Today.AddDays(-1).Date;
            DateTime? dateTo = DateTime.Today.Date;

            ulong appId = 5391843; // указываем id приложения
            string email = "89164913669"; // email для авторизации
            string password = "PressNon798520"; // пароль
            Settings settings = Settings.Wall; // уровень доступа к данным

            VkApi api = new VkApi();
            api.Authorize(new ApiAuthParams
            {
                ApplicationId = appId,
                Login = email,
                Password = password,
                Settings = settings
            }); // авторизуемся

            var res = api.Stats.GetByGroup(groupId, dateFrom, dateTo);

            long views = res[0].Views;
            long visitors = res[0].Visitors;
            long reach = res[0].Reach ?? 0;
            long reach_subscribers = res[0].ReachSubscribers ?? 0;
            long subscribed = res[0].Subscribed ?? 0;
            long unsubscribed = res[0].Unsubscribed ?? 0;

            int members = 0;
            GroupsGetMembersParams prms = new GroupsGetMembersParams();
            prms.Count = 0;
            prms.GroupId = groupId.ToString();
            prms.Offset = 0;
            api.Groups.GetMembers(out members, prms);

            long likes = 0;
            long comments = 0;
            long reposts = 0;

            var respWall = api.Wall.Get(new WallGetParams
            {
                OwnerId = 0 - groupId,
                Offset = 0,
                Count = 100,
                Filter = Owner,
                Extended = false
            });

            var cnt = respWall.TotalCount;
            var countPost = (long)cnt;

            foreach (Post post in respWall.WallPosts)
            {
                likes += post.Likes.Count;
                comments += post.Comments.Count;
                reposts += post.Reposts.Count;
            };

            ulong offset = 100;

            int reqCount = 0;
            while (offset < cnt)
            {
                if ((reqCount % 3) == 0) // 3 запроса в секунду
                {
                    Thread.Sleep(1500);
                    reqCount = 0;
                }
                respWall = api.Wall.Get(new WallGetParams
                {
                    OwnerId = 0 - groupId,
                    Offset = offset,
                    Count = 100,
                    Filter = Owner,
                    Extended = false
                });
                reqCount++;

                foreach (Post post in respWall.WallPosts)
                {
                    likes += post.Likes.Count;
                    comments += post.Comments.Count;
                    reposts += post.Reposts.Count;
                };

                offset += 100;
            }

            dc.StaCommVK_Save(groupId, dateStart, views, visitors, reach, reach_subscribers, subscribed, unsubscribed, likes, comments, reposts, countPost, members);
        }
    }
}
