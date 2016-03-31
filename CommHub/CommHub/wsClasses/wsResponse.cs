using System.Collections.Generic;
using System.Runtime.Serialization;
using CommHub.wsClasses;

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

    public class SubjectComm_ReadDict_Resp : List<wsSubjectComm> { }
}