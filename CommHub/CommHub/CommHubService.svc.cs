using CommHub.wsClasses;
using System;

namespace CommHub
{
    public class CommHubService : IService
    {

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

        #region Comm
        public wsResponse<Comm_ReadDict_Resp> Comm_ReadDict(wsRequest<Comm_ReadDict_Req> req)
        {
            var funcName = "Comm_ReadDict";
            var errCode = 0;
            var errorText = "";

            var results = new wsResponse<Comm_ReadDict_Resp>();
            var resp = new Comm_ReadDict_Resp();
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
                foreach (Comm_ReadDictResult itm in dc.Comm_ReadDict(ownerHubID))
                {
                    resp.Add(new wsComm()
                    {
                        id = itm.id,
                        name = itm.name,
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
                ownerHubID = req.Params.ownerHubID;
                name = req.Params.name;
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

    }
}
