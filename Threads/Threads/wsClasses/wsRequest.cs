using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace Threads
{
    [DataContract]
    public class wsRequest
    {
        [DataMember(Name = "Session")]
        public String Session;

        [DataMember(Name = "DID")]
        public String DID;
    }

    [DataContract]
    public class wsRequest<T> : wsRequest where T : class
    {
        [DataMember(Name = "Params")]
        public T Params;
    }

    public class Community_ReadDict_Req { }

    [DataContract]
    public class Entry_ReadByCommunityID_Req
    {
        [DataMember]
        public int CommunityID;
    }

    [DataContract]
    public class News_ReadByMemberID_Req
    {
        [DataMember]
        public int MemberID;
    }

    [DataContract]
    public class Member_ReadInstance_Req
    {
        [DataMember]
        public int MemberID;
    }

}