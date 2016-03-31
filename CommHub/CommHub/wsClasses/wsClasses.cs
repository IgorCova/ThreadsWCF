using System;
using System.Drawing.Imaging;
using System.Runtime.Serialization;

namespace CommHub.wsClasses
{
    public class wsSubjectComm
    {
        [DataMember]
        public long id { get; set; }

        [DataMember]
        public long ownerHubID { get; set; }

        [DataMember]
        public string name { get; set; }
    }
}