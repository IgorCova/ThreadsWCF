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
    public class wsAdminComm_Del
    {
        [DataMember]
        public bool isSuccessful { get; set; }
    }
    #endregion

    #region Comm
    [DataContract]
    public class wsComm_Extended : wsComm
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

    [DataContract]
    public class wsStaComm
    {
        [DataMember]
        public long comm_id { get; set; }

        [DataMember]
        public string comm_name { get; set; }

        [DataMember]
        public string comm_photoLink { get; set; }

        [DataMember]
        public long comm_groupID { get; set; }

        [DataMember]
        public string subjectComm_name { get; set; }

        [DataMember]
        public string areaComm_code { get; set; }

        [DataMember]
        public string adminComm_fullName { get; set; }

        [DataMember]
        public string adminComm_linkFB { get; set; }

        [DataMember]
        public long members { get; set; }

        [DataMember]
        public long membersNew { get; set; }

        [DataMember]
        public decimal membersNewPercent { get; set; }
        
        [DataMember]
        public long subscribed { get; set; }

        [DataMember]
        public long subscribedNew { get; set; }

        [DataMember]
        public decimal subscribedNewPercent { get; set; }

        [DataMember]
        public long unsubscribed { get; set; }

        [DataMember]
        public long unsubscribedNew { get; set; }

        [DataMember]
        public decimal unsubscribedNewPercent { get; set; }
        
        [DataMember]
        public long visitors { get; set; }

        [DataMember]
        public long visitorsNew { get; set; }

        [DataMember]
        public decimal visitorsNewPercent { get; set; }
        
        [DataMember]
        public long views { get; set; }

        [DataMember]
        public long viewsNew { get; set; }

        [DataMember]
        public decimal viewsNewPercent { get; set; }

        [DataMember]
        public long reach { get; set; }

        [DataMember]
        public long reachNew { get; set; }

        [DataMember]
        public decimal reachNewPercent { get; set; }

        [DataMember]
        public long reachSubscribers { get; set; }

        [DataMember]
        public long reachSubscribersNew { get; set; }

        [DataMember]
        public decimal reachSubscribersNewPercent { get; set; }

        [DataMember]
        public long postCount { get; set; }

        [DataMember]
        public long postCountNew { get; set; }

        [DataMember]
        public decimal postCountNewPercent { get; set; }
        
        [DataMember]
        public long likes { get; set; }

        [DataMember]
        public long likesNew { get; set; }

        [DataMember]
        public decimal likesNewPercent { get; set; }
    
        [DataMember]
        public long comments { get; set; }

        [DataMember]
        public long commentsNew { get; set; }

        [DataMember]
        public decimal commentsNewPercent { get; set; }

        [DataMember]
        public long reposts { get; set; }

        [DataMember]
        public long repostsNew { get; set; }

        [DataMember]
        public decimal repostsNewPercent { get; set; }

    }

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
