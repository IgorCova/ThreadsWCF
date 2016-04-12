using System.Runtime.Serialization;

namespace CommSta
{
    [DataContract]
    public class wsRequest
    {
        [DataMember]
        public long groupID;

    }
}