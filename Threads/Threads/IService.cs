using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.IO;

namespace Threads
{
    [ServiceContract]
    public interface IService
    {
        //----------------------------Community
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetCommunity_ReadDict")]
        wsResponse<Community_ReadDict_Resp> GetCommunity_ReadDict();

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "Community_ReadDict")]
        wsResponse<Community_ReadDict_Resp> Community_ReadDict(wsRequest<Community_ReadDict_Req> req);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "Community_ReadMyDict")]
        wsResponse<Community_ReadDict_Resp> Community_ReadMyDict(wsRequest<Community_ReadDict_Req> req);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "Community_ReadSuggestDict")]
        wsResponse<Community_ReadDict_Resp> Community_ReadSuggestDict(wsRequest<Community_ReadDict_Req> req);


        //----------------------------Entry
        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "Entry_ReadByCommunityID")]
        wsResponse<Entry_ReadByCommunityID_Resp> Entry_ReadByCommunityID(wsRequest<Entry_ReadByCommunityID_Req> req);


        //----------------------------Member
        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "Member_ReadInstance")]
        wsResponse<Member_ReadInstance_Resp> Member_ReadInstance(wsRequest<Member_ReadInstance_Req> req);


        //----------------------------News
        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "News_ReadByMemberID")]
        wsResponse<News_ReadByMemberID_Resp> News_ReadByMemberID(wsRequest<News_ReadByMemberID_Req> req);
        // TODO: Добавьте здесь операции служб
    }
}
