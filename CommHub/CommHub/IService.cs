using System.ServiceModel;
using System.ServiceModel.Web;

namespace CommHub
{
    [ServiceContract]
    public interface IService
    {
        #region AdminComm
        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "AdminComm_Del")]
        wsResponse<AdminComm_Del_Resp> AdminComm_Del(wsRequest<InstanceID> req);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "AdminComm_ReadDict")]
        wsResponse<AdminComm_ReadDict_Resp> AdminComm_ReadDict(wsRequest req);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "AdminComm_Save")]
        wsResponse<AdminComm_Save_Resp> AdminComm_Save(wsRequest<AdminComm_Save_Req> req);
        #endregion

        #region OwnerHub
        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "OwnerHub_Save")]
        wsResponse OwnerHub_Save(wsRequest<OwnerHub_Save_Req> req);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "OwnerHub_Read")]
        wsResponse<OwnerHub_Read_Resp> OwnerHub_Read(wsRequest req);
        #endregion

        #region Comm
        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "Comm_Del")]
        wsResponse Comm_Del(wsRequest<InstanceID> req);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "Comm_ReadDict")]
        wsResponse<Comm_ReadDict_Resp> Comm_ReadDict(wsRequest req);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "Comm_Save")]
        wsResponse Comm_Save(wsRequest<Comm_Save_Req> req);
        #endregion

        #region StaComm
        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "StaCommVKDaily_ReportDay")]
        wsResponse<StaCommVKDaily_ReportDay_Resp> StaCommVKDaily_ReportDay(wsRequest req);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "StaCommVKDaily_Report")]
        wsResponse<StaCommVKDaily_Report_Resp> StaCommVKDaily_Report(wsRequest req);
        #endregion

        #region SubjectComm
        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "SubjectComm_Del")]
        wsResponse SubjectComm_Del(wsRequest<InstanceID> req);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "SubjectComm_ReadDict")]
        wsResponse<SubjectComm_ReadDict_Resp> SubjectComm_ReadDict(wsRequest req);
        
        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "SubjectComm_Save")]
        wsResponse<SubjectComm_Save_Resp> SubjectComm_Save(wsRequest<SubjectComm_Save_Req> req);
        #endregion   

        #region Session
        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "SessionReq_Save")]
        wsResponse<SessionReq_Save_Resp> SessionReq_Save(SessionReq_Save_Req req);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "Session_Save")]
        wsResponse<Session_Save_Resp> Session_Save(Session_Save_Req req);
        #endregion
    }
}
