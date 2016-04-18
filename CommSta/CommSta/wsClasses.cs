using System;
using System.Runtime.Serialization;

namespace CommSta
{
    [DataContract]
    public class wsRequest
    {
        [DataMember]
        public long groupID;

    }

    public class wsRequestByDate : wsRequest
    {
        [DataMember]
        public DateTime dateFrom;

        [DataMember]
        public DateTime? dateTo;

    }
}