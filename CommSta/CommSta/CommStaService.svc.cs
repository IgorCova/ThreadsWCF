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
        private int getDayOfWeek()
        {
            int dow = (int)DateTime.Today.DayOfWeek;
            switch (dow)
            {
                case 0: dow = 7; break;
                case 1: dow = 0; break;
                case 2: dow = 1; break;
                case 3: dow = 2; break;
                case 4: dow = 3; break;
                case 5: dow = 4; break;
                case 6: dow = 5; break;
                case 7: dow = 6; break;
            };

            return dow;
        }

        #region WallFilter
        public WallFilter Owner { get; private set; }
        #endregion

        #region VKontakte_Sta
        public void VKontakte_Sta()
        {
            if (DateTime.Now.Hour == 0)
            {
                VKontakte_Sta_CloseDay();
            }

            wsRequestByDate exreq = new wsRequestByDate();

            exreq.dateFrom = DateTime.Today.Date;
            exreq.dateTo = DateTime.Now;
            exreq.dateType = DateType.day;

            var lst = GetVKGroups(false);

            foreach (var gr in lst.dir)
            {
                exreq.groupID = gr.groupID;
                VKontakte_Sta_ByDate_Parallels(exreq);
                Thread.Sleep(1500);
            }

            //
            wsRequestByDate exreqW = new wsRequestByDate();

            exreqW.dateFrom = DateTime.Today.Date.AddDays(-getDayOfWeek());
            exreqW.dateTo = DateTime.Now;
            exreqW.dateType = DateType.week;

            foreach (var gr in lst.dir)
            {
                exreqW.groupID = gr.groupID;
                VKontakte_Sta_ByDate_Parallels(exreqW);
                Thread.Sleep(1500);
            }
        }

        public void VKontakte_Sta_CloseDay()
        {
            wsRequestByDate exreq = new wsRequestByDate();

            exreq.dateFrom = DateTime.Today.AddDays(-1).Date;
            exreq.dateTo = DateTime.Today.Date.AddMilliseconds(-1);
            exreq.dateType = DateType.day;

            var lst = GetVKGroups(false);

            foreach (var gr in lst.dir)
            {
                exreq.groupID = gr.groupID;
                VKontakte_Sta_ByDate_Parallels(exreq);
                Thread.Sleep(1500);
            }

            if (getDayOfWeek() == 0)
            {
                VKontakte_Sta_CloseWeek();
            }
        }

        public void VKontakte_Sta_CloseWeek()
        {
            wsRequestByDate exreq = new wsRequestByDate();

            exreq.dateFrom = DateTime.Today.Date.AddDays(-7);
            exreq.dateTo = DateTime.Today.Date.AddMilliseconds(-1);
            exreq.dateType = DateType.week;

            var lst = GetVKGroups(false);

            foreach (var gr in lst.dir)
            {
                exreq.groupID = gr.groupID;
                VKontakte_Sta_ByDate_Parallels(exreq);
                Thread.Sleep(1500);
            }
        }

        public void VKontakte_Sta_ForNew()
        {
            wsRequestByDate exreq = new wsRequestByDate();
            exreq.dateType = DateType.day;

            // Для новых сообществ за вчера и позавчера считаем стаитистику
            var newlst = GetVKGroups(true);
            if (newlst.dir.Count > 0)
            {
                exreq.dateFrom = DateTime.Today.Date;
                exreq.dateTo = DateTime.Now;

                foreach (var gr in newlst.dir)
                {
                    if (gr.groupID == 0)
                    {
                        gr.groupID = SetCommVK(gr.link);
                        Thread.Sleep(1000);
                    }
                }

                foreach (var gr in newlst.dir)
                {
                    exreq.groupID = gr.groupID;
                    VKontakte_Sta_ByDate_Parallels(exreq);
                    Thread.Sleep(1500);
                }

                exreq.dateFrom = DateTime.Today.Date.AddDays(-1);
                exreq.dateTo = DateTime.Today.Date.AddMilliseconds(-1);

                foreach (var gr in newlst.dir)
                {
                    exreq.groupID = gr.groupID;
                    VKontakte_Sta_ByDate_Parallels(exreq);
                    Thread.Sleep(1500);
                }

                exreq.dateFrom = DateTime.Today.Date.AddDays(-2);
                exreq.dateTo = DateTime.Today.Date.AddDays(-1).AddMilliseconds(-1);

                foreach (var gr in newlst.dir)
                {
                    exreq.groupID = gr.groupID;
                    VKontakte_Sta_ByDate_Parallels(exreq);
                    Thread.Sleep(1500);
                }

                // weekly report
                wsRequestByDate exreqW = new wsRequestByDate();

                int dw = getDayOfWeek();
                exreqW.dateType = DateType.week;

                exreqW.dateFrom = DateTime.Today.Date.AddDays(-dw + 1).AddDays(-14);
                exreqW.dateTo = DateTime.Today.Date.AddDays(-dw + 1).AddDays(-7).AddMilliseconds(-1);

                foreach (var gr in newlst.dir)
                {
                    exreqW.groupID = gr.groupID;
                    VKontakte_Sta_ByDate_Parallels(exreqW);
                    Thread.Sleep(1500);
                }

                exreqW.dateFrom = DateTime.Today.Date.AddDays(-dw + 1).AddDays(-7);
                exreqW.dateTo = DateTime.Today.Date.AddDays(-dw + 1).AddMilliseconds(-1);

                foreach (var gr in newlst.dir)
                {
                    exreqW.groupID = gr.groupID;
                    VKontakte_Sta_ByDate_Parallels(exreqW);
                    Thread.Sleep(1500);
                }

                exreqW.dateFrom = DateTime.Today.Date.AddDays(-dw + 1);
                exreqW.dateTo = DateTime.Now;
                exreqW.dateType = DateType.week;

                foreach (var gr in newlst.dir)
                {
                    exreqW.groupID = gr.groupID;
                    VKontakte_Sta_ByDate_Parallels(exreqW);
                    Thread.Sleep(1500);
                }
            }
        }
        #endregion

        #region VKontakte_Sta_ByDate
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
            long countPost = 0;

            ulong appId = 5391843; // указываем id приложения
            string email = "89652562584"; // email для авторизации
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

                api.Stats.TrackVisitor();
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
                likes = 0;
                comments = 0;
                reposts = 0;
                offset = 0;

                WallGetObject respWall = api_Wall_Get(api, groupId, offset);

                ulong cnt = respWall.TotalCount;

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
                    if (reqCount == 3) // 3 запроса в секунду
                    {
                        Thread.Sleep(1500);
                        reqCount = 0;
                    }

                    respWall = api_Wall_Get(api, groupId, offset);

                    reqCount++;

                    foreach (Post post in respWall.WallPosts)
                    {
                        likes += post.Likes.Count;
                        comments += post.Comments.Count;
                        reposts += post.Reposts.Count;

                        if ((dateFrom > post.Date) && (dateTo < post.Date))
                        {
                            countPost += 1;
                        }
                    };

                    offset += 100;
                }

                dc.StaCommVKDaily_Save(groupId, dateFrom, views, visitors, reach, reach_subscribers, subscribed, unsubscribed, likes, comments, reposts, countPost, members);
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
        #endregion

        #region VKontakte_Sta_ByDate_Parallels
        public void VKontakte_Sta_ByDate_Parallels(wsRequestByDate req)
        {
            long groupId = req.groupID;
            DateTime dateFrom = req.dateFrom;
            DateTime? dateTo = req.dateTo;

            HubDataClassesDataContext dc = new HubDataClassesDataContext();

            DateTime dateStart = DateTime.Now;

            // - Periodically ↓↓↓
            long subscribed = 0;
            long unsubscribed = 0;
            long visitors = 0;
            long views = 0;
            long reach = 0;
            long reach_subscribers = 0;
            // - Periodically ↑↑↑

            int members = 0;

            VkApi api = api_Authorize(6);

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

                WallGetObject respWall = api_Wall_Get(api, groupId, 0);

                ulong cnt = respWall.TotalCount;

                List<commPosts> lst = new List<commPosts>();

                long likes = 0;
                long comments = 0;
                long reposts = 0;
                long countPost = 0;

                int remaining = 5;

                ulong leftover = cnt % 100;
                ulong countPerThread = (cnt / 100) * 5;
                ulong offset = 0;

                using (ManualResetEvent mre = new ManualResetEvent(false))
                {
                    ThreadPool.QueueUserWorkItem(delegate
                    {
                        for (int i = 1; i < 6; i++)
                        {
                            if (i == 5)
                            {
                                countPerThread += leftover;
                            }

                            lst.Add(calcpost(offset, countPerThread, api_Authorize(i), groupId, dateFrom, dateTo));
                            offset += countPerThread;
                        }
                        foreach (commPosts cmp in lst)
                        {
                            likes += cmp.likes;
                            comments += cmp.comments;
                            reposts += cmp.repost;
                            countPost += cmp.count;
                        }

                        if (req.dateType == DateType.week)
                        {
                            dc.StaCommVKWeekly_Save(groupId, dateFrom, views, visitors, reach, reach_subscribers, subscribed, unsubscribed, likes, comments, reposts, countPost, members);
                        }
                        else if (req.dateType == DateType.day)
                        {
                            dc.StaCommVKDaily_Save(groupId, dateFrom, views, visitors, reach, reach_subscribers, subscribed, unsubscribed, likes, comments, reposts, countPost, members);
                        }
                        // проверяем, не последнее ли это задание выполнилось
                        if (Interlocked.Decrement(ref remaining) == 0)
                        {
                            mre.Set();
                        }
                    });
                }
            }
            catch (AccessDeniedException e)
            {
                string exInnerExceptionMessage = "";
                if (e.InnerException != null)
                {
                    exInnerExceptionMessage = e.InnerException.Message;
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
                dc.Exception_Save("VKontakte_Sta_ByDate_Parallels", "", e.Message, exInnerExceptionMessage, e.HelpLink, e.HResult, e.Source, e.StackTrace);
            }
        }
        #endregion

        #region calcpost
        public commPosts calcpost(ulong offset, ulong cnt, VkApi api, long groupId, DateTime dateFrom, DateTime? dateTo)
        {
            long likes = 0;
            long comments = 0;
            long reposts = 0;
            long countPost = 0;
            int reqCount = 0;

            while (offset < cnt)
            {
                if (reqCount == 3) // 3 запроса в секунду
                {
                    Thread.Sleep(1500);
                    reqCount = 0;
                }

                WallGetObject respWall = api_Wall_Get(api, groupId, offset);

                reqCount++;

                foreach (Post post in respWall.WallPosts)
                {
                    if (post.Date <= dateTo)
                    {
                        likes += post.Likes.Count;
                        comments += post.Comments.Count;
                        reposts += post.Reposts.Count;
                    }

                    if ((dateFrom < post.Date) && (dateTo > post.Date))
                    {
                        countPost += 1;
                    }
                };

                offset += 100;
            }

            commPosts calcPost = new commPosts()
            {
                comments = comments,
                likes = likes,
                repost = reposts,
                count = countPost,
            };

            return calcPost;
        }
        #endregion

        #region SetCommVK
        private static long SetCommVK(string link)
        {
            long groupID = 0;
            string name = "";
            string photoLink = "";
            HubDataClassesDataContext dc = new HubDataClassesDataContext();

            VkApi api = api_Authorize(3);

            IEnumerable<string> groupIds = new string[] { link };
            ReadOnlyCollection<Group> groups = api.Groups.GetById(groupIds, "", GroupsFields.Description);

            foreach (Group group in groups)
            {
                groupID = group.Id;
                name = group.Name;
                photoLink = group.Photo100.ToString();
            }

            dc.Comm_Set(link, groupID, name, photoLink);
            return groupID;
        }
        #endregion

        #region GetCountMembers
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
        #endregion

        #region api_Authorize
        private static VkApi api_Authorize(int thread)
        {
            HubDataClassesDataContext dc = new HubDataClassesDataContext();

            string login; // login для авторизации

            switch (thread)
            {
                case 1:
                    login = "89652562584";
                    break;
                case 2:
                    login = "89652562849";
                    break;
                case 3:
                    login = "89152810427";
                    break;
                case 4:
                    login = "89169495598";
                    break;
                case 5:
                    login = "89299833547";
                    break;
                case 6:
                    login = "89157232003";
                    break;
                case 7:
                    login = "89299833566";
                    break;
                default:
                    login = "89652562584";
                    break;
            }

            ulong appId = 5391843; // указываем id приложения
            string password = "PressNon798520"; // пароль
            Settings settings = Settings.All; // уровень доступа к данным

            VkApi api = new VkApi();
            try
            {
                api.Authorize(new ApiAuthParams
                {
                    ApplicationId = appId,
                    Login = login,
                    Password = password,
                    Settings = settings
                }); // авторизуемся

                api.Stats.TrackVisitor();
            }
            catch (TooManyRequestsException)
            {
                Thread.Sleep(1000);
                api = api_Authorize(7);
            }
            catch (Exception e)
            {
                string exInnerExceptionMessage = "";
                if (e.InnerException != null)
                {
                    exInnerExceptionMessage = e.InnerException.Message;
                }
                dc.Exception_Save("VKontakte_Sta_ByDate", "VkApi.Authorize", e.Message, exInnerExceptionMessage, e.HelpLink, e.HResult, e.Source, e.StackTrace);
            }

            return api;
        }
        #endregion

        #region api_Wall_Get
        private WallGetObject api_Wall_Get(VkApi api, long groupId, ulong offset)
        {
            WallGetObject res;

            try
            {
                res = api.Wall.Get(new WallGetParams
                {
                    OwnerId = 0 - groupId,
                    Offset = offset,
                    Count = 100,
                    Filter = Owner,
                    Extended = false
                });
            }
            catch (TooManyRequestsException)
            {
                Thread.Sleep(1000);
                res = api_Wall_Get(api, groupId, offset);
            }

            return res;
        }
        #endregion

        #region GetVKGroups
        private static wsGroups<Comm_ReadForSta_Resp> GetVKGroups(bool isNewComm)
        {
            var results = new wsGroups<Comm_ReadForSta_Resp>();
            var resp = new Comm_ReadForSta_Resp();
            HubDataClassesDataContext dc = new HubDataClassesDataContext();

            foreach (Comm_ReadForStaResult comm in dc.Comm_ReadForSta(isNewComm))
            {
                resp.Add(new wsGroup()
                {
                    groupID = comm.groupID ?? 0,
                    link = comm.link
                });
            };

            results.dir = resp;

            return results;
        }
        #endregion
    }
}
