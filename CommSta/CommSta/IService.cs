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
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "VKontakte_Sta_ForNew")]
        void VKontakte_Sta_ForNew();

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "VKontakte_Sta_CloseDay")]
        void VKontakte_Sta_CloseDay();

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "VKontakte_Sta_CloseWeek")]
        void VKontakte_Sta_CloseWeek();        

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "VKontakte_Sta_ByDate")]
        void VKontakte_Sta_ByDate(wsRequestByDate req);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "VKontakte_Sta_Graph")]
        void VKontakte_Sta_Graph();

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "OK_Sta_Graph")]
        void OK_Sta_Graph();
        
    }
}
