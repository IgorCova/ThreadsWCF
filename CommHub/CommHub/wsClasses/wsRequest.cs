using CommHub.wsClasses;
using System.Runtime.Serialization;

namespace CommHub
{
    #region Request
    [DataContract]
    public class wsRequest
    {
        [DataMember]
        public string Session;

        [DataMember]
        public string DID;
    }

    [DataContract]
    public class wsRequest<T> : wsRequest where T : class
    {
        [DataMember]
        public T Params;
    }
    #endregion


    [DataContract]
    public class AdminComm_Save_Req
    {
        [DataMember]
        public long? id;

        [DataMember]
        public string firstName { get; set; }

        [DataMember]
        public string lastName { get; set; }

        [DataMember]
        public string phone { get; set; }

        [DataMember]
        public string linkFB { get; set; }
    }

    [DataContract]
    public class OwnerHub_Save_Req
    {
        [DataMember]
        public long? id;

        [DataMember]
        public string firstName { get; set; }

        [DataMember]
        public string lastName { get; set; }

        [DataMember]
        public string phone { get; set; }

        [DataMember]
        public string linkFB { get; set; }
    }

    [DataContract]
    public class Comm_Save_Req
    {
        [DataMember]
        public wsComm comm { get; set; }
    }

    [DataContract]
    public class SubjectComm_Save_Req
    {
        [DataMember]
        public long id { get; set; }

        [DataMember]
        public string name { get; set; }
    }

    [DataContract]
    public class SessionReq_Save_Req
    {
        [DataMember]
        public string did { get; set; }

        [DataMember]
        public string phone { get; set; }

    }

    [DataContract]
    public class Session_Save_Req
    {
        [DataMember]
        public string did { get; set; }

        [DataMember]
        public long sessionReqID;
    }

    [DataContract]
    public class InstanceID
    {
        [DataMember]
        public long id;

    }

    [DataContract]
    public class StaCommVKDaily_Report_Req
    {
        [DataMember]
        public bool isPast;
    }
    
}