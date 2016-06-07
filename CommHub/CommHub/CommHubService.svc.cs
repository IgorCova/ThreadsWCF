using CommHub.wsClasses;
using System;
using System.Globalization;
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

        #region OwnerHub
        public wsResponse<OwnerHub_Read_Resp> OwnerHub_Read(wsRequest req)
        {
            var funcName = "OwnerHub_Read";
            var errCode = 0;
            var errorText = "";

            var results = new wsResponse<OwnerHub_Read_Resp>();
            var resp = new OwnerHub_Read_Resp();
            var dc = new DataHubDataContext();

            long id = 0;

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
                    id = own.ownerHubID;
                }

                foreach (OwnerHub_ReadResult itm in dc.OwnerHub_Read(id))
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
                string param = string.Format("id: {0}", id);
                Tools.ErrorLog_Save(req, param, funcName, e.Message);

                results.ErrCode = errCode;
                results.ErrText = string.Format("{0}\n{1}\n{2}", funcName, Tools.GetErrorTextByCode(errCode), e.Message);
            }
            return results;
        }
        public wsResponse OwnerHub_Save(wsRequest<OwnerHub_Save_Req> req)
        {
            var funcName = "OwnerHub_Save";
            var errCode = 0;
            var errorText = "";

            var results = new wsResponse();
            var dc = new DataHubDataContext();

            long id = 0;
            string firstName;
            string lastName;
            string phone;
            string linkFB;

            if (req.Params != null)
            {
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
                    id = own.ownerHubID;
                }

                dc.OwnerHub_Save(id, firstName, lastName, phone, linkFB);

            }
            catch (Exception e)
            {
                errCode = 101;
                string param = string.Format("id: {0}, firstName: {1}, lastName: {2}, phone: {3}, linkFB: {4}", id, firstName, lastName, phone, linkFB);
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
                        groupID = itm.groupID ?? 0,
                        link = itm.link,
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
            long subjectCommID;
            string link;

            if (req != null)
            {
                wsComm comm = req.Params.comm;
                id = comm.id;
                name = comm.name;
                adminCommID = comm.adminCommID;
                subjectCommID = comm.subjectCommID;
                link = comm.link;
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

                int areaID = 1;
                areaID = (link.Contains("vk.com")) ? 1 : 2;

                dc.Comm_Save(id, ownerHubID, subjectCommID, name, adminCommID, link, areaID);
            }
            catch (Exception e)
            {
                errCode = 101;
                string param = string.Format("id: {0}, ownerHubID: {1}, subjectCommID: {2}, name: {4}, adminCommID: {5}, link: {6}", id, ownerHubID, subjectCommID, name, adminCommID, link);
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
        public wsResponse<StaCommVK_Report_Resp> StaCommVKDaily_Report(wsRequest<StaCommDaily_Report_Req> req)
        {
            var funcName = "StaCommVKDaily_Report";
            var errCode = 0;
            var errorText = "";

            wsResponse<StaCommVK_Report_Resp> results = new wsResponse<StaCommVK_Report_Resp>();
            StaCommVK_Report_Resp resp = new StaCommVK_Report_Resp();
            DataHubDataContext dc = new DataHubDataContext();
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

                foreach (StaCommVKDaily_ReportResult itm in dc.StaCommVKDaily_Report(ownerHubID, req.Params.isPast))
                {
                    resp.Add(new wsSta()
                    {
                        comm_id = itm.comm_id,
                        comm_name = itm.comm_name,
                        comm_photoLink = itm.comm_photoLink,
                        comm_photoLinkBig = itm.comm_photoLinkBig,
                        comm_groupID = itm.comm_groupID ?? 0,

                        subjectComm_name = itm.subjectComm_name,
                        areaComm_code = itm.areaComm_code,

                        adminComm_fullName = itm.adminComm_fullName,
                        adminComm_linkFB = itm.adminComm_linkFB,

                        members = itm.members,
                        membersNew = itm.membersNew,
                        membersDifPercent = itm.membersDifPercent ?? 0,

                        increaseNew = itm.increaseNew,
                        increaseDifPercent = itm.increaseDifPercent ?? 0,

                        subscribed = itm.subscribed,
                        subscribedNew = itm.subscribedNew,
                        subscribedDifPercent = itm.subscribedDifPercent ?? 0,

                        unsubscribed = itm.unsubscribed,
                        unsubscribedNew = itm.unsubscribedNew,
                        unsubscribedDifPercent = itm.unsubscribedDifPercent ?? 0,

                        visitors = itm.visitors,
                        visitorsNew = itm.visitorsNew,
                        visitorsDifPercent = itm.visitorsDifPercent ?? 0,

                        views = itm.views,
                        viewsNew = itm.viewsNew,
                        viewsDifPercent = itm.viewsDifPercent ?? 0,

                        reach = itm.reach,
                        reachNew = itm.reachNew,
                        reachDifPercent = itm.reachDifPercent ?? 0,

                        reachSubscribers = itm.reachSubscribers,
                        reachSubscribersNew = itm.reachSubscribersNew,
                        reachSubscribersDifPercent = itm.reachSubscribersDifPercent ?? 0,

                        postCount = itm.postCount,
                        postCountNew = itm.postCountNew,
                        postCountDifPercent = itm.postCountDifPercent ?? 0,

                        likes = itm.likes,
                        likesNew = itm.likesNew,
                        likesDifPercent = itm.likesDifPercent ?? 0,

                        comments = itm.comments,
                        commentsNew = itm.commentsNew,
                        commentsDifPercent = itm.commentsDifPercent ?? 0,

                        reposts = itm.reposts,
                        repostsNew = itm.repostsNew,
                        repostsDifPercent = itm.repostsDifPercent ?? 0,
                        
                        resharesNew = itm.repostsNew,
                        resharesDifPercent = itm.repostsDifPercent ?? 0,
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

        public wsResponse<StaCommVK_Report_Resp> StaCommVKWeekly_Report(wsRequest req)
        {
            var funcName = "StaCommVKWeekly_Report";
            var errCode = 0;
            var errorText = "";

            wsResponse<StaCommVK_Report_Resp> results = new wsResponse<StaCommVK_Report_Resp>();
            StaCommVK_Report_Resp resp = new StaCommVK_Report_Resp();
            DataHubDataContext dc = new DataHubDataContext();
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

                foreach (StaCommVKWeekly_ReportResult itm in dc.StaCommVKWeekly_Report(ownerHubID))
                {
                    resp.Add(new wsSta()
                    {
                        comm_id = itm.comm_id,
                        comm_name = itm.comm_name,
                        comm_photoLink = itm.comm_photoLink,
                        comm_photoLinkBig = itm.comm_photoLinkBig,
                        comm_groupID = itm.comm_groupID ?? 0,

                        subjectComm_name = itm.subjectComm_name,
                        areaComm_code = itm.areaComm_code,

                        adminComm_fullName = itm.adminComm_fullName,
                        adminComm_linkFB = itm.adminComm_linkFB,

                        members = itm.members,
                        membersNew = itm.membersNew,
                        membersDifPercent = itm.membersDifPercent ?? 0,

                        increaseNew = itm.increaseNew,
                        increaseDifPercent = itm.increaseDifPercent ?? 0,

                        subscribed = itm.subscribed,
                        subscribedNew = itm.subscribedNew,
                        subscribedDifPercent = itm.subscribedDifPercent ?? 0,

                        unsubscribed = itm.unsubscribed,
                        unsubscribedNew = itm.unsubscribedNew,
                        unsubscribedDifPercent = itm.unsubscribedDifPercent ?? 0,

                        visitors = itm.visitors,
                        visitorsNew = itm.visitorsNew,
                        visitorsDifPercent = itm.visitorsDifPercent ?? 0,

                        views = itm.views,
                        viewsNew = itm.viewsNew,
                        viewsDifPercent = itm.viewsDifPercent ?? 0,

                        reach = itm.reach,
                        reachNew = itm.reachNew,
                        reachDifPercent = itm.reachDifPercent ?? 0,

                        reachSubscribers = itm.reachSubscribers,
                        reachSubscribersNew = itm.reachSubscribersNew,
                        reachSubscribersDifPercent = itm.reachSubscribersDifPercent ?? 0,

                        postCount = itm.postCount,
                        postCountNew = itm.postCountNew,
                        postCountDifPercent = itm.postCountDifPercent ?? 0,

                        likes = itm.likes,
                        likesNew = itm.likesNew,
                        likesDifPercent = itm.likesDifPercent ?? 0,

                        comments = itm.comments,
                        commentsNew = itm.commentsNew,
                        commentsDifPercent = itm.commentsDifPercent ?? 0,

                        reposts = itm.reposts,
                        repostsNew = itm.repostsNew,
                        repostsDifPercent = itm.repostsDifPercent ?? 0,
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

        #region StaCommVKGraph_Report
        public wsResponse<StaCommVKGraph_Report_Resp> StaCommVKGraph_Report(wsRequest<StaCommVKGraph_Report_Req> req)
        {
            string funcName = "StaCommVKGraph_Report";
            int errCode = 0;
            string errorText = "";

            wsResponse<StaCommVKGraph_Report_Resp> results = new wsResponse<StaCommVKGraph_Report_Resp>();
            StaCommVKGraph_Report_Resp resp = new StaCommVKGraph_Report_Resp();
            DataHubDataContext dc = new DataHubDataContext();
            long ownerHubID = 0;
            long commID = 0;

            if (req.Params == null)
            {
                errCode = 200;
                errorText = Tools.GetErrorTextByCode(errCode);
                Tools.ErrorLog_Save(req, "", funcName, errorText);

                results.ErrText = string.Format("{0}\n{1}", funcName, errorText);
                results.ErrCode = errCode;
                return results;
            }
            else
            {
                commID = req.Params.commID;
            }

            try
            {
                foreach (GetOwnerHubIDResult own in dc.GetOwnerHubID(req.Session))
                {
                    ownerHubID = own.ownerHubID;
                }

                foreach (StaCommVKGraph_ReportResult itm in dc.StaCommVKGraph_Report(ownerHubID, commID))
                {
                    resp.Add(new wsGraph()
                    {
                        comments = itm.commComments,
                        likes = itm.commLikes,
                        removed = itm.commRemoved,
                        share = itm.commShare,
                        members = itm.commMembers,
                        membersLost = itm.commMembersLost,
                        dayDate = itm.dayDate ?? DateTime.Now.Date,
                        dayString = string.Format("{0}.{1} {2}", (itm.dayDate ?? DateTime.Now.Date).Day, (itm.dayDate ?? DateTime.Now.Date).Month.ToString(), (itm.dayDate ?? DateTime.Now.Date).DayOfWeek),
                        isLast = itm.isLast ?? false,
                        isFuture = itm.isFuture ?? false

                    });
                };

                results.Data = resp;
            }
            catch (Exception e)
            {
                errCode = 101;
                string param = string.Format("ownerHubID: {0}, commID: {1}", ownerHubID, commID);
                Tools.ErrorLog_Save(req, param, funcName, e.Message);

                results.ErrCode = errCode;
                results.ErrText = string.Format("funcName: {0}, errCode: {1}, Message: {2}", funcName, Tools.GetErrorTextByCode(errCode), e.Message);
            }

            return results;
        }
        #endregion 

        #region StaCommOK 
        public wsResponse<StaCommOK_Report_Resp> StaCommOKDaily_Report(wsRequest<StaCommDaily_Report_Req> req) {
            var funcName = "StaCommOKDaily_Report";
            var errCode = 0;
            var errorText = "";

            wsResponse<StaCommOK_Report_Resp> results = new wsResponse<StaCommOK_Report_Resp>();
            StaCommOK_Report_Resp resp = new StaCommOK_Report_Resp();
            DataHubDataContext dc = new DataHubDataContext();
            long ownerHubID = 0;

            if (req == null) {
                errCode = 200;
                errorText = Tools.GetErrorTextByCode(errCode);
                Tools.ErrorLog_Save(req, "", funcName, errorText);

                results.ErrText = string.Format("{0}\n{1}", funcName, errorText);
                results.ErrCode = errCode;
                return results;
            }

            try {
                foreach (GetOwnerHubIDResult own in dc.GetOwnerHubID(req.Session)) {
                    ownerHubID = own.ownerHubID;
                }

                foreach (StaCommOKDaily_ReportResult itm in dc.StaCommOKDaily_Report(ownerHubID, req.Params.isPast)) {
                    resp.Add(new wsStaOK() {
                        comm_id = itm.comm_id,
                        comm_name = itm.comm_name,
                        comm_photoLink = itm.comm_photoLink,
                        comm_photoLinkBig = itm.comm_photoLinkBig,
                        comm_groupID = itm.comm_groupID ?? 0,

                        subjectComm_name = itm.subjectComm_name,
                        areaComm_code = itm.areaComm_code,

                        adminComm_fullName = itm.adminComm_fullName,
                        adminComm_linkFB = itm.adminComm_linkFB,

                        members = itm.members, 

                        increaseNew = itm.increaseNew,
                        increaseDifPercent = itm.increaseDifPercent ?? 0,                      

                        reachNew = itm.reachNew,
                        reachDifPercent = itm.reachDifPercent ?? 0,

                        postCountNew = itm.postCountNew,
                        postCountDifPercent = itm.postCountDifPercent ?? 0,
                        
                        likesNew = itm.likesNew,
                        likesDifPercent = itm.likesDifPercent ?? 0,
                        
                        commentsNew = itm.commentsNew,
                        commentsDifPercent = itm.commentsDifPercent ?? 0,

                         resharesNew = itm.resharesNew,
                        resharesDifPercent = itm.resharesDifPercent ?? 0,
                    });
                };

                results.Data = resp;
            } catch (Exception e) {
                errCode = 101;
                string param = string.Format("ownerHubID: {0}", ownerHubID);
                Tools.ErrorLog_Save(req, param, funcName, e.Message);

                results.ErrCode = errCode;
                results.ErrText = string.Format("funcName: {0}, errCode: {1}, Message: {2}", funcName, Tools.GetErrorTextByCode(errCode), e.Message);
            }

            return results;
        }


        public wsResponse<StaCommOK_Report_Resp> StaCommOKWeekly_Report(wsRequest req) {
            var funcName = "StaCommOKWeekly_Report";
            var errCode = 0;
            var errorText = "";

            wsResponse<StaCommOK_Report_Resp> results = new wsResponse<StaCommOK_Report_Resp>();
            StaCommOK_Report_Resp resp = new StaCommOK_Report_Resp();
            DataHubDataContext dc = new DataHubDataContext();
            long ownerHubID = 0;

            if (req == null) {
                errCode = 200;
                errorText = Tools.GetErrorTextByCode(errCode);
                Tools.ErrorLog_Save(req, "", funcName, errorText);

                results.ErrText = string.Format("{0}\n{1}", funcName, errorText);
                results.ErrCode = errCode;
                return results;
            }

            try {
                foreach (GetOwnerHubIDResult own in dc.GetOwnerHubID(req.Session)) {
                    ownerHubID = own.ownerHubID;
                }

                foreach (StaCommOKWeekly_ReportResult itm in dc.StaCommOKWeekly_Report(ownerHubID)) {
                    resp.Add(new wsStaOK() {
                        comm_id = itm.comm_id,
                        comm_name = itm.comm_name,
                        comm_photoLink = itm.comm_photoLink,
                        comm_photoLinkBig = itm.comm_photoLinkBig,
                        comm_groupID = itm.comm_groupID ?? 0,

                        subjectComm_name = itm.subjectComm_name,
                        areaComm_code = itm.areaComm_code,

                        adminComm_fullName = itm.adminComm_fullName,
                        adminComm_linkFB = itm.adminComm_linkFB,

                        members = itm.members,

                        increaseNew = itm.increaseNew,
                        increaseDifPercent = itm.increaseDifPercent ?? 0,

                        reachNew = itm.reachNew,
                        reachDifPercent = itm.reachDifPercent ?? 0,

                        postCountNew = itm.postCountNew,
                        postCountDifPercent = itm.postCountDifPercent ?? 0,

                        likesNew = itm.likesNew,
                        likesDifPercent = itm.likesDifPercent ?? 0,

                        commentsNew = itm.commentsNew,
                        commentsDifPercent = itm.commentsDifPercent ?? 0,

                        resharesNew = itm.resharesNew,
                        resharesDifPercent = itm.resharesDifPercent ?? 0,
                    });
                };

                results.Data = resp;
            } catch (Exception e) {
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
            
            try
            {
                string message = string.Format("CommHub+code+confirm:+{0}", code);
                string http = string.Format("{0}sms.ru/sms/send?api_id={1}&to={2}&text={3}", "http://", "8B4D21F6-33D2-DBD4-8425-34631CD434BE", phone, message);
                var request = (HttpWebRequest)WebRequest.Create(http);
                var response = (HttpWebResponse)request.GetResponse();
                var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
            }
            catch (Exception e)
            {
                results.ErrCode = 40;
                results.ErrText = string.Format("Send sms {0}, error: {1}", phone, e.Message);
            }
            
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

                if (resp.IsNewMember == true)
                {
                    dc.Owner_Set(resp.ownerHubID, sessionReqID);
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
