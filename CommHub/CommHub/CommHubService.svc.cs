using CommHub.wsClasses;
using System;
using VkNet;
using VkNet.Enums.Filters;
using VkNet.Model.RequestParams;
using VkNet.Enums;
using VkNet.Model;
using System.Threading;

namespace CommHub
{
    public class CommHubService : IService
    {
        public WallFilter Owner { get; private set; }

        #region AdminComm
        public wsResponse<AdminComm_ReadDict_Resp> AdminComm_ReadDict(wsRequest<AdminComm_ReadDict_Req> req)
        {
            var funcName = "AdminComm_ReadDict";
            var errCode = 0;
            var errorText = "";

            var results = new wsResponse<AdminComm_ReadDict_Resp>();
            var resp = new AdminComm_ReadDict_Resp();
            var dc = new DataHubDataContext();
            long ownerHubID = 0;

            if (req.Params != null)
            {
                ownerHubID = req.Params.ownerHubID;
            }
            else
            {
                errCode = 200;
                errorText = Tools.GetErrorTextByCode(errCode);
                Tools.ErrorLog_Save(req, "", funcName, errorText);

                results.ErrText = string.Format("{0}\n{1}", funcName, errorText);
                results.ErrCode = -1;
                return results;
            }

            try
            {
                foreach (AdminComm_ReadDictResult itm in dc.AdminComm_ReadDict(ownerHubID))
                {
                    resp.Add(new wsAdminComm()
                    {
                        id = itm.id,
                        firstName = itm.firstName,
                        lastName = itm.lastName,
                        phone = itm.phone,
                        linkFB = itm.linkFB
                    });
                };

                results.Data = resp;
            }
            catch (Exception e)
            {
                errCode = 101;
                string param = string.Format("ownerHubID: {0}", ownerHubID);
                Tools.ErrorLog_Save(req, param, funcName, e.Message);

                results.ErrCode = errCode;
                results.ErrText = string.Format("{0}\n{1}\n{2}", funcName, Tools.GetErrorTextByCode(errCode), e.Message);
            }

            return results;
        }

        public wsResponse<AdminComm_Save_Resp> AdminComm_Save(wsRequest<AdminComm_Save_Req> req)
        {
            var funcName = "AdminComm_Save";
            var errCode = 0;
            var errorText = "";

            var results = new wsResponse<AdminComm_Save_Resp>();
            var resp = new AdminComm_Save_Resp();
            var dc = new DataHubDataContext();

            long id = 0;
            long ownerHubID = 0;
            string firstName;
            string lastName;
            string phone;
            string linkFB;

            if (req.Params != null)
            {
                id = req.Params.id ?? 0;
                ownerHubID = req.Params.ownerHubID;
                firstName = req.Params.firstName;
                lastName = req.Params.lastName;
                phone = req.Params.phone;
                linkFB = req.Params.linkFB;
            }
            else
            {
                errCode = 200;
                errorText = Tools.GetErrorTextByCode(errCode);
                Tools.ErrorLog_Save(req, "", funcName, errorText);

                results.ErrText = string.Format("{0}\n{1}", funcName, errorText);
                results.ErrCode = -1;
                return results;
            }

            try
            {
                foreach (AdminComm_SaveResult itm in dc.AdminComm_Save(id, ownerHubID, firstName, lastName, phone, linkFB))
                {
                    resp.id = itm.id;
                    resp.firstName = itm.firstName;
                    resp.lastName = itm.lastName;
                    resp.phone = itm.phone;
                    resp.linkFB = itm.linkFB;

                };

                results.Data = resp;
            }
            catch (Exception e)
            {
                errCode = 101;
                string param = string.Format("id: {0}, ownerHubID: {1}, firstName: {2}, lastName: {3}, phone: {4}, linkFB: {5}", id, ownerHubID, firstName, lastName, phone, linkFB);
                Tools.ErrorLog_Save(req, param, funcName, e.Message);

                results.ErrCode = errCode;
                results.ErrText = string.Format("{0}\n{1}\n{2}", funcName, Tools.GetErrorTextByCode(errCode), e.Message);
            }

            return results;
        }

        #endregion

        #region SubjectComm
        public wsResponse<SubjectComm_ReadDict_Resp> SubjectComm_ReadDict(wsRequest<SubjectComm_ReadDict_Req> req)
        {
            var funcName = "SubjectComm_ReadDict";
            var errCode = 0;
            var errorText = "";

            var results = new wsResponse<SubjectComm_ReadDict_Resp>();
            var resp = new SubjectComm_ReadDict_Resp();
            var dc = new DataHubDataContext();
            long ownerHubID = 0;

            if (req.Params != null)
            {
                ownerHubID = req.Params.ownerHubID;
            }
            else
            {
                errCode = 200;
                errorText = Tools.GetErrorTextByCode(errCode);
                Tools.ErrorLog_Save(req, "", funcName, errorText);

                results.ErrText = string.Format("{0}\n{1}", funcName, errorText);
                results.ErrCode = -1;
                return results;
            }

            try
            {
                foreach (SubjectComm_ReadDictResult itm in dc.SubjectComm_ReadDict(ownerHubID))
                {
                    resp.Add(new wsSubjectComm()
                    {
                        id = itm.id,
                        name = itm.name
                    });
                };

                results.Data = resp;
            }
            catch (Exception e)
            {
                errCode = 101;
                string param = string.Format("ownerHubID: {0}", ownerHubID);
                Tools.ErrorLog_Save(req, param, funcName, e.Message);

                results.ErrCode = errCode;
                results.ErrText = string.Format("{0}\n{1}\n{2}", funcName, Tools.GetErrorTextByCode(errCode), e.Message);
            }

            return results;
        }
        #endregion

        #region VK_Stats

        public wsResponseSimple VK_Stats_Get(wsRequest<VK_Stats_Get_Req> req)
        {
            var funcName = "VK_Stats_Get";
            var errCode = 0;
            var errorText = "";

            var dc = new DataHubDataContext();

            long groupId = 0;
            DateTime dateFrom = DateTime.Today.AddDays(-1).Date;
            DateTime? dateTo = DateTime.Today.Date;

            string email = "89164913669"; // email для авторизации
            string password = "PressNon798520"; // пароль
            Settings settings = Settings.All; // уровень доступа к данным
            if (req.Params != null)
            {
                groupId = req.Params.groupId;
                dateFrom = req.Params.dateFrom;
                dateTo = req.Params.dateTo;
            }
            else
            {
                errCode = 200;
                errorText = Tools.GetErrorTextByCode(errCode);
                Tools.ErrorLog_Save(req, "", funcName, errorText);
            }

            var api = new VkApi();
            api.Authorize(new ApiAuthParams
            {
                ApplicationId = 5391843,
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

            while (offset < cnt)
            {
                respWall = api.Wall.Get(new WallGetParams
                {
                    OwnerId = 0 - groupId,
                    Offset = offset,
                    Count = 100,
                    Filter = Owner,
                    Extended = false
                });

                foreach (Post post in respWall.WallPosts)
                {
                    likes += post.Likes.Count;
                    comments += post.Comments.Count;
                    reposts += post.Reposts.Count;
                };

                offset += 100;
            }

            dc.StatsCommVK_Save(3, views, visitors, reach, reach_subscribers, subscribed, unsubscribed, likes, comments, reposts, countPost);
            wsResponseSimple resp = new wsResponseSimple();
            resp.ErrCode = 0;
            resp.ErrText = "No Error";
            return resp;
        }

        public void VK_Stats_GetNorm()
        {
            var dc = new DataHubDataContext();

            long groupId = 10639516; // http://vk.com/MDK
            DateTime dateFrom = DateTime.Today.AddDays(-1).Date;
            DateTime? dateTo = DateTime.Today.Date;
            ulong appId = 5391843; // указываем id приложения
            string email = "89164913669"; // email для авторизации
            string password = "PressNon798520"; // пароль
            Settings settings = Settings.All; // уровень доступа к данным

            var api = new VkApi();
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

            dc.StatsCommVK_Save(57, views, visitors, reach, reach_subscribers, subscribed, unsubscribed, likes, comments, reposts, countPost);
        }
        #endregion

        #region Session

        public wsResponse<SessionReq_Save_Resp> SessionReq_Save(wsRequest<SessionReq_Save_Req> req)
        {
            var results = new wsResponse<SessionReq_Save_Resp>();
            var resp = new SessionReq_Save_Resp();
            var dc = new DataHubDataContext();
            string DID = "";
            string Phone = "";

            Random generator = new Random();
            string code = generator.Next(0, 1000).ToString("D4");

            if (req.Params != null)
            {
                DID = req.DID;
                Phone = req.Params.Phone;
            }
            else
            {
                results.ErrCode = 200;
                results.ErrText = "SessionReq_Save: No Params";
                return results;
            }

            /*
            try
            {
                  string message = string.Format("Comm+code+confirm:+{0}", code);
                  string http = string.Format("{0}sms.ru/sms/send?api_id={1}&to={2}&text={3}", "http://", "8B4D21F6-33D2-DBD4-8425-34631CD434BE", Phone, message);
                  var request = (HttpWebRequest)WebRequest.Create(http);
                  var response = (HttpWebResponse)request.GetResponse();
                  var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
            }
            catch (Exception e)
            {
                results.ErrCode = 40;
                results.ErrText = string.Format("Send sms {0}, error: {1}", Phone, e.Message);
            }*/

            try
            {
                foreach (SessionReq_SaveResult res in dc.SessionReq_Save(DID, Phone))
                {
                    resp.id = res.id ?? 0;
                    resp.code = code;
                    resp.ownerHubID = res.ownerHubID;
                }
                results.Data = resp;
            }
            catch (Exception e)
            {
                results.ErrCode = 101;
                results.ErrText = string.Format("SessionReq_Save {0}, error: {1}", Phone, e.Message);
            }

            return results;
        }

        public wsResponse<Session_Save_Resp> Session_Save(wsRequest<Session_Save_Req> req)
        {
            var results = new wsResponse<Session_Save_Resp>();
            var resp = new Session_Save_Resp();
            var dc = new DataHubDataContext();
            long sessionReq_ID = 0;
            string dID = "";
            string Phone = "";

            if (req.Params != null)
            {
                sessionReq_ID = req.Params.SessionReq_ID;
                dID = req.DID;
            }
            else
            {
                results.ErrCode = 200;
                results.ErrText = "Session_Save: No Params";
                return results;
            }

            try
            {
                foreach (Session_SaveResult res in dc.Session_Save(sessionReq_ID, dID))
                {
                    resp.SessionID = res.sessionID;
                    resp.ownerHubID = res.ownerHubID ?? 0;
                    resp.IsNewMember = res.isNewMember ?? true;
                }
                results.Data = resp;
            }
            catch (Exception e)
            {
                results.ErrCode = 101;
                results.ErrText = string.Format("Session_Save {0}: {1}", Phone, e.Message);
            }

            return results;
        }

        #endregion


        public string RequestStatVk()
        {
            VK_Stats_GetNorm();
            return "No Error";
        }
    }
}
