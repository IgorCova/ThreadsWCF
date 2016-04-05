using System.Collections.Generic;
using System.Runtime.Serialization;
using CommHub.wsClasses;
using VkNet.Model;
using System.Collections.ObjectModel;

namespace CommHub
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

    [DataContract]
    public class wsResponseSimple
    {
        [DataMember]
        public int ErrCode;

        [DataMember]
        public string ErrText;
    }
    
    public class SubjectComm_ReadDict_Resp : List<wsSubjectComm> { }
    public class AdminComm_ReadDict_Resp : List<wsAdminComm> { }

    public class SessionReq_Save_Resp : wsSessionReq_Out { }
    public class Session_Save_Resp : wsSession { }

    public class AdminComm_Save_Resp : wsAdminComm { }
    
}