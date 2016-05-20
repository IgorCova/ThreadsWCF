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
using xNet;
using System.Xml;
using Newtonsoft.Json;
using System.Web.Script.Serialization;
using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json.Linq;

namespace CommSta
{
    public class CommStaService : IService
    {
        #region OK_Sta
        public void OK_Sta()
        {
            if ((DateTime.Now.Hour == 10) || (DateTime.Now.Hour == 17))
            {
                ok_UpdateComm();
            }            
        }
        #endregion

        #region VKontakte_Sta_Graph
        public void VKontakte_Sta_Graph()
        {
            using (HttpRequest net = new HttpRequest())
            {
                net.UserAgent = Http.ChromeUserAgent();
                CookieDictionary coockie = new CookieDictionary(false);
                net.Cookies = coockie;
                net.CharacterSet = Encoding.GetEncoding("utf-8");

                HubDataClassesDataContext dc = new HubDataClassesDataContext();

                try
                {
                    string data = net.Get("https://vk.com?_fm=index").ToString();
                    string lg_h = data.Substring("name=\"lg_h\" value=\"", "\"");
                    string log = net.Get(string.Format("https://login.vk.com/?act=login&email={0}&pass={1}&lg_h={2}", "89299833547", "PressNon798520", lg_h)).ToString();

                    wsGroups groups = getGroups(false, "vk");

                    foreach (var gr in groups)
                    {
                        long groupID = gr.groupID;
                        string resp = net.Get(string.Format("https://vk.com/stats?act=activity&gid={0}", groupID)).ToString();

                        if (resp.IndexOf("cur.graphDatas['feedback_graph'] = ") == 0)
                        {

                        }

                        int substrIndexIn = resp.IndexOf("cur.graphDatas['feedback_graph'] = ") + 36;
                        int substrIndexOut = resp.IndexOf("cur.graphUrls['feedback_graph'] = ");

                        string json = resp.Substring(substrIndexIn, substrIndexOut - substrIndexIn - 3).Replace("root", "feed");

                        // To convert JSON text contained in string json into an XML node
                        XmlDocument xdoc = JsonConvert.DeserializeXmlNode("{\"root\":" + json + "}", "root");
                        string feedback_graph = xdoc.InnerXml.ToString();

                        substrIndexIn = resp.IndexOf("cur.graphDatas['activity_graph'] = ") + 36;
                        substrIndexOut = resp.IndexOf("cur.graphUrls['activity_graph'] = ");

                        json = resp.Substring(substrIndexIn, substrIndexOut - substrIndexIn - 3);

                        // To convert JSON text contained in string json into an XML node
                        var jsonWithRoot = string.Format("{{\"root\": {0}}}", json);
                        xdoc = JsonConvert.DeserializeXmlNode(jsonWithRoot, "root");

                        JavaScriptSerializer serializer = new JavaScriptSerializer();
                        string activity_graph = xdoc.InnerXml.ToString();

                        dc.StaCommVKGraph_Save(groupID, feedback_graph, activity_graph);
                    }
                }
                catch (Exception e)
                {
                    string exInnerExceptionMessage = "";
                    if (e.InnerException != null)
                    {
                        exInnerExceptionMessage = e.InnerException.Message;
                    }
                    dc.Exception_Save("VKontakte_Sta_Graph", "", e.Message, exInnerExceptionMessage, e.HelpLink, e.HResult, e.Source, e.StackTrace);
                }
            }
        }
        #endregion

        #region VKontakte_Sta
        public void VKontakte_Sta()
        {
            if (DateTime.Now.Hour == 0)
            {
                VKontakte_Sta_CloseDay();
            }

            if ((DateTime.Now.Hour == 10) || (DateTime.Now.Hour == 17))
            {
                vk_UpdateComm();
            }

            wsRequestByDate exreq = new wsRequestByDate();

            exreq.dateFrom = DateTime.Today.Date;
            exreq.dateTo = DateTime.Now;
            exreq.dateType = DateType.day;

            wsGroups lst = getGroups(false, "vk");

            foreach (var gr in lst)
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

            foreach (var gr in lst)
            {
                exreqW.groupID = gr.groupID;
                VKontakte_Sta_ByDate_Parallels(exreqW);
                Thread.Sleep(1500);
            }

            VKontakte_Sta_Graph();
        }
        #endregion

        #region VKontakte_Sta_CloseDay
        public void VKontakte_Sta_CloseDay()
        {
            wsRequestByDate exreq = new wsRequestByDate();

            exreq.dateFrom = DateTime.Today.AddDays(-1).Date;
            exreq.dateTo = DateTime.Today.Date.AddMilliseconds(-1);
            exreq.dateType = DateType.day;

            wsGroups lst = getGroups(false, "vk");

            foreach (var gr in lst)
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
        #endregion

        #region VKontakte_Sta_CloseWeek
        public void VKontakte_Sta_CloseWeek()
        {
            wsRequestByDate exreq = new wsRequestByDate();

            exreq.dateFrom = DateTime.Today.Date.AddDays(-7);
            exreq.dateTo = DateTime.Today.Date.AddMilliseconds(-1);
            exreq.dateType = DateType.week;

            wsGroups lst = getGroups(false, "vk");

            foreach (var gr in lst)
            {
                exreq.groupID = gr.groupID;
                VKontakte_Sta_ByDate_Parallels(exreq);
                Thread.Sleep(1500);
            }
        }
        #endregion

        #region VKontakte_Sta_ForNew
        public void VKontakte_Sta_ForNew()
        {
            wsRequestByDate exreq = new wsRequestByDate();
            exreq.dateType = DateType.day;

            // Для новых сообществ за вчера и позавчера считаем статистику
            wsGroups newlst = getGroups(true, "vk");
            if (newlst.Count > 0)
            {
                exreq.dateFrom = DateTime.Today.Date;
                exreq.dateTo = DateTime.Now;

                foreach (var gr in newlst)
                {
                    if (gr.groupID == 0)
                    {
                        gr.groupID = vk_SetComm(gr.link);
                        Thread.Sleep(1000);
                    }
                }

                foreach (var gr in newlst)
                {
                    exreq.groupID = gr.groupID;
                    VKontakte_Sta_ByDate_Parallels(exreq);
                    Thread.Sleep(1500);
                }

                exreq.dateFrom = DateTime.Today.Date.AddDays(-1);
                exreq.dateTo = DateTime.Today.Date.AddMilliseconds(-1);

                foreach (var gr in newlst)
                {
                    exreq.groupID = gr.groupID;
                    VKontakte_Sta_ByDate_Parallels(exreq);
                    Thread.Sleep(1500);
                }

                exreq.dateFrom = DateTime.Today.Date.AddDays(-2);
                exreq.dateTo = DateTime.Today.Date.AddDays(-1).AddMilliseconds(-1);

                foreach (var gr in newlst)
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

                foreach (var gr in newlst)
                {
                    exreqW.groupID = gr.groupID;
                    VKontakte_Sta_ByDate_Parallels(exreqW);
                    Thread.Sleep(1500);
                }

                exreqW.dateFrom = DateTime.Today.Date.AddDays(-dw + 1).AddDays(-7);
                exreqW.dateTo = DateTime.Today.Date.AddDays(-dw + 1).AddMilliseconds(-1);

                foreach (var gr in newlst)
                {
                    exreqW.groupID = gr.groupID;
                    VKontakte_Sta_ByDate_Parallels(exreqW);
                    Thread.Sleep(1500);
                }

                exreqW.dateFrom = DateTime.Today.Date.AddDays(-dw + 1);
                exreqW.dateTo = DateTime.Now;
                exreqW.dateType = DateType.week;

                foreach (var gr in newlst)
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

                members = vk_GetCountMembers(api, groupId);
                offset = 0;

                WallGetObject respWall = vk_Wall_Get(api, groupId, offset);

                ulong cnt = respWall.TotalCount;

                offset = 100;

                int reqCount = 0;

                while (offset < cnt)
                {
                    if (reqCount == 3) // 3 запроса в секунду
                    {
                        Thread.Sleep(1500);
                        reqCount = 0;
                    }

                    respWall = vk_Wall_Get(api, groupId, offset);

                    reqCount++;

                    foreach (Post post in respWall.WallPosts)
                    {
                        if ((dateFrom > post.Date) && (dateTo < post.Date))
                        {
                            countPost += 1;
                        }
                    };

                    offset += 100;
                }

                dc.StaCommVKDaily_Save(groupId, dateFrom, views, visitors, reach, reach_subscribers, subscribed, unsubscribed, countPost, members);
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

            VkApi api = vk_Authorize(6);

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

                members = vk_GetCountMembers(api, groupId);

                WallGetObject respWall = vk_Wall_Get(api, groupId, 0);

                ulong cnt = respWall.TotalCount;

                List<commPosts> lst = new List<commPosts>();

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

                            lst.Add(vk_calcPost(offset, countPerThread, vk_Authorize(i), groupId, dateFrom, dateTo));
                            offset += countPerThread;
                        }
                        foreach (commPosts cmp in lst)
                        {
                            countPost += cmp.count;
                        }

                        if (req.dateType == DateType.week)
                        {
                            dc.StaCommVKWeekly_Save(groupId, dateFrom, views, visitors, reach, reach_subscribers, subscribed, unsubscribed, countPost, members);
                        }
                        else if (req.dateType == DateType.day)
                        {
                            dc.StaCommVKDaily_Save(groupId, dateFrom, views, visitors, reach, reach_subscribers, subscribed, unsubscribed, countPost, members);
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

        #region vk_calcPost
        public commPosts vk_calcPost(ulong offset, ulong cnt, VkApi api, long groupId, DateTime dateFrom, DateTime? dateTo)
        {
            long countPost = 0;
            int reqCount = 0;
            bool isOlderPosts = false;
            while ((offset < cnt) && (isOlderPosts == false))
            {
                if (reqCount == 3) // 3 запроса в секунду
                {
                    Thread.Sleep(3000);
                    reqCount = 0;
                }

                WallGetObject respWall = vk_Wall_Get(api, groupId, offset);

                reqCount++;

                foreach (Post post in respWall.WallPosts)
                {
                    if ((dateFrom < post.Date) && (dateTo > post.Date))
                    {
                        countPost += 1;
                    }

                    isOlderPosts = post.Date < dateFrom;

                };

                offset += 100;
            }

            commPosts comm = new commPosts()
            {
                count = countPost,
            };

            return comm;
        }
        #endregion

        #region vk_UpdateComm
        private static void vk_UpdateComm()
        {
            wsGroups groups = getGroups(false, "vk");

            if (groups.Count > 0)
            {
                foreach (var gr in groups)
                {
                    vk_SetComm(gr.link);
                    Thread.Sleep(1000);
                }
            }
        }
        #endregion

        #region vk_SetComm
        private static long vk_SetComm(string link)
        {
            long groupID = 0;
            string name = "";
            string photoLink = "";
            string photoLinkBig = "";
            HubDataClassesDataContext dc = new HubDataClassesDataContext();

            VkApi api = vk_Authorize(6);

            IEnumerable<string> groupIds = new string[] { link };
            ReadOnlyCollection<Group> groups = api.Groups.GetById(groupIds, "", GroupsFields.Description);

            foreach (Group group in groups)
            {
                groupID = group.Id;
                name = group.Name;
                photoLink = group.Photo100.ToString();
                photoLinkBig = group.Photo200.ToString();
            }

            dc.Comm_Set(link, groupID, name, photoLink, photoLinkBig, "vk");
            return groupID;
        }
        #endregion

        #region ok_UpdateComm
        private static void ok_UpdateComm()
        {
            wsGroups groups = getGroups(false, "ok");

            if (groups.Count > 0)
            {
                foreach (var gr in groups)
                {
                    ok_SetComm(gr.link);
                    Thread.Sleep(1000);
                }
            }
        }
        #endregion

        #region ok_SetComm
        private static long ok_SetComm(string link)
        {
            long groupID = ok_GetGroupID(link);
            string name = "";
            string photo_id = "";
            string photoLink = "";
            string photoLinkBig = "";

            string method = "group.getInfo";
            string fields = "uid,name,shortname,photo_id,members_count";

            string sig;
            HubDataClassesDataContext dc = new HubDataClassesDataContext();

            using (HttpRequest net = new HttpRequest())
            {
                net.CharacterSet = Encoding.GetEncoding("utf-8");

                string sigSource = string.Format("application_key={0}fields={1}method={2}session_key={3}uids={4}{5}"
                    , ok_application_key
                    , fields
                    , method
                    , ok_secret_session_key
                    , groupID
                    , ok_secret_session_key
                    );

                using (MD5 md5Hash = MD5.Create())
                {
                    sig = getMd5Hash(md5Hash, sigSource);
                }

                string sourceuri = string.Format("https://api.ok.ru/fb.do?application_key={0}&sig={1}&session_key={2}&uids={3}&fields={4}&method={5}&ok_access_token={6}"
                    , ok_application_key
                    , sig
                    , ok_secret_session_key
                    , groupID
                    , fields
                    , method
                    , ok_access_token
                    );

                string source = net.Get(sourceuri).ToString();
                JArray a = JArray.Parse(source);
                foreach (JObject o in a.Children<JObject>())
                {
                    foreach (JProperty p in o.Properties())
                    {
                        if (p.Name == "name")
                        {
                            name = (string)p.Value;
                        }
                        else if (p.Name == "photo_id")
                            photo_id = (string)p.Value;
                    }
                }
            }

            photoLink = string.Format("http://groupava2.odnoklassniki.ru/getImage?photoId={0}&photoType=2", photo_id);
            photoLinkBig = string.Format("http://groupava2.odnoklassniki.ru/getImage?photoId={0}&photoType=1", photo_id);

            dc.Comm_Set(link, groupID, name, photoLink, photoLinkBig, "ok");

            return groupID;
        }
        #endregion

        #region ok_GetGroupID
        private static long ok_GetGroupID(string link)
        {
            link = string.Format("https://ok.ru/{0}", link);
            long groupID = 0;
            wsGroups newlst = getGroups(true, "ok");
            string method = "url.getInfo";
            string sig;

            using (HttpRequest net = new HttpRequest())
            {
                net.CharacterSet = Encoding.GetEncoding("utf-8");
                string sigSource = string.Format("application_key={0}method={1}session_key={2}url={3}{4}"
                    , ok_application_key
                    , method
                    , ok_secret_session_key
                    , link
                    , ok_secret_session_key
                    );

                using (MD5 md5Hash = MD5.Create())
                {
                    sig = getMd5Hash(md5Hash, sigSource);
                }

                string sourceuri = string.Format("https://api.ok.ru/fb.do?application_key={0}&sig={1}&session_key={2}&url={3}&method={4}&ok_access_token={5}"
                    , ok_application_key
                    , sig
                    , ok_secret_session_key
                    , link
                    , method
                    , ok_access_token
                    );

                string source = net.Get(sourceuri).ToString();
                dynamic data = JObject.Parse(source);
                groupID = data.objectId;
            }

            return groupID;
        }
        #endregion

        #region vk_GetCountMembers
        private int vk_GetCountMembers(VkApi api, long groupId)
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

        #region vk_Authorize
        private static VkApi vk_Authorize(int thread)
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
                api = vk_Authorize(7);
            }
            catch (Exception e)
            {
                string exInnerExceptionMessage = "";
                if (e.InnerException != null)
                {
                    exInnerExceptionMessage = e.InnerException.Message;
                }
                dc.Exception_Save("vk_Authorize", "", e.Message, exInnerExceptionMessage, e.HelpLink, e.HResult, e.Source, e.StackTrace);
            }

            return api;
        }
        #endregion

        #region vk_Wall_Get
        private WallGetObject vk_Wall_Get(VkApi api, long groupId, ulong offset)
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
                res = vk_Wall_Get(api, groupId, offset);
            }

            return res;
        }
        #endregion

        #region getGroups
        private static wsGroups getGroups(bool isNewComm, string areaCode)
        {
            var results = new wsGroups();
            HubDataClassesDataContext dc = new HubDataClassesDataContext();

            foreach (Comm_ReadForStaResult comm in dc.Comm_ReadForSta(isNewComm, areaCode))
            {
                results.Add(new wsGroup()
                {
                    groupID = comm.groupID ?? 0,
                    link = comm.link
                });
            };

            return results;
        }
        #endregion

        #region getDayOfWeek
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
        #endregion

        #region getMd5Hash
        static string getMd5Hash(MD5 md5Hash, string input)
        {

            // Convert the input string to a byte array and compute the hash.
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }
        #endregion

        #region public
        public WallFilter Owner { get; private set; }
        public static string ok_access_token = "tkn18ulFlCCtfqPQMQzX5yoVrQNa4E5IT9uhP5V5KLgO1mkfCtbtSgaVPqeJvR7TIW5lq";
        public static string ok_secret_session_key = "9deb812bf2ea8c05f32eb79c24427d14";
        public static string ok_application_key = "CBAPDFFLEBABABABA";
        public static string ok_application_key_secret = "A050BBA2FE2FCA7DFBFB945A";
        #endregion

    }
}