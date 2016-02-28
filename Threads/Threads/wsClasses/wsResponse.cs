﻿using System.Collections.Generic;
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

}