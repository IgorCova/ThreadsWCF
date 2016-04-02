using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace CommHub
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IService
    {

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "SubjectComm_ReadDict")]
        wsResponse<SubjectComm_ReadDict_Resp> SubjectComm_ReadDict(wsRequest<SubjectComm_ReadDict_Req> req);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "AdminComm_ReadDict")]
        wsResponse<AdminComm_ReadDict_Resp> AdminComm_ReadDict(wsRequest<AdminComm_ReadDict_Req> req);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "AdminComm_Save")]
        wsResponse<AdminComm_Save_Resp> AdminComm_Save(wsRequest<AdminComm_Save_Req> req);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "VK_Stats_Get")]
        wsResponse VK_Stats_Get(wsRequest<VK_Stats_Get_Req> req);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "VK_Stats_GetNorm")]
        void VK_Stats_GetNorm();

        #region Session

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "SessionReq_Save")]
        wsResponse<SessionReq_Save_Resp> SessionReq_Save(wsRequest<SessionReq_Save_Req> req);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "Session_Save")]
        wsResponse<Session_Save_Resp> Session_Save(wsRequest<Session_Save_Req> req);

        #endregion
    }
}
