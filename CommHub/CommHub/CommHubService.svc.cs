using CommHub.wsClasses;
using System;
using System.IO;
using System.Net;

namespace CommHub
{
    public class CommHubService : IService
    {
        #region AdminComm
        public wsResponse<AdminComm_Del_Resp> AdminComm_Del(wsRequest<InstanceID> req)
        {
            var funcName = "AdminComm_Del";
            var errCode = 0;
            var errorText = "";

            var results = new wsResponse<AdminComm_Del_Resp>();
            var resp = new AdminComm_Del_Resp();
            var dc = new DataHubDataContext();
            long id = 0;
            long ownerHubID = 0;

            if (req.Params != null)
            {
                id = req.Params.id;
            }
            else
            {
                errCode = 200;
                errorText = Tools.GetErrorTextByCode(errCode);
                Tools.ErrorLog_Save(req, "", funcName, errorText);

                results.ErrText = string.Format("{0}\n{1}", funcName, errorText);
                results.ErrCode = errCode;
                return results;
            }

            try
            {
                foreach (GetOwnerHubIDResult own in dc.GetOwnerHubID(req.Session))
                {
                    ownerHubID = own.ownerHubID;
                }

                dc.AdminComm_Del(id, ownerHubID);

                resp.isSuccessful = true;
                results.Data = resp;
            }
            catch (Exception e)
            {
                errCode = 101;
                string param = string.Format("id: {0}, ownerHubID: {1}", id, ownerHubID);
                Tools.ErrorLog_Save(req, param, funcName, e.Message);
                results.ErrCode = errCode;
                results.ErrText = string.Format("{0}\n{1}\n{2}", funcName, Tools.GetErrorTextByCode(errCode), e.Message);

                resp.isSuccessful = false;
                results.Data = resp;
            }
            return results;
        }
        public wsResponse<AdminComm_ReadDict_Resp> AdminComm_ReadDict(wsRequest req)
        {
            var funcName = "AdminComm_ReadDict";
            var errCode = 0;
            var errorText = "";

            var results = new wsResponse<AdminComm_ReadDict_Resp>();
            var resp = new AdminComm_ReadDict_Resp();
            var dc = new DataHubDataContext();
            long ownerHubID = 0;

            if (req == null)
            {
                errCode = 200;
                errorText = Tools.GetErrorTextByCode(errCode);
                Tools.ErrorLog_Save(req, "", funcName, errorText);

                results.ErrText = string.Format("{0}\n{1}", funcName, errorText);
                results.ErrCode = errCode;
                return results;
            }

            try
            {
                foreach (GetOwnerHubIDResult own in dc.GetOwnerHubID(req.Session))
                {
                    ownerHubID = own.ownerHubID;
                }

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
                results.ErrCode = errCode;
                return results;
            }

            try
            {
                foreach (GetOwnerHubIDResult own in dc.GetOwnerHubID(req.Session))
                {
                    ownerHubID = own.ownerHubID;
                }

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

        #region Comm
        public wsResponse Comm_Del(wsRequest<InstanceID> req)
        {
            var funcName = "Comm_Del";
            var errCode = 0;
            var errorText = "";

            var results = new wsResponse();
            var dc = new DataHubDataContext();
            long ownerHubID = 0;
            long id;

            if (req != null)
            {
                id = req.Params.id;
            }
            else
            {
                errCode = 200;
                errorText = Tools.GetErrorTextByCode(errCode);
                Tools.ErrorLog_Save(req, "", funcName, errorText);

                results.ErrText = string.Format("{0}\n{1}", funcName, errorText);
                results.ErrCode = errCode;
                return results;
            }

            try
            {
                foreach (GetOwnerHubIDResult own in dc.GetOwnerHubID(req.Session))
                {
                    ownerHubID = own.ownerHubID;
                }

                dc.Comm_Del(id, ownerHubID);

            }
            catch (Exception e)
            {
                errCode = 101;
                string param = string.Format("id: {0}, ownerHubID: {0}", id, ownerHubID);
                Tools.ErrorLog_Save(req, param, funcName, e.Message);

                results.ErrCode = errCode;
                results.ErrText = string.Format("{0}\n{1}\n{2}", funcName, Tools.GetErrorTextByCode(errCode), e.Message);
            }

            return results;
        }
        public wsResponse<Comm_ReadDict_Resp> Comm_ReadDict(wsRequest req)
        {
            var funcName = "Comm_ReadDict";
            var errCode = 0;
            var errorText = "";

            var results = new wsResponse<Comm_ReadDict_Resp>();
            var resp = new Comm_ReadDict_Resp();
            var dc = new DataHubDataContext();
            long ownerHubID = 0;

            if (req == null)
            {
                errCode = 200;
                errorText = Tools.GetErrorTextByCode(errCode);
                Tools.ErrorLog_Save(req, "", funcName, errorText);

                results.ErrText = string.Format("{0}\n{1}", funcName, errorText);
                results.ErrCode = errCode;
                return results;
            }

            try
            {
                foreach (GetOwnerHubIDResult own in dc.GetOwnerHubID(req.Session))
                {
                    ownerHubID = own.ownerHubID;
                }

                foreach (Comm_ReadDictResult itm in dc.Comm_ReadDict(ownerHubID))
                {
                    resp.Add(new wsComm_Extended()
                    {
                        id = itm.id,
                        name = itm.name,
                        photoLink = itm.photoLink,
                        adminCommID = itm.adminCommID ?? 0,
                        adminCommID_firstName = itm.adminCommID_firstName,
                        adminCommID_lastName = itm.adminCommID_lastName,
                        adminCommID_linkFB = itm.adminCommID_linkFB,
                        adminCommID_phone = itm.adminCommID_phone,

                        areaCommID = itm.areaCommID ?? 0,
                        areaCommID_name = itm.areaCommID_name,

                        ownerHubID = itm.ownerHubID ?? 0,
                        ownerHubID_firstName = itm.ownerHubID_firstName,
                        ownerHubID_lastName = itm.ownerHubID_lastName,
                        ownerHubID_linkFB = itm.ownerHubID_linkFB,

                        subjectCommID = itm.subjectCommID ?? 0,
                        subjectCommID_name = itm.subjectCommID_name
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

        public wsResponse Comm_Save(wsRequest<Comm_Save_Req> req)
        {
            var funcName = "Comm_Save";
            var errCode = 0;
            var errorText = "";

            var results = new wsResponse();
            var dc = new DataHubDataContext();

            long id = 0;
            long ownerHubID = 0;
            string name;
            long adminCommID;
            int areaCommID = 0;
            long subjectCommID;
            string link;
            long groupID;

            if (req != null)
            {
                wsComm comm = req.Params.comm;

                name = comm.name;
                adminCommID = comm.adminCommID;
                areaCommID = comm.areaCommID;
                subjectCommID = comm.subjectCommID;
                link = comm.link;
                groupID = comm.groupID;
            }
            else
            {
                errCode = 200;
                errorText = Tools.GetErrorTextByCode(errCode);
                Tools.ErrorLog_Save(req, "", funcName, errorText);

                results.ErrText = string.Format("{0}\n{1}", funcName, errorText);
                results.ErrCode = errCode;
                return results;
            }

            try
            {
                foreach (GetOwnerHubIDResult own in dc.GetOwnerHubID(req.Session))
                {
                    ownerHubID = own.ownerHubID;
                }

                dc.Comm_Save(id, ownerHubID, subjectCommID, areaCommID, name, adminCommID, link, groupID);

            }
            catch (Exception e)
            {
                errCode = 101;
                string param = string.Format("id: {0}, ownerHubID: {1}, subjectCommID: {2}, areaCommID: {3}, name: {4}, adminCommID: {5}, link: {6}, groupID: {7}", id, ownerHubID, subjectCommID, areaCommID, name, adminCommID, link, groupID);
                Tools.ErrorLog_Save(req, param, funcName, e.Message);

                results.ErrCode = errCode;
                results.ErrText = string.Format("{0}\n{1}\n{2}", funcName, Tools.GetErrorTextByCode(errCode), e.Message);
            }

            return results;
        }
        #endregion

        #region SubjectComm
        public wsResponse<SubjectComm_ReadDict_Resp> SubjectComm_ReadDict(wsRequest req)
        {
            var funcName = "SubjectComm_ReadDict";
            var errCode = 0;
            var errorText = "";

            var results = new wsResponse<SubjectComm_ReadDict_Resp>();
            var resp = new SubjectComm_ReadDict_Resp();
            var dc = new DataHubDataContext();
            long ownerHubID = 0;

            if (req == null)
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
                foreach (GetOwnerHubIDResult own in dc.GetOwnerHubID(req.Session))
                {
                    ownerHubID = own.ownerHubID;
                }

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

        public wsResponse<SubjectComm_Save_Resp> SubjectComm_Save(wsRequest<SubjectComm_Save_Req> req)
        {
            var funcName = "SubjectComm_Save";
            var errCode = 0;
            var errorText = "";

            var results = new wsResponse<SubjectComm_Save_Resp>();
            var resp = new SubjectComm_Save_Resp();
            var dc = new DataHubDataContext();
            long id = 0;
            long ownerHubID = 0;
            string name = "";

            if (req.Params != null)
            {
                id = req.Params.id;
                name = req.Params.name;
            }
            else
            {
                errCode = 200;
                errorText = Tools.GetErrorTextByCode(errCode);
                Tools.ErrorLog_Save(req, "", funcName, errorText);

                results.ErrText = string.Format("{0}\n{1}", funcName, errorText);
                results.ErrCode = errCode;
                return results;
            }

            try
            {
                foreach (GetOwnerHubIDResult own in dc.GetOwnerHubID(req.Session))
                {
                    ownerHubID = own.ownerHubID;
                }

                foreach (SubjectComm_SaveResult itm in dc.SubjectComm_Save(id, ownerHubID, name))
                {
                    resp.id = itm.id;
                    resp.name = itm.name;
                }

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

        public wsResponse SubjectComm_Del(wsRequest<InstanceID> req)
        {
            var funcName = "SubjectComm_Del";
            var errCode = 0;
            var errorText = "";

            var results = new wsResponse();
            var dc = new DataHubDataContext();
            long id = 0;
            long ownerHubID = 0;

            if (req.Params != null)
            {
                id = req.Params.id;
            }
            else
            {
                errCode = 200;
                errorText = Tools.GetErrorTextByCode(errCode);
                Tools.ErrorLog_Save(req, "", funcName, errorText);

                results.ErrText = string.Format("{0}\n{1}", funcName, errorText);
                results.ErrCode = errCode;
                return results;
            }

            try
            {
                foreach (GetOwnerHubIDResult own in dc.GetOwnerHubID(req.Session))
                {
                    ownerHubID = own.ownerHubID;
                }

                dc.SubjectComm_Del(id, ownerHubID);
            }
            catch (Exception e)
            {
                errCode = 101;
                string param = string.Format("id: {0}, ownerHubID: {1}", id, ownerHubID);
                Tools.ErrorLog_Save(req, param, funcName, e.Message);

                results.ErrCode = errCode;
                results.ErrText = string.Format("{0}\n{1}\n{2}", funcName, Tools.GetErrorTextByCode(errCode), e.Message);
            }

            return results;
        }
        #endregion

        #region StaComm 
        public wsResponse<StaCommVKDaily_Report_Resp> StaCommVKDaily_ReportDay(wsRequest req)
        {
            var funcName = "StaCommVKDaily_ReportDay";
            var errCode = 0;
            var errorText = "";

            var results = new wsResponse<StaCommVKDaily_Report_Resp>();
            var resp = new StaCommVKDaily_Report_Resp();
            var dc = new DataHubDataContext();
            long ownerHubID = 0;

            if (req == null)
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
                foreach (GetOwnerHubIDResult own in dc.GetOwnerHubID(req.Session))
                {
                    ownerHubID = own.ownerHubID;
                }
                
                foreach (StaCommVKDaily_ReportDayResult itm in dc.StaCommVKDaily_ReportDay(ownerHubID))
                {
                    resp.Add(new wsStaComm()
                    {
                        comm_id = itm.comm_id,
                        comm_name = itm.comm_name,
                        comm_photoLink = itm.comm_photoLink,
                        comm_groupID = itm.comm_groupID ?? 0,

                        subjectComm_name = itm.subjectComm_name,
                        areaComm_code = itm.areaComm_code,

                        adminComm_fullName = itm.adminComm_fullName,
                        adminComm_linkFB = itm.adminComm_linkFB,

                        members = itm.members,
                        membersNew = itm.membersNew,
                        membersNewPercent = itm.membersNewPercent,

                        subscribed = itm.subscribed,
                        subscribedNew = itm.subscribedNew,
                        subscribedNewPercent = itm.subscribedNewPercent,

                        unsubscribed = itm.unsubscribed,
                        unsubscribedNew = itm.unsubscribedNew,
                        unsubscribedNewPercent = itm.unsubscribedNewPercent,

                        visitors = itm.visitors,
                        visitorsNew = itm.visitorsNew,
                        visitorsNewPercent = itm.visitorsNewPercent,

                        views = itm.views,
                        viewsNew = itm.viewsNew,
                        viewsNewPercent = itm.viewsNewPercent,

                        reach = itm.reach,
                        reachNew = itm.reachNew,
                        reachNewPercent = itm.reachNewPercent,

                        reachSubscribers = itm.reachSubscribers,
                        reachSubscribersNew = itm.reachSubscribersNew,
                        reachSubscribersNewPercent = itm.reachSubscribersNewPercent,

                        postCount = itm.postCount,
                        postCountNew = itm.postCountNew,
                        postCountNewPercent = itm.postCountNewPercent,

                        likes = itm.likes,
                        likesNew = itm.likesNew,
                        likesNewPercent = itm.likesNewPercent,

                        comments = itm.comments,
                        commentsNew = itm.commentsNew,
                        commentsNewPercent = itm.commentsNewPercent,

                        reposts = itm.reposts,
                        repostsNew = itm.repostsNew,
                        repostsNewPercent = itm.repostsNewPercent,

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
                results.ErrText = string.Format("funcName: {0}, errCode: {1}, Message: {2}", funcName, Tools.GetErrorTextByCode(errCode), e.Message);
            }

            return results;
        }
        #endregion

        #region Session

        public wsResponse<SessionReq_Save_Resp> SessionReq_Save(SessionReq_Save_Req req)
        {
            var results = new wsResponse<SessionReq_Save_Resp>();
            var resp = new SessionReq_Save_Resp();
            var dc = new DataHubDataContext();
            string did;
            string phone;

            Random generator = new Random();
            string code = generator.Next(0, 1000).ToString("D4");

            if (req != null)
            {
                did = req.did;
                phone = req.phone;
            }
            else
            {
                results.ErrCode = 200;
                results.ErrText = "SessionReq_Save: No Params";
                return results;
            }

            /*try
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
                results.ErrText = string.Format("Send sms {0}, error: {1}", phone, e.Message);
            }
            */
            try
            {
                foreach (SessionReq_SaveResult res in dc.SessionReq_Save(did, phone))
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
                results.ErrText = string.Format("SessionReq_Save {0}, error: {1}", phone, e.Message);
            }

            return results;
        }

        public wsResponse<Session_Save_Resp> Session_Save(Session_Save_Req req)
        {
            var results = new wsResponse<Session_Save_Resp>();
            var resp = new Session_Save_Resp();
            var dc = new DataHubDataContext();
            long sessionReqID = 0;
            string did = "";

            if (req != null)
            {
                sessionReqID = req.sessionReqID;
                did = req.did;
            }
            else
            {
                results.ErrCode = 200;
                results.ErrText = "Session_Save: No Params";
                return results;
            }

            try
            {
                foreach (Session_SaveResult res in dc.Session_Save(sessionReqID, did))
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
                results.ErrText = string.Format("Session_Save {0}: {1}", sessionReqID, e.Message);
            }

            return results;
        }
        #endregion
    }
}
