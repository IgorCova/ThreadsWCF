using System.ServiceModel;
using System.ServiceModel.Web;

namespace Threads
{
    [ServiceContract]
    public interface IService
    {
        #region Bookmark
        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "Bookmark_Save")]
        wsResponse<Bookmark_Save_Resp> Bookmark_Save(wsRequest<Bookmark_Save_Req> req);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "Bookmark_ReadByMemberID")]
        wsResponse<Bookmark_ReadByMemberID_Resp> Bookmark_ReadByMemberID(wsRequest<Bookmark_ReadByMemberID_Req> req);
        #endregion

        #region Community
        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "Community_ReadDict")]
        wsResponse<Community_ReadDict_Resp> Community_ReadDict(wsRequest<Community_ReadDict_Req> req);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "Community_ReadMyDict")]
        wsResponse<Community_ReadDict_Resp> Community_ReadMyDict(wsRequest<Community_ReadDict_Req> req);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "Community_ReadSuggestDict")]
        wsResponse<Community_ReadDict_Resp> Community_ReadSuggestDict(wsRequest<Community_ReadDict_Req> req);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "Community_ReadInstance")]
        wsResponse<Community_ReadInstance_Resp> Community_ReadInstance(wsRequest<Community_ReadInstance_Req> req);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "Community_Save")]
        wsResponse<Community_Save_Resp> Community_Save(wsRequest<Community_Save_Req> req);
        #endregion

        #region Country
        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "Country_ReadDict")]
        wsResponse<Country_ReadDict_Resp> Country_ReadDict();
        #endregion

        #region Entry
        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "Entry_ReadByCommunityID")]
        wsResponse<Entry_ReadByCommunityID_Resp> Entry_ReadByCommunityID(wsRequest<Entry_ReadByCommunityID_Req> req);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "Entry_Save")]
        wsResponse<Entry_Save_Resp> Entry_Save(wsRequest<Entry_Save_Req> req);
        #endregion

        #region Member
        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "Member_ReadInstance")]
        wsResponse<Member_ReadInstance_Resp> Member_ReadInstance(wsRequest<Member_ReadInstance_Req> req);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "Member_Save")]
        wsResponse<Member_Save_Resp> Member_Save(wsRequest<Member_Save_Req> req);
        #endregion

        #region Session

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "SessionReq_Save")]
        wsResponse<SessionReq_Save_Resp> SessionReq_Save(wsRequest<SessionReq_Save_Req> req);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "Session_Save")]
        wsResponse<Session_Save_Resp> Session_Save(wsRequest<Session_Save_Req> req);

        #endregion

        #region News
        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "News_ReadByMemberID")]
        wsResponse<News_ReadByMemberID_Resp> News_ReadByMemberID(wsRequest<News_ReadByMemberID_Req> req);
        #endregion

        #region Image
        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "LogoSave")]
        wsResponse<LogoSave_Resp> LogoSave(wsRequest<LogoSave_Req> req);
        #endregion


    }
}
