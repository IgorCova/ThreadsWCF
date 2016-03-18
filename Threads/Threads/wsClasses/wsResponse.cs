using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Threads
{
    [DataContract]
    public class wsResponse<T> where T : class
    {
        [DataMember]
        public T Data;

        [DataMember]
        public int ErrCode;

        [DataMember]
        public string ErrText;
    }

    public class Community_ReadDict_Resp : List<wsCommunity> { }
    public class Entry_ReadByCommunityID_Resp : List<wsEntry> { }
    public class News_ReadByMemberID_Resp : List<wsEntry> { }
    public class Member_ReadInstance_Resp : wsMember { }
    public class Entry_Save_Resp : wsEntrySaveState { }
    public class Member_Save_Resp : wsMember { }
    public class SessionReq_Save_Resp : wsSessionReq_Out { }
    public class Country_ReadDict_Resp : List<wsCountry> { }
    public class Session_Save_Resp : wsSession { }
    public class LogoSave_Resp : wsLogoOut { }
    public class Community_ReadInstance_Resp : wsCommunity { }
    public class Community_Save_Resp : wsCommunitySaveState { }
    public class Bookmark_Save_Resp : wsBookmarkSave_Out { }
    public class Bookmark_ReadByMemberID_Resp : List<wsEntry> { }
}