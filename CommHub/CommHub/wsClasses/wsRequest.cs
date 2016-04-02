using System.Runtime.Serialization;
using System.IO;
using System;

namespace CommHub
{
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
    public class SubjectComm_ReadDict_Req
    {
        [DataMember]
        public long ownerHubID;
    }

    [DataContract]
    public class VK_Stats_Get_Req
    {
        [DataMember]
        public long groupId;

        [DataMember]
        public DateTime dateFrom;

        [DataMember]
        public DateTime? dateTo = null;
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