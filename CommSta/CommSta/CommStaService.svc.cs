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
using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json.Linq;

namespace CommSta {
    public class CommStaService : IService {

        public void zzz_tester() {
        
        }

        #region OK_Sta_Graph
        public void OK_Sta_Graph() {
            using (HttpRequest net = new HttpRequest()) {
                net.UserAgent = Http.ChromeUserAgent();
                CookieDictionary coockie = new CookieDictionary(false);
                net.Cookies = coockie;
                net.CharacterSet = Encoding.GetEncoding("utf-8");
                HttpResponse resp;

                HubDataClassesDataContext dc = new HubDataClassesDataContext();

                try {
                    string strUri = string.Format("{0}ok.ru/oauth/authorize?client_id={1}&scope={2}&response_type=code&redirect_uri={0}{3}", "https://", ok_aaplication_id, "VALUABLE_ACCESS;GROUP_CONTENT", "major-comm.com/ok");
                    resp = net.Get(strUri);

                    string strUriPost = resp.Address.AbsoluteUri;
                    // resp = net.Get(strUriPost + "&fr.posted=set&fr.email=89164913669&fr.password=test1221&fr.remember=on");

                    var reqParams = new RequestParams();

                    strUri = "https://www.ok.ru/";
                    resp = net.Get(strUri);

                    strUriPost = "https://ok.ru/dk?cmd=AnonymLogin&st.cmd=anonymLogin&httpsdata=" + resp.Location ?? "";

                    reqParams["st.redirect"] = "";
                    reqParams["st.asr"] = "";
                    reqParams["st.posted"] = "set";
                    reqParams["st.originalaction"] = "https://ok.ru/dk?cmd=AnonymLogin&st.cmd=anonymLogin";
                    reqParams["st.fJS"] = "on";
                    reqParams["st.st.screenSize"] = "1920 x 1080";
                    reqParams["st.st.browserSize"] = "920";
                    reqParams["st.st.flashVer"] = "21.0.0";
                    reqParams["st.email"] = "89099304038"; // "89164913669"; 
                    reqParams["st.password"] = "jMIuXIwUKU"; // "test1221"; 
                    reqParams["st.remember"] = "on";
                    reqParams["st.iscode"] = "false";
                    resp = net.Post(strUriPost, reqParams);
                    string data = resp.ToString();

                    resp = net.Get("https://ok.ru/designideas.da/stat/usertrends");
                    data = resp.ToString();

                } catch (Exception e) {
                    string exInnerExceptionMessage = e.Message;
                    if (e.InnerException != null) {
                        exInnerExceptionMessage = e.InnerException.Message;
                    }
                    dc.Exception_Save("OK_Sta_Graph", "", e.Message, exInnerExceptionMessage, e.HelpLink, e.HResult, e.Source, e.StackTrace);
                }
            }
        }
        #endregion

        #region OK_Sta
        public void OK_Sta() {
            wsGroups lstOK = getGroups(false, "ok");

            foreach (wsGroup gr in lstOK) {
                ok_GetStatTopics(DateTime.Now.Date, gr.groupID);
                ok_GetStatTrends(DateTime.Now.Date, gr.groupID);

                if (DateTime.Now.Hour == 0) {
                    ok_GetStatTopics(DateTime.Now.Date.AddDays(-1), gr.groupID);
                    ok_GetStatTrends(DateTime.Now.Date.AddDays(-1), gr.groupID);
                }
            }
        }
        #endregion

        #region OK_Sta_ForNew
        public void OK_Sta_ForNew() {
            wsGroups groups = getGroups(true, "ok");

            int day = getDayOfWeek();

            if (groups.Count > 0) {
                foreach (var gr in groups) {
                    if (gr.groupID == 0) {
                        gr.groupID = ok_SetComm(gr);

                    }
                    Thread.Sleep(5000);

                    //last  two weeks
                    for (int i = 0; i < 14 + day; i++) {
                        ok_GetStatTopics(DateTime.Now.Date.AddDays(-i), gr.groupID);
                        Thread.Sleep(1000);
                        ok_GetStatTrends(DateTime.Now.Date.AddDays(-i), gr.groupID);
                    }
                }
            }
        }
        #endregion

        #region VKontakte_Sta_Graph
        public void VKontakte_Sta_Graph() {
            string login = "";
            string password = "";
            bool is_norm = false;

            HubDataClassesDataContext dc = new HubDataClassesDataContext();

            wsGroups groups = getGroupsVkTop(7);
            int cnt = 1;

            foreach (var gr in groups) {
                get_vklog(cnt, out login, out password);
                is_norm = VKontakte_Sta_Graph_Is(login, password, gr.groupID);

                if (is_norm == false) {
                    string note = string.Format("try with login: {0}, password: {1}", login, password);
                    dc.Exception_Save("VKontakte_Sta_Graph", note, "", "", "", 0, "", "");
                }
                cnt++;
            }
        }

        public void VKontakte_Sta_GraphTest() {
            string login = "";
            string password = "";
            bool is_norm = false;
            wsGroups groups = getGroups(false, "vk");
            int cnt = 1;
            get_vklog(cnt, out login, out password);

            foreach (var gr in groups) {
                is_norm = VKontakte_Sta_Graph_Is(login, password, gr.groupID);

                while (is_norm == false) {
                    if (cnt < 7) {
                        cnt++;
                    } else {
                        cnt = 1;
                    }

                    get_vklog(cnt, out login, out password);
                    is_norm = VKontakte_Sta_Graph_Is(login, password, gr.groupID);
                }
            }
        }

        private bool VKontakte_Sta_Graph_Is(string login, string password, long groupID) {
            bool res = false;
            using (HttpRequest net = new HttpRequest()) {
                net.UserAgent = Http.ChromeUserAgent();
                CookieDictionary coockie = new CookieDictionary(false);
                net.Cookies = coockie;
                net.CharacterSet = Encoding.GetEncoding("utf-8");

                HubDataClassesDataContext dc = new HubDataClassesDataContext();

                try {
                    string data = net.Get(string.Format("https://{0}vk.com?_fm=index", login == "89652562849" ? "new." : "")).ToString();
                    string lg_h = data.Substring("name=\"lg_h\" value=\"", "\"");
                    string log = net.Get(string.Format("https://login.vk.com/?act=login&email={0}&pass={1}&lg_h={2}", login, password, lg_h)).ToString();

                    int substr = log.IndexOf("<title>News</title>");
                    if (substr == 0) {
                        res = false;
                        throw new Exception(string.Format("login failed. login: {0}, password: {1}", login, password));
                    }

                    string json;
                    string feedback_graph = "";
                    string activity_graph = "";
                    string members_graph = "";

                    XmlDocument xdoc;

                    string resp = net.Get(string.Format("https://{0}vk.com/stats?act=activity&gid={1}", login == "89652562849" ? "new." : "", groupID)).ToString();
                    #region feedback_graph                   
                    int substrIndexIn = resp.IndexOf("cur.graphDatas['feedback_graph'] = ") + 36;
                    int substrIndexOut = resp.IndexOf("cur.graphUrls['feedback_graph'] = ");
                    if (substrIndexOut > 0) {
                        json = resp.Substring(substrIndexIn, substrIndexOut - substrIndexIn - 3).Replace("root", "feed");
                        // To convert JSON text contained in string json into an XML node
                        xdoc = JsonConvert.DeserializeXmlNode("{\"root\":" + json + "}", "root");
                        feedback_graph = xdoc.InnerXml.ToString();
                    } else {
                        res = false;
                        throw new Exception("feedback_graph not found");
                    }
                    #endregion

                    #region activity_graph
                    substrIndexIn = resp.IndexOf("cur.graphDatas['activity_graph'] = ") + 36;
                    substrIndexOut = resp.IndexOf("cur.graphUrls['activity_graph'] = ");
                    if (substrIndexOut > 0) {
                        json = resp.Substring(substrIndexIn, substrIndexOut - substrIndexIn - 3);

                        // To convert JSON text contained in string json into an XML node
                        var jsonWithRoot = string.Format("{{\"root\": {0}}}", json);
                        xdoc = JsonConvert.DeserializeXmlNode(jsonWithRoot, "root");

                        activity_graph = xdoc.InnerXml.ToString();
                    } else {
                        res = false;
                        throw new Exception("activity_graph not found");
                    }

                    #endregion

                    #region members_graph
                    Thread.Sleep(3000);
                    resp = net.Get(string.Format("https://vk.com/stats?gid={0}", groupID)).ToString();

                    substrIndexIn = resp.IndexOf("cur.graphDatas['members_graph'] = ") + 35;
                    substrIndexOut = resp.IndexOf("cur.graphUrls['members_graph'] = ");
                    if (substrIndexOut > 0) {
                        json = resp.Substring(substrIndexIn, substrIndexOut - substrIndexIn - 3);

                        // To convert JSON text contained in string json into an XML node                       
                        xdoc = xdoc = JsonConvert.DeserializeXmlNode("{\"root\":" + json + "}", "root");
                        members_graph = xdoc.InnerXml.ToString();
                    } else {
                        res = false;
                        throw new Exception("members_graph not found");
                    }
                    #endregion

                    dc.StaCommVKGraph_Save(groupID, feedback_graph, activity_graph, members_graph);
                    res = true;

                } catch (Exception e) {
                    string exInnerExceptionMessage = "";
                    if (e.InnerException != null) {
                        exInnerExceptionMessage = e.InnerException.Message;
                    }
                    dc.Exception_Save("VKontakte_Sta_Graph_Is", "", e.Message, exInnerExceptionMessage, e.HelpLink, e.HResult, e.Source, e.StackTrace);
                    res = false;
                }
            }

            return res;
        }
        #endregion

        #region VKontakte_Sta
        public void VKontakte_Sta() {
            if (DateTime.Now.Hour == 0) {
                VKontakte_Sta_CloseDay();
            }

            wsRequestByDate exreq = new wsRequestByDate();

            exreq.dateFrom = DateTime.Today.Date;
            exreq.dateTo = DateTime.Now;
            exreq.dateType = DateType.day;

            wsGroups lstVK = getGroups(false, "vk");

            foreach (var gr in lstVK) {
                exreq.groupID = gr.groupID;
                VKontakte_Sta_ByDate(exreq);
                Thread.Sleep(1500);
            }

            wsRequestByDate exreqW = new wsRequestByDate();

            exreqW.dateFrom = DateTime.Today.Date.AddDays(-getDayOfWeek());
            exreqW.dateTo = DateTime.Now;
            exreqW.dateType = DateType.week;

            foreach (var gr in lstVK) {
                exreqW.groupID = gr.groupID;
                VKontakte_Sta_ByDate(exreqW);
                Thread.Sleep(1500);
            }
        }
        #endregion

        #region VKontakte_Sta_CloseDay
        private void VKontakte_Sta_CloseDay() {
            wsRequestByDate exreq = new wsRequestByDate();

            exreq.dateFrom = DateTime.Today.AddDays(-1).Date;
            exreq.dateTo = DateTime.Today.Date.AddMilliseconds(-1);
            exreq.dateType = DateType.day;

            wsGroups lst = getGroups(false, "vk");

            foreach (var gr in lst) {
                exreq.groupID = gr.groupID;
                VKontakte_Sta_ByDate(exreq);
                Thread.Sleep(1500);
            }

            if (getDayOfWeek() == 0) {
                VKontakte_Sta_CloseWeek();
            }
        }
        #endregion

        #region VKontakte_Sta_CloseWeek
        private void VKontakte_Sta_CloseWeek() {
            wsRequestByDate exreq = new wsRequestByDate();

            exreq.dateFrom = DateTime.Today.Date.AddDays(-7);
            exreq.dateTo = DateTime.Today.Date.AddMilliseconds(-1);
            exreq.dateType = DateType.week;

            wsGroups lst = getGroups(false, "vk");

            foreach (var gr in lst) {
                exreq.groupID = gr.groupID;
                VKontakte_Sta_ByDate(exreq);
                Thread.Sleep(1500);
            }
        }
        #endregion

        #region VKontakte_Sta_ForNew
        public void VKontakte_Sta_ForNew() {
            wsRequestByDate exreq = new wsRequestByDate();
            exreq.dateType = DateType.day;

            // Для новых сообществ за вчера и позавчера считаем статистику
            wsGroups newlst = getGroups(true, "vk");
            if (newlst.Count > 0) {
                exreq.dateFrom = DateTime.Today.Date;
                exreq.dateTo = DateTime.Now;

                foreach (var gr in newlst) {
                    if (gr.groupID == 0) {
                        gr.groupID = vk_SetComm(gr.link);
                        Thread.Sleep(1000);
                    }
                }

                foreach (var gr in newlst) {
                    exreq.groupID = gr.groupID;
                    VKontakte_Sta_ByDate(exreq);
                    Thread.Sleep(1500);
                }

                exreq.dateFrom = DateTime.Today.Date.AddDays(-1);
                exreq.dateTo = DateTime.Today.Date.AddMilliseconds(-1);

                foreach (var gr in newlst) {
                    exreq.groupID = gr.groupID;
                    VKontakte_Sta_ByDate(exreq);
                    Thread.Sleep(1500);
                }

                exreq.dateFrom = DateTime.Today.Date.AddDays(-2);
                exreq.dateTo = DateTime.Today.Date.AddDays(-1).AddMilliseconds(-1);

                foreach (var gr in newlst) {
                    exreq.groupID = gr.groupID;
                    VKontakte_Sta_ByDate(exreq);
                    Thread.Sleep(1500);
                }

                // weekly report
                wsRequestByDate exreqW = new wsRequestByDate();

                int dw = getDayOfWeek();
                exreqW.dateType = DateType.week;

                exreqW.dateFrom = DateTime.Today.Date.AddDays(-dw + 1).AddDays(-14);
                exreqW.dateTo = DateTime.Today.Date.AddDays(-dw + 1).AddDays(-7).AddMilliseconds(-1);

                foreach (var gr in newlst) {
                    exreqW.groupID = gr.groupID;
                    VKontakte_Sta_ByDate(exreqW);
                    Thread.Sleep(1500);
                }

                exreqW.dateFrom = DateTime.Today.Date.AddDays(-dw + 1).AddDays(-7);
                exreqW.dateTo = DateTime.Today.Date.AddDays(-dw + 1).AddMilliseconds(-1);

                foreach (var gr in newlst) {
                    exreqW.groupID = gr.groupID;
                    VKontakte_Sta_ByDate(exreqW);
                    Thread.Sleep(1500);
                }

                exreqW.dateFrom = DateTime.Today.Date.AddDays(-dw + 1);
                exreqW.dateTo = DateTime.Now;
                exreqW.dateType = DateType.week;

                foreach (var gr in newlst) {
                    exreqW.groupID = gr.groupID;
                    VKontakte_Sta_ByDate(exreqW);
                    Thread.Sleep(1500);
                }
            }
        }
        #endregion

        #region VKontakte_Sta_ByDate
        private void VKontakte_Sta_ByDate(wsRequestByDate req) {
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
            Random rnd = new Random();
            int index = rnd.Next(1, 8);
            VkApi api = vk_Authorize(index);

            ReadOnlyCollection<StatsPeriod> res;

            try {
                res = api.Stats.GetByGroup(groupId, dateFrom, dateTo);

                if (res.Count > 0) {
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

                int remaining = 4;

                ulong leftover = cnt % 100;
                ulong countPerThread = (cnt / 100) * 4;
                ulong offset = 0;

                using (ManualResetEvent mre = new ManualResetEvent(false)) {
                    ThreadPool.QueueUserWorkItem(delegate {
                        for (int i = 1; i < 5; i++) {
                            if (i == 4) {
                                countPerThread += leftover;
                            }

                            lst.Add(vk_calcPost(offset, countPerThread, vk_Authorize(i), groupId, dateFrom, dateTo));
                            offset += countPerThread;
                        }

                        foreach (commPosts cmp in lst) {
                            countPost += cmp.count;
                        }

                        if (req.dateType == DateType.week) {
                            dc.StaCommVKWeekly_Save(groupId, dateFrom, views, visitors, reach, reach_subscribers, subscribed, unsubscribed, countPost, members);
                        } else if (req.dateType == DateType.day) {
                            dc.StaCommVKDaily_Save(groupId, dateFrom, views, visitors, reach, reach_subscribers, subscribed, unsubscribed, countPost, members);
                        }
                        // проверяем, не последнее ли это задание выполнилось
                        if (Interlocked.Decrement(ref remaining) == 0) {
                            mre.Set();
                        }
                    });
                }
            } catch (AccessDeniedException e) {
                string exInnerExceptionMessage = "";
                if (e.InnerException != null) {
                    exInnerExceptionMessage = e.InnerException.Message;
                }

                dc.GroupAccess_Save(groupId, e.Message, exInnerExceptionMessage);
            } catch (Exception e) {
                string exInnerExceptionMessage = "";
                if (e.InnerException != null) {
                    exInnerExceptionMessage = e.InnerException.Message;
                }
                dc.Exception_Save("VKontakte_Sta_ByDate", "", e.Message, exInnerExceptionMessage, e.HelpLink, e.HResult, e.Source, e.StackTrace);
            }
        }
        #endregion

        #region vk_calcPost
        private commPosts vk_calcPost(ulong offset, ulong cnt, VkApi api, long groupId, DateTime dateFrom, DateTime? dateTo) {
            long countPost = 0;
            int reqCount = 0;
            bool isOlderPosts = false;
            while ((offset < cnt) && (isOlderPosts == false)) {
                if (reqCount == 3) // 3 запроса в секунду
                {
                    Thread.Sleep(3000);
                    reqCount = 0;
                }

                WallGetObject respWall = vk_Wall_Get(api, groupId, offset);

                reqCount++;

                foreach (Post post in respWall.WallPosts) {
                    if ((dateFrom < post.Date) && (dateTo > post.Date)) {
                        countPost += 1;
                    }

                    isOlderPosts = post.Date < dateFrom;

                };

                offset += 100;
            }

            commPosts comm = new commPosts() {
                count = countPost,
            };

            return comm;
        }
        #endregion

        #region VK_UpdateComm
        public void VK_UpdateComm() {
            wsGroups groups = getGroups(false, "vk");

            if (groups.Count > 0) {
                foreach (var gr in groups) {
                    vk_SetComm(gr.link);
                    Thread.Sleep(2000);
                }
            }
        }
        #endregion

        #region vk_SetComm
        private static long vk_SetComm(string link) {
            long groupID = 0;
            string screenName = "";
            string name = "";
            string photoLink = "";
            string photoLinkBig = "";
            int membersCount = 0;

            HubDataClassesDataContext dc = new HubDataClassesDataContext();
            Random rnd = new Random();
            int index = rnd.Next(1, 8);
            VkApi api = vk_Authorize(index);

            IEnumerable<string> groupIds = new string[] { link };
            ReadOnlyCollection<Group> groups = api.Groups.GetById(groupIds, "", GroupsFields.Description);

            foreach (Group group in groups) {
                groupID = group.Id;
                screenName = group.ScreenName;
                name = group.Name;
                photoLink = group.Photo100.ToString();
                photoLinkBig = group.Photo200.ToString();
                membersCount = group.MembersCount ?? 0;
            }

            dc.Comm_Set(link, groupID, name, photoLink, photoLinkBig, "vk", membersCount, screenName);
            return groupID;
        }
        #endregion

        #region vk_getMembers
        private static void vk_getMembers(long groupID) {
            HubDataClassesDataContext dc = new HubDataClassesDataContext();

            VkApi api = vk_Authorize(2);
            VkApi api5 = vk_Authorize(3);
            VkApi api6 = vk_Authorize(7);

            int count = 10001;

            int reqCount = 0;
            int offset = 0;

            int fother = 0;
            int funder18 = 0;
            int ffrom18to21 = 0;
            int ffrom21to24 = 0;
            int ffrom24to27 = 0;
            int ffrom27to30 = 0;
            int ffrom30to35 = 0;
            int ffrom35to45 = 0;
            int fover45 = 0;

            int mother = 0;
            int munder18 = 0;
            int mfrom18to21 = 0;
            int mfrom21to24 = 0;
            int mfrom24to27 = 0;
            int mfrom27to30 = 0;
            int mfrom30to35 = 0;
            int mfrom35to45 = 0;
            int mover45 = 0;

            int uother = 0;
            int uunder18 = 0;
            int ufrom18to21 = 0;
            int ufrom21to24 = 0;
            int ufrom24to27 = 0;
            int ufrom27to30 = 0;
            int ufrom30to35 = 0;
            int ufrom35to45 = 0;
            int uover45 = 0;

            int year = DateTime.Now.Year;

            while (offset < count) {
                if (reqCount % 2 == 0) {
                    api = api5;
                } else {
                    api = api6;
                }

                if (reqCount % 6 == 0) {
                    Thread.Sleep(2500);
                }

                reqCount++;

                GroupsGetMembersParams ggmp = new GroupsGetMembersParams();
                ggmp.GroupId = groupID.ToString();
                ggmp.Fields = UsersFields.BirthDate | UsersFields.Sex;
                ggmp.Offset = 0;
                ggmp.Offset = offset;
                ReadOnlyCollection<User> users;

                try {
                    users = api.Groups.GetMembers(out count, ggmp);
                } catch {
                    users = api6.Groups.GetMembers(out count, ggmp);
                }

                foreach (User user in users) {
                    if ((user.BirthDate == null) || (user.BirthDate.Length < 6) || (user.BirthDate.Length > 10)) {
                        if (user.Sex == Sex.Female) {
                            fother += 1;
                        } else if (user.Sex == Sex.Male) {
                            mother += 1;
                        } else if (user.Sex == Sex.Unknown) {
                            uother += 1;
                        }
                    } else {
                        int age = 0;

                        try {
                            DateTime date = Convert.ToDateTime(user.BirthDate);
                            age = year - date.Year;
                        } catch {
                            age = 0;
                        }

                        if (age == 0) {
                            try {
                                age = year - Convert.ToInt32(user.BirthDate.Substring(user.BirthDate.Length - 4, 4));
                            } catch {
                                age = 0;
                            }
                        }

                        if (user.Sex == Sex.Female) {
                            if (age > 45) {
                                fover45 += 1;
                            } else if (age > 35) {
                                ffrom35to45 += 1;
                            } else if (age > 30) {
                                ffrom30to35 += 1;
                            } else if (age > 27) {
                                ffrom27to30 += 1;
                            } else if (age > 24) {
                                ffrom24to27 += 1;
                            } else if (age > 21) {
                                ffrom21to24 += 1;
                            } else if (age > 18) {
                                ffrom18to21 += 1;
                            } else {
                                funder18 += 1;
                            }

                        } else if (user.Sex == Sex.Male) {

                            if (age > 45) {
                                mover45 += 1;
                            } else if (age > 35) {
                                mfrom35to45 += 1;
                            } else if (age > 30) {
                                mfrom30to35 += 1;
                            } else if (age > 27) {
                                mfrom27to30 += 1;
                            } else if (age > 24) {
                                mfrom24to27 += 1;
                            } else if (age > 21) {
                                mfrom21to24 += 1;
                            } else if (age > 18) {
                                mfrom18to21 += 1;
                            } else {
                                munder18 += 1;
                            }
                        } else if (user.Sex == Sex.Unknown) {

                            if (age > 45) {
                                uover45 += 1;
                            } else if (age > 35) {
                                ufrom35to45 += 1;
                            } else if (age > 30) {
                                ufrom30to35 += 1;
                            } else if (age > 27) {
                                ufrom27to30 += 1;
                            } else if (age > 24) {
                                ufrom24to27 += 1;
                            } else if (age > 21) {
                                ufrom21to24 += 1;
                            } else if (age > 18) {
                                ufrom18to21 += 1;
                            } else {
                                uunder18 += 1;
                            }
                        }
                    }
                }

                offset += 1000;
            }

            dc.SexComm_Save(groupID, 1, 0, fother, funder18, ffrom18to21, ffrom21to24, ffrom24to27, ffrom27to30, ffrom30to35, ffrom35to45, fover45);
            dc.SexComm_Save(groupID, 1, 1, mother, munder18, mfrom18to21, mfrom21to24, mfrom24to27, mfrom27to30, mfrom30to35, mfrom35to45, mover45);
            dc.SexComm_Save(groupID, 1, 2, uother, uunder18, ufrom18to21, ufrom21to24, ufrom24to27, ufrom27to30, ufrom30to35, ufrom35to45, uover45);
        }
        #endregion

        #region ok_UpdateComm
        public void OK_UpdateComm() {
            wsGroups groups = getGroups(false, "ok");

            if (groups.Count > 0) {
                foreach (var gr in groups) {
                    long groupID = gr.groupID;
                    if (groupID == 0) {
                        groupID = ok_GetGroupID(gr.link);
                    }
                    ok_SetComm(gr);
                    Thread.Sleep(1000);
                }
            }
        }
        #endregion

        #region ok_SetComm
        private static long ok_SetComm(wsGroup group) {
            if (group.groupID == 0) {
                group.groupID = ok_GetGroupID(group.link);
            }
            string name = "";
            string screenName = "";
            string photo_id = "";
            string photoLink = "";
            string photoLinkBig = "";
            int members_count = 0;

            string method = "group.getInfo";
            string fields = "uid,name,shortname,photo_id,members_count";

            string sig;
            HubDataClassesDataContext dc = new HubDataClassesDataContext();

            using (HttpRequest net = new HttpRequest()) {
                net.CharacterSet = Encoding.GetEncoding("utf-8");

                string sigSource = string.Format("application_key={0}fields={1}method={2}session_key={3}uids={4}{5}"
                    , ok_application_key
                    , fields
                    , method
                    , ok_secret_session_key
                    , group.groupID
                    , ok_secret_session_key
                    );

                using (MD5 md5Hash = MD5.Create()) {
                    sig = getMd5Hash(md5Hash, sigSource);
                }

                string sourceuri = string.Format("https://api.ok.ru/fb.do?application_key={0}&sig={1}&session_key={2}&uids={3}&fields={4}&method={5}&access_token={6}"
                    , ok_application_key
                    , sig
                    , ok_secret_session_key
                    , group.groupID
                    , fields
                    , method
                    , ok_access_token
                    );

                string source = net.Get(sourceuri).ToString();
                JArray a = JArray.Parse(source);
                foreach (JObject o in a.Children<JObject>()) {
                    foreach (JProperty p in o.Properties()) {
                        if (p.Name == "name") {
                            name = (string)p.Value;
                        } else if (p.Name == "photo_id") {
                            photo_id = (string)p.Value;
                        } else if (p.Name == "members_count") {
                            members_count = (int)p.Value;
                        } else if (p.Name == "shortname") {
                            screenName = (string)p.Value;
                        }
                    }
                }
            }

            photoLink = string.Format("http://groupava2.odnoklassniki.ru/getImage?photoId={0}&photoType=2", photo_id);
            photoLinkBig = string.Format("http://groupava2.odnoklassniki.ru/getImage?photoId={0}&photoType=1", photo_id);

            dc.Comm_Set(group.link, group.groupID, name, photoLink, photoLinkBig, "ok", members_count, screenName);

            return group.groupID;
        }
        #endregion

        #region ok_GetStatTopics
        private static void ok_GetStatTopics(DateTime dayDate, long groupID) {
            string method = "group.getStatTopics";
            string fields = "renderings,reach,engagement,feedback,reach_own,reach_earned,renderings_own,renderings_earned,content_opens,feedback_total,likes,comments,reshares,video_plays,music_plays,link_clicks,negatives,hides_from_feed,complaints"; //created_ms
            string sig;

            DateTime start_time = dayDate.Date;
            DateTime end_time = start_time.AddDays(1);

            long uxStart_time = get_UnixTime(start_time);
            long uxEnd_time = get_UnixTime(end_time);

            HubDataClassesDataContext dc = new HubDataClassesDataContext();

            try {
                using (HttpRequest net = new HttpRequest()) {
                    net.CharacterSet = Encoding.GetEncoding("utf-8");

                    string sigSource = string.Format("application_key={0}count=50end_time={1}fields={2}format=XMLgid={3}method={4}session_key={5}start_time={6}{7}"
                        , ok_application_key
                        , uxEnd_time
                        , fields
                        , groupID
                        , method
                        , ok_secret_session_key
                        , uxStart_time
                        , ok_secret_session_key
                        );

                    using (MD5 md5Hash = MD5.Create()) {
                        sig = getMd5Hash(md5Hash, sigSource);
                    }

                    string sourceuri = string.Format("https://api.ok.ru/fb.do?application_key={0}&sig={1}&session_key={2}&format=XML&gid={3}&fields={4}&method={5}&start_time={6}&end_time={7}&count=50&access_token={8}"
                        , ok_application_key
                        , sig
                        , ok_secret_session_key
                        , groupID
                        , fields
                        , method
                        , uxStart_time
                        , uxEnd_time
                        , ok_access_token
                        );

                    string source = net.Get(sourceuri).ToString();
                    dc.StaCommOKTopics_Save(groupID, start_time, source);
                }
            } catch (Exception e) {
                string exInnerExceptionMessage = "";
                if (e.InnerException != null) {
                    exInnerExceptionMessage = e.InnerException.Message;
                }

                string note = count_try_ok_GetStatTopics == 0 ? "" : string.Format("Trying a {0} time", count_try_ok_GetStatTopics);
                dc.Exception_Save("ok_GetStatTopics", note, e.Message, exInnerExceptionMessage, e.HelpLink, e.HResult, e.Source, e.StackTrace);

                if (count_try_ok_GetStatTopics < 3) {
                    Thread.Sleep(1500);
                    ok_GetStatTopics(dayDate, groupID);
                    count_try_ok_GetStatTopics++;
                }
            }
        }
        #endregion

        #region ok_GetStatTrends
        private static void ok_GetStatTrends(DateTime dayDate, long groupID) {
            string method = "group.getStatTrends";
            string fields = "reach,engagement,feedback,members_count,new_members,new_members_target,left_members,members_diff,reach_own,reach_earned,reach_mob,reach_web,reach_mobweb,renderings,page_visits,content_opens,likes,comments,reshares,votes,link_clicks,video_plays,music_plays,topic_opens,photo_opens,negatives,hides_from_feed,complaints";
            string sig;

            DateTime start_time = dayDate.Date;
            DateTime end_time = start_time.AddDays(1);

            long uxStart_time = get_UnixTime(start_time);
            long uxEnd_time = get_UnixTime(end_time);

            HubDataClassesDataContext dc = new HubDataClassesDataContext();

            string sigSource;
            string sourceuri;

            try {
                using (HttpRequest net = new HttpRequest()) {
                    net.CharacterSet = Encoding.GetEncoding("utf-8");
                    sigSource = string.Format("application_key={0}end_time={1}fields={2}format=XMLgid={3}method={4}session_key={5}start_time={6}{7}"
                       , ok_application_key
                       , uxEnd_time
                       , fields
                       , groupID
                       , method
                       , ok_secret_session_key
                       , uxStart_time
                       , ok_secret_session_key
                       );

                    using (MD5 md5Hash = MD5.Create()) {
                        sig = getMd5Hash(md5Hash, sigSource);
                    }

                    sourceuri = string.Format("https://api.ok.ru/fb.do?application_key={0}&sig={1}&session_key={2}&format=XML&gid={3}&fields={4}&method={5}&start_time={6}&end_time={7}&access_token={8}"
                        , ok_application_key
                        , sig
                        , ok_secret_session_key
                        , groupID
                        , fields
                        , method
                        , uxStart_time
                        , uxEnd_time
                        , ok_access_token
                        );
                    //  }
                    string source = net.Get(sourceuri).ToString();
                    dc.StaCommOKTrends_Save(groupID, start_time, source);
                }
            } catch (Exception e) {
                string exInnerExceptionMessage = "";
                if (e.InnerException != null) {
                    exInnerExceptionMessage = e.InnerException.Message;
                }

                string note = count_try_ok_GetStatTopics == 0 ? "" : string.Format("Trying a {0} time", count_try_ok_GetStatTopics);
                dc.Exception_Save("ok_GetStatTrends", note, e.Message, exInnerExceptionMessage, e.HelpLink, e.HResult, e.Source, e.StackTrace);

                if (count_try_ok_GetStatTopics < 3) {
                    Thread.Sleep(1500);
                    ok_GetStatTopics(dayDate, groupID);
                    count_try_ok_GetStatTopics++;
                } else {
                    count_try_ok_GetStatTopics = 0;
                }
            }
        }
        #endregion

        #region UnixTime
        private static long get_UnixTime(DateTime datetime) {
            DateTimeOffset dto = new DateTimeOffset(datetime.Year, datetime.Month, datetime.Day, 0, 0, 0, TimeSpan.Zero);
            return dto.ToUnixTimeMilliseconds();
        }
        private static long ConvertToUnixTimestamp(DateTime date) {
            DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            TimeSpan diff = date - origin;
            return (long)Math.Floor(diff.TotalSeconds);
        }
        #endregion

        #region ok_GetGroupID
        private static long ok_GetGroupID(string link) {
            link = string.Format("https://ok.ru/{0}", link);
            long groupID = 0;
            string method = "url.getInfo";
            string sig;

            HubDataClassesDataContext dc = new HubDataClassesDataContext();

            using (HttpRequest net = new HttpRequest()) {
                net.CharacterSet = Encoding.GetEncoding("utf-8");
                string sigSource = string.Format("application_key={0}method={1}session_key={2}url={3}{4}"
                    , ok_application_key
                    , method
                    , ok_secret_session_key
                    , link
                    , ok_secret_session_key
                    );

                using (MD5 md5Hash = MD5.Create()) {
                    sig = getMd5Hash(md5Hash, sigSource);
                }

                string sourceuri = string.Format("https://api.ok.ru/fb.do?application_key={0}&sig={1}&session_key={2}&url={3}&method={4}&access_token={5}"
                    , ok_application_key
                    , sig
                    , ok_secret_session_key
                    , link
                    , method
                    , ok_access_token
                    );

                try {
                    string source = net.Get(sourceuri).ToString();
                    dynamic data = JObject.Parse(source);
                    groupID = data.objectId;
                } catch (Exception e) {
                    string exInnerExceptionMessage = "";
                    if (e.InnerException != null) {
                        exInnerExceptionMessage = e.InnerException.Message;
                    }

                    string note = count_try_ok_GetGroupID == 0 ? "" : string.Format("Trying a {0} time", count_try_ok_GetGroupID);
                    dc.Exception_Save("ok_GetGroupID", note, e.Message, exInnerExceptionMessage, e.HelpLink, e.HResult, e.Source, e.StackTrace);

                    if (count_try_ok_GetGroupID < 3) {
                        Thread.Sleep(1500);
                        groupID = ok_GetGroupID(link);
                        count_try_ok_GetGroupID++;
                    } else {
                        count_try_ok_GetGroupID = 0;
                    }
                }
            }

            return groupID;
        }
        #endregion

        #region vk_GetCountMembers
        private static int vk_GetCountMembers(VkApi api, long groupId) {
            int members;
            GroupsGetMembersParams prms = new GroupsGetMembersParams();
            prms.Count = 0;
            prms.GroupId = groupId.ToString();
            prms.Offset = 0;

            api.Groups.GetMembers(out members, prms);
            return members;
        }
        #endregion

        #region get_vklog
        private static void get_vklog(int ix, out string login, out string password) {
            login = "89157232003";
            password = "press1221";
            switch (ix) {
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
                    password = "non1221";
                    break;
                default:
                    login = "89652562584";
                    break;
            }
        }
        #endregion

        #region vk_Authorize
        private static VkApi vk_Authorize(int thread) {
            HubDataClassesDataContext dc = new HubDataClassesDataContext();

            Random rnd = new Random();

            string login = "";
            string password = "";
            get_vklog(thread, out login, out password);
            ulong appId = 5391843; // указываем id приложения

            Settings settings = Settings.All; // уровень доступа к данным

            VkApi api = new VkApi();
            try {
                api.Authorize(new ApiAuthParams {
                    ApplicationId = appId,
                    Login = login,
                    Password = password,
                    Settings = settings
                }); // авторизуемся

                api.Stats.TrackVisitor();
            } catch (TooManyRequestsException) {
                Thread.Sleep(1500);
                int index = rnd.Next(1, 8);
                api = vk_Authorize(index);
            } catch (Exception e) {
                string exInnerExceptionMessage = "";
                if (e.InnerException != null) {
                    exInnerExceptionMessage = e.InnerException.Message;
                }
                dc.Exception_Save("vk_Authorize", string.Format("login: {0}, password: {1}", login, password), e.Message, exInnerExceptionMessage, e.HelpLink, e.HResult, e.Source, e.StackTrace);

                int index = rnd.Next(1, 8);
                api = vk_Authorize(index);
            }

            return api;
        }
        #endregion

        #region vk_Wall_Get
        private WallGetObject vk_Wall_Get(VkApi api, long groupId, ulong offset) {
            WallGetObject res;

            try {
                res = api.Wall.Get(new WallGetParams {
                    OwnerId = 0 - groupId,
                    Offset = offset,
                    Count = 100,
                    Filter = Owner,
                    Extended = false
                });
            } catch (TooManyRequestsException) {
                Thread.Sleep(1000);
                res = vk_Wall_Get(api, groupId, offset);
            }

            return res;
        }
        #endregion

        #region getGroups
        private static wsGroups getGroups(bool isNewComm, string areaCode) {
            var results = new wsGroups();
            HubDataClassesDataContext dc = new HubDataClassesDataContext();

            foreach (Comm_ReadForStaResult comm in dc.Comm_ReadForSta(isNewComm, areaCode)) {
                results.Add(new wsGroup() {
                    groupID = comm.groupID ?? 0,
                    link = comm.link
                });
            };

            return results;
        }
        #endregion

        #region getGroupsVkTop
        private static wsGroups getGroupsVkTop(int topCount) {
            var results = new wsGroups();
            HubDataClassesDataContext dc = new HubDataClassesDataContext();

            foreach (Comm_ReadForStaVKGraphResult comm in dc.Comm_ReadForStaVKGraph(topCount)) {
                results.Add(new wsGroup() {
                    groupID = comm.groupID ?? 0,
                    link = comm.link
                });
            };

            return results;
        }
        #endregion

        #region getDayOfWeek
        private static int getDayOfWeek() {
            int dow = (int)DateTime.Today.DayOfWeek;
            switch (dow) {
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
        private static string getMd5Hash(MD5 md5Hash, string input) {

            // Convert the input string to a byte array and compute the hash.
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++) {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }
        #endregion

        #region public
        public WallFilter Owner { get; private set; }
        private const string ok_access_token = "tkn14AyTiUGxffCnkD3xuRBv4iY2Rp4vOoHwXYbKFgufADQOR3X6XuxnSclcNflQ6SulX2";
        private const string ok_secret_session_key = "1cf6c032bd25f2f6952d50f082b51045";
        private const string ok_aaplication_id = "1247034880";
        private const string ok_application_key = "CBAPDFFLEBABABABA";
        private const string ok_application_key_secret = "A050BBA2FE2FCA7DFBFB945A";
        private static int count_try_ok_GetStatTopics = 0;
        private static int count_try_ok_GetGroupID = 0;
        #endregion

        #region Send_SMS
        public void Send_SMS(string message, string phone) {
            HubDataClassesDataContext dc = new HubDataClassesDataContext();

            try {
                string http = string.Format("{0}sms.ru/sms/send?api_id={1}&from=Comm&to={2}&text={3}", "http://", "8B4D21F6-33D2-DBD4-8425-34631CD434BE", phone, message);
                var request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(http);
                var response = (System.Net.HttpWebResponse)request.GetResponse();
                var responseString = new System.IO.StreamReader(response.GetResponseStream()).ReadToEnd();
            } catch (Exception e) {
                string exInnerExceptionMessage = "";
                if (e.InnerException != null) {
                    exInnerExceptionMessage = e.InnerException.Message;
                }
                dc.Exception_Save("Send_SMS", string.Format("phone: {0}, message: {1}", phone, message), e.Message, exInnerExceptionMessage, e.HelpLink, e.HResult, e.Source, e.StackTrace);
            }
        }
        #endregion
    }
}