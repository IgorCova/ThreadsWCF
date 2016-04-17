using System.ServiceModel;
using System.ServiceModel.Web;

namespace CommSta
{
    [ServiceContract]
    public interface IService
    {
        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "VKontakte_Sta")]
        void VKontakte_Sta(wsRequest req);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "VKontakte_Sta_ByDate")]
        void VKontakte_Sta_ByDate(wsRequestByDate req);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "CommVK_GetPhoto")]
        string CommVK_GetPhoto(wsCommVK_GetPhoto_Req req);
    }
}
