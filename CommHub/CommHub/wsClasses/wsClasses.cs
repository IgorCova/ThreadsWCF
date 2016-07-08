using System;
using System.Runtime.Serialization;

namespace CommHub.wsClasses
{
    #region AdminComm
    [DataContract]
    public class wsAdminComm : wsPerson { }

    [DataContract]
    public class wsOwnerHub : wsPerson { }

    [DataContract]
    public class wsPerson
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
        public string photoLink { get; set; }

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

    [DataContract]
    public class wsCommInstance {
        [DataMember]
        public long id { get; set; }

        [DataMember]
        public string name { get; set; }

        [DataMember]
        public string subjectCommID_name { get; set; }

        [DataMember]
        public string areaCommID_code { get; set; }

        [DataMember]
        public string adminCommID_Name { get; set; }

        [DataMember]
        public string adminCommID_linkFB { get; set; }

        [DataMember]
        public string link { get; set; }

        [DataMember]
        public string photoLink { get; set; }

        [DataMember]
        public string photoLinkBig { get; set; }

        [DataMember]
        public long groupID { get; set; }

        [DataMember]
        public long countMembers { get; set; }

        [DataMember]
        public long countWoman { get; set; }

        [DataMember]
        public long countWomanPercent { get; set; }

        [DataMember]
        public long countMen { get; set; }

        [DataMember]
        public long countMenPercent { get; set; }

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

    #region wsSta
    [DataContract]
    public class wsSta
    {
        [DataMember]
        public long comm_id { get; set; }

        [DataMember]
        public string comm_name { get; set; }

        [DataMember]
        public string comm_photoLink { get; set; }

        [DataMember]
        public string comm_photoLinkBig { get; set; }

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
        public long membersOld { get; set; }

        [DataMember]
        public int membersDifPercent { get; set; }

        [DataMember]
        public long increase { get; set; }
        [DataMember]
        public long increaseNew { get; set; }

        [DataMember]
        public long increaseOld { get; set; }

        [DataMember]
        public long increaseDifPercent { get; set; }

        [DataMember]
        public long subscribed { get; set; }

        [DataMember]
        public long subscribedNew { get; set; }

        [DataMember]
        public long subscribedOld { get; set; }

        [DataMember]
        public int subscribedDifPercent { get; set; }

        [DataMember]
        public long unsubscribed { get; set; }

        [DataMember]
        public long unsubscribedNew { get; set; }

        [DataMember]
        public long unsubscribedOld { get; set; }

        [DataMember]
        public int unsubscribedDifPercent { get; set; }

        [DataMember]
        public long visitors { get; set; }

        [DataMember]
        public long visitorsNew { get; set; }

        [DataMember]
        public long visitorsOld { get; set; }

        [DataMember]
        public int visitorsDifPercent { get; set; }

        [DataMember]
        public long views { get; set; }

        [DataMember]
        public long viewsNew { get; set; }

        [DataMember]
        public long viewsOld { get; set; }

        [DataMember]
        public int viewsDifPercent { get; set; }

        [DataMember]
        public long reach { get; set; }

        [DataMember]
        public long reachNew { get; set; }

        [DataMember]
        public long reachOld { get; set; }

        [DataMember]
        public int reachDifPercent { get; set; }

        [DataMember]
        public long reachSubscribers { get; set; }

        [DataMember]
        public long reachSubscribersNew { get; set; }

        [DataMember]
        public long reachSubscribersOld { get; set; }

        [DataMember]
        public int reachSubscribersDifPercent { get; set; }

        [DataMember]
        public long postCount { get; set; }

        [DataMember]
        public long postCountNew { get; set; }

        [DataMember]
        public long postCountOld { get; set; }

        [DataMember]
        public int postCountDifPercent { get; set; }

        [DataMember]
        public long likes { get; set; }

        [DataMember]
        public long likesNew { get; set; }

        [DataMember]
        public long likesOld { get; set; }

        [DataMember]
        public int likesDifPercent { get; set; }

        [DataMember]
        public long comments { get; set; }

        [DataMember]
        public long commentsNew { get; set; }

        [DataMember]
        public long commentsOld { get; set; }

        [DataMember]
        public int commentsDifPercent { get; set; }

        [DataMember]
        public long reposts { get; set; }

        [DataMember]
        public long repostsNew { get; set; }

        [DataMember]
        public long repostsOld { get; set; }

        [DataMember]
        public int repostsDifPercent { get; set; }

        [DataMember]
        public long resharesNew { get; set; }
        
        [DataMember]
        public int resharesDifPercent { get; set; }

    }
    #endregion

    public class wsGraph
    {
        [DataMember]
        public long likes { get; set; }

        [DataMember]
        public long comments { get; set; }

        [DataMember]
        public long share { get; set; }

        [DataMember]
        public long removed { get; set; }

        [DataMember]
        public long members { get; set; }

        [DataMember]
        public long membersLost { get; set; }

        [DataMember]
        public DateTime dayDate { get; set; }

        [DataMember]
        public string dayString { get; set; }

        [DataMember]
        public bool isLast { get; set; }

        [DataMember]
        public bool isFuture { get; set; }        
    }

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


    #region wsStaOK
    [DataContract]
    public class wsStaOK {
        [DataMember]
        public long comm_id { get; set; }

        [DataMember]
        public string comm_name { get; set; }

        [DataMember]
        public string comm_photoLink { get; set; }

        [DataMember]
        public string comm_photoLinkBig { get; set; }

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
        public long increase { get; set; }
        [DataMember]
        public long increaseNew { get; set; }

        [DataMember]
        public long increaseOld { get; set; }

        [DataMember]
        public long increaseDifPercent { get; set; }

        [DataMember]
        public long reachNew { get; set; }
      
        [DataMember]
        public int reachDifPercent { get; set; }
        
        [DataMember]
        public long postCountNew { get; set; }

        [DataMember]
        public int postCountDifPercent { get; set; }

        [DataMember]
        public long likesNew { get; set; }

        [DataMember]
        public int likesDifPercent { get; set; }      

        [DataMember]
        public long commentsNew { get; set; }
       
        [DataMember]
        public int commentsDifPercent { get; set; }
       
        [DataMember]
        public long resharesNew { get; set; }

        [DataMember]
        public int resharesDifPercent { get; set; }
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
