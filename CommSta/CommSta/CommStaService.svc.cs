using System;
using VkNet;
using VkNet.Enums.Filters;
using VkNet.Model.RequestParams;
using VkNet.Enums;
using VkNet.Model;
using VkNet.Exception;
using System.Threading;
using System.Collections.ObjectModel;
using System.Collections.Generic;

namespace CommSta
{
    public class CommStaService : IService
    {
        public WallFilter Owner { get; private set; }

        private WallGetObject api_Wall_Get(VkApi api, long groupId, ulong offset)
        {
            return api.Wall.Get(new WallGetParams
            {
                OwnerId = 0 - groupId,
                Offset = offset,
                Count = 100,
                Filter = Owner,
                Extended = false
            });
        }

        private int GetCountMembers(VkApi api, long groupId)
        {
            int members;
            GroupsGetMembersParams prms = new GroupsGetMembersParams();
            prms.Count = 0;
            prms.GroupId = groupId.ToString();
            prms.Offset = 0;

            api.Groups.GetMembers(out members, prms);
            return members;
        }

        private string GetPhotoLink(VkApi api, long groupId)
        {
            string linkTo = "";
            IEnumerable<string> groupIds = new string[] { groupId.ToString()};
            ReadOnlyCollection<Group> groups = api.Groups.GetById(groupIds, "", GroupsFields.Description);

            foreach (Group group in groups)
            {
                linkTo = group.Photo100.ToString();
            }
            return linkTo;
        }

        public void VKontakte_Sta(wsRequest req)
        {
            DateTime dateFrom = DateTime.Today.Date;

            wsRequestByDate exreq = new wsRequestByDate();
            exreq.dateFrom = DateTime.Now;
            exreq.groupID = req.groupID;

            VKontakte_Sta_ByDate(exreq);
        }

        public void VKontakte_Sta_ByDate(wsRequestByDate req)
        {
            long groupId = req.groupID;
            DateTime dateFrom = req.dateFrom;
            DateTime? dateTo = req.dateTo;

            HubDataClassesDataContext dc = new HubDataClassesDataContext();

            DateTime dateStart = DateTime.Now;

            long views = 0;
            long visitors = 0;
            long reach = 0;
            long reach_subscribers = 0;
            long subscribed = 0;
            long unsubscribed = 0;
            int members = 0;
            long likes = 0;
            long comments = 0;
            long reposts = 0;
            ulong offset = 0;
            string photoLink = "";

            ulong appId = 5391843; // указываем id приложения
            string email = "89164913669"; // email для авторизации
            string password = "PressNon798520"; // пароль
            Settings settings = Settings.All; // уровень доступа к данным

            VkApi api = new VkApi();
            try
            {
                api.Authorize(new ApiAuthParams
                {
                    ApplicationId = appId,
                    Login = email,
                    Password = password,
                    Settings = settings
                }); // авторизуемся
            }
            catch (Exception e)
            {
                string exInnerExceptionMessage = "";
                if (e.InnerException != null)
                {
                    exInnerExceptionMessage = e.InnerException.Message;
                }
                dc.Exception_Save("VKontakte_Sta_ByDate", "VkApi.Authorize", e.Message, exInnerExceptionMessage, e.HelpLink, e.HResult, e.Source, e.StackTrace);
                return;
            }

            ReadOnlyCollection<StatsPeriod> res;

            try
            {
                res = api.Stats.GetByGroup(groupId, dateFrom, dateTo);

                if (res.Count > 0)
                {
                    views = res[0].Views;
                    visitors = res[0].Visitors;
                    reach = res[0].Reach ?? 0;
                    reach_subscribers = res[0].ReachSubscribers ?? 0;
                    subscribed = res[0].Subscribed ?? 0;
                    unsubscribed = res[0].Unsubscribed ?? 0;
                }

                members = GetCountMembers(api, groupId);
                photoLink = GetPhotoLink(api, groupId);
                likes = 0;
                comments = 0;
                reposts = 0;
                offset = 0;

                WallGetObject respWall = api_Wall_Get(api, groupId, offset);

                ulong cnt = respWall.TotalCount;
                long countPost = (long)cnt;

                foreach (Post post in respWall.WallPosts)
                {
                    likes += post.Likes.Count;
                    comments += post.Comments.Count;
                    reposts += post.Reposts.Count;
                };

                offset = 100;

                int reqCount = 0;
                while (offset < cnt)
                {
                    if ((reqCount % 3) == 0) // 3 запроса в секунду
                    {
                        Thread.Sleep(1200);
                        reqCount = 0;
                    }

                    try
                    {
                        respWall = api_Wall_Get(api, groupId, offset);
                    }
                    catch (TooManyRequestsException)
                    {
                        Thread.Sleep(1200);
                        reqCount = 0;
                        respWall = api_Wall_Get(api, groupId, offset);
                    }

                    reqCount++;

                    foreach (Post post in respWall.WallPosts)
                    {
                        likes += post.Likes.Count;
                        comments += post.Comments.Count;
                        reposts += post.Reposts.Count;
                    };

                    offset += 100;
                }

                dc.StaCommVKDaily_Save(groupId, dateFrom, views, visitors, reach, reach_subscribers, subscribed, unsubscribed, likes, comments, reposts, countPost, members, photoLink);
            }
            catch (AccessDeniedException e)
            {
                string exInnerExceptionMessage = "";
                if (e.InnerException != null)
                {
                    exInnerExceptionMessage = e.InnerException.Message;
                }
                string exMessage = "";
                if (e as Exception != null)
                {
                    exMessage = e.Message;
                }

                dc.GroupAccess_Save(groupId, e.Message, exInnerExceptionMessage);
            }
            catch (Exception e)
            {
                string exInnerExceptionMessage = "";
                if (e.InnerException != null)
                {
                    exInnerExceptionMessage = e.InnerException.Message;
                }
                dc.Exception_Save("VKontakte_Sta_ByDate", "", e.Message, exInnerExceptionMessage, e.HelpLink, e.HResult, e.Source, e.StackTrace);
            }
        }
    }
}
