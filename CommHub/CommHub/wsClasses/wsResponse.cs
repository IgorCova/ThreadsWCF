using System.Collections.Generic;
using System.Runtime.Serialization;
using CommHub.wsClasses;

namespace CommHub
{
    #region Response
    [DataContract]
    public class wsResponse
    {
        [DataMember]
        public int ErrCode;

        [DataMember]
        public string ErrText;
    }

    [DataContract]
    public class wsResponse<T> : wsResponse where T : class
    {
        [DataMember]
        public T Data;
    }
    #endregion

    public class SubjectComm_ReadDict_Resp : List<wsSubjectComm> { }
    public class AdminComm_ReadDict_Resp : List<wsAdminComm> { }

    public class SessionReq_Save_Resp : wsSessionReq_Out { }
    public class Session_Save_Resp : wsSession { }

    public class AdminComm_Save_Resp : wsAdminComm { }

    public class SubjectComm_Save_Resp : wsSubjectComm { }

    public class Comm_ReadDict_Resp: List<wsComm_Extended> { }

    public class AdminComm_Del_Resp: wsAdminComm_Del { }

    public class OwnerHub_Read_Resp : wsOwnerHub { }

    public class StaCommVKDaily_ReportDay_Resp : List<wsStaComm> { }

    public class StaCommVK_Report_Resp : List<wsSta> { }

    public class StaCommOK_Report_Resp : List<wsStaOK> { }
    
    public class StaCommVKGraph_Report_Resp : List<wsGraph> { } 
    

}