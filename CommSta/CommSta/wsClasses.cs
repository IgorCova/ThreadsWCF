using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace CommSta
{
    [DataContract]
    public class wsRequest
    {
        [DataMember]
        public long groupID;

    }
    [DataContract]
    public class wsRequestByDate : wsRequest
    {
        [DataMember]
        public DateTime dateFrom;

        [DataMember]
        public DateTime? dateTo;

    }

    [DataContract]
    public class wsGroup
    {
        [DataMember]
        public long groupID { get; set; }
    }

    [DataContract]
    public class wsGroups<T> where T : class
    {
        [DataMember]
        public T dir;
    }

    [DataContract]
    public class commPosts
    {
        [DataMember]
        public long likes;

        [DataMember]
        public long repost;

        [DataMember]
        public long comments;

        [DataMember]
        public long count;
    }

    public class Comm_ReadForSta_Resp : List<wsGroup> { }
}