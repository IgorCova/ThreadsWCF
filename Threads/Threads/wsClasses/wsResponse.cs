using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace Threads
{
    [DataContract]
    public class wsResponse<T> where T : class
    {
        [DataMember(Name = "Data")]
        public T Data;

        [DataMember(Name = "ErrCode")]
        public int ErrCode;

        [DataMember(Name = "ErrText")]
        public string ErrText;
    }

    public class Community_ReadDict_Resp : List<wsCommunity> { }
    public class Entry_ReadByCommunityID_Resp : List<wsEntry> { }
    public class News_ReadByMemberID_Resp : List<wsEntry> { }
    public class Member_ReadInstance_Resp : wsMember { }
}