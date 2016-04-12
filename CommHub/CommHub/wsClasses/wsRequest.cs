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
    public class AdminComm_ReadDict_Req
    {
        [DataMember]
        public long ownerHubID;
    }

    [DataContract]
    public class AdminComm_Save_Req
    {
        [DataMember]
        public long? id;

        [DataMember]
        public long ownerHubID;

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
    public class Comm_ReadDict_Req
    {
        [DataMember]
        public long ownerHubID;
    }

    [DataContract]
    public class SubjectComm_ReadDict_Req
    {
        [DataMember]
        public long ownerHubID;
    }

    [DataContract]
    public class SubjectComm_Save_Req
    {
        [DataMember]
        public long id { get; set; }

        [DataMember]
        public long ownerHubID { get; set; }

        [DataMember]
        public string name { get; set; }
    }

    [DataContract]
    public class SessionReq_Save_Req
    {
        [DataMember]
        public string Phone { get; set; }

    }

    [DataContract]
    public class Session_Save_Req
    {
        [DataMember]
        public long SessionReq_ID;

    }
}