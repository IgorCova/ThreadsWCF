using System;
using System.Drawing.Imaging;
using System.Runtime.Serialization;

namespace CommHub.wsClasses
{
    public class wsSubjectComm
    {
        [DataMember]
        public long id { get; set; }

        [DataMember]
        public long ownerHubID { get; set; }

        [DataMember]
        public string name { get; set; }
    }

    public class wsAdminComm
    {
        [DataMember]
        public long id { get; set; }

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
    public class wsSessionReq_Out
    {
        [DataMember]
        public long id { get; set; }

        [DataMember]
        public string code { get; set; }

        [DataMember]
        public long ownerHubID { get; set; }

    }

    [DataContract]
    public class wsSession
    {
        [DataMember]
        public Guid? SessionID { get; set; }

        [DataMember]
        public long ownerHubID { get; set; }

        [DataMember]
        public bool IsNewMember { get; set; }
    }
}
