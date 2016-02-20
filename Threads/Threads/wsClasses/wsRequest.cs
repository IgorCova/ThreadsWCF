using System;
using System.Runtime.Serialization;

namespace Threads
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

    public class Community_ReadDict_Req
    {
        [DataMember]
        public long MemberID;
    }

    [DataContract]
    public class Entry_ReadByCommunityID_Req
    {
        [DataMember]
        public long CommunityID;
    }

    [DataContract]
    public class Entry_Save_Req
    {
        [DataMember]
        public long CommunityID;

        [DataMember]
        public long ColumnID;

        [DataMember]
        public long CreatorID;

        [DataMember]
        public string EntryText;

    }

    [DataContract]
    public class News_ReadByMemberID_Req
    {
        [DataMember]
        public long MemberID;
    }

    [DataContract]
    public class Member_ReadInstance_Req
    {
        [DataMember]
        public long MemberID;
    }

    [DataContract]
    public class Member_Save_Req
    {
        [DataMember]
        public wsMember Member;

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