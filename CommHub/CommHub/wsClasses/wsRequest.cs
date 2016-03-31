using System.Runtime.Serialization;
using System.IO;

namespace CommHub
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

    [DataContract]
    public class SubjectComm_ReadDict_Req
    {
        [DataMember]
        public long ownerHubID;
    }
}