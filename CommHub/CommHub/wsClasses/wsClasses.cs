using System;
using System.Runtime.Serialization;

namespace CommHub.wsClasses
{
    #region AdminComm
    [DataContract]
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

    public class wsOwnerHub
    {
        [DataMember]
        public string firstName { get; set; }

        [DataMember]
        public string lastName { get; set; }

        [DataMember]
        public string phone { get; set; }

        [DataMember]
        public string linkFB { get; set; }
    }

    public class wsAdminComm_Del
    {
        [DataMember]
        public bool isSuccessful { get; set; }
    }
    #endregion

    #region Comm
    [DataContract]
    public class wsComm_Extended: wsComm
    {
        [DataMember]
        public string ownerHubID_firstName { get; set; }

        [DataMember]
        public string ownerHubID_lastName { get; set; }

        [DataMember]
        public string ownerHubID_linkFB { get; set; }

        [DataMember]
        public string subjectCommID_name { get; set; }

        [DataMember]
        public string areaCommID_name { get; set; }

        [DataMember]
        public string adminCommID_firstName { get; set; }

        [DataMember]
        public string adminCommID_lastName { get; set; }

        [DataMember]
        public string adminCommID_phone { get; set; }

        [DataMember]
        public string adminCommID_linkFB { get; set; }
    }

    [DataContract]
    public class wsComm
    {
        [DataMember]
        public long id { get; set; }

        [DataMember]
        public string name { get; set; }

        [DataMember]
        public long ownerHubID { get; set; }

        [DataMember]
        public long subjectCommID { get; set; }
      
        [DataMember]
        public int areaCommID { get; set; }

        [DataMember]
        public long adminCommID { get; set; }

        [DataMember]
        public string link { get; set; }

        [DataMember]
        public long groupID { get; set; }

    }
    #endregion

    #region SubjectComm
    [DataContract]
    public class wsSubjectComm
    {
        [DataMember]
        public long id { get; set; }

        [DataMember]
        public long ownerHubID { get; set; }

        [DataMember]
        public string name { get; set; }
    }
    #endregion

    #region Session
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
    #endregion
}
