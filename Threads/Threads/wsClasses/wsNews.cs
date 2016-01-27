using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace Threads
{
    [DataContract]
    public class wsNews
    {
        [DataMember]
        public long CommunityID { get; set; }

        [DataMember]
        public string CommunityID_Name { get; set; }

        [DataMember]
        public long EntryID { get; set; }
        
        [DataMember]
        public string ColumnCommunityID_Name { get; set; }
        
        [DataMember]
        public string EntryID_EntryText { get; set; }

        [DataMember]
        public DateTime EntryID_CreateDate { get; set; }

    }
}