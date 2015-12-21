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
        public long ID { get; set; }

        [DataMember]
        public long CommunityID { get; set; }
        
        [DataMember]
        public string CommunityID_Name { get; set; }

        [DataMember]
        public long ColumnID { get; set; }

        [DataMember]
        public string ColumnID_Name { get; set; }

        [DataMember]
        public long CreatorID { get; set; }

        [DataMember]
        public string CreatorID_FullName { get; set; }
        
        [DataMember]
        public string EntryText { get; set; }

        [DataMember]
        public DateTime CreateDate { get; set; }
        
    }
}