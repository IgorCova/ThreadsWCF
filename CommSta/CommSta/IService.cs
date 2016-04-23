using System.ServiceModel;
using System.ServiceModel.Web;

namespace CommSta
{
    [ServiceContract]
    public interface IService
    {
        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "VKontakte_Sta")]
        void VKontakte_Sta();

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "VKontakte_Sta_ByDate")]
        void VKontakte_Sta_ByDate(wsRequestByDate req);
    }
}
