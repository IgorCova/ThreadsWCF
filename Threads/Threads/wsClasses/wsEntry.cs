using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace Threads
{
    [DataContract]
    public class wsEntry
    {
        [DataMember]
        public long Community_ID { get; set; }

        [DataMember]
        public string Community_Name { get; set; }

        [DataMember]
        public long Entry_ID { get; set; }
        
        [DataMember]
        public string ColumnCommunity_Name { get; set; }
        
        [DataMember]
        public string Entry_Text { get; set; }

        [DataMember]
        public DateTime Entry_CreateDate { get; set; }

    }
}