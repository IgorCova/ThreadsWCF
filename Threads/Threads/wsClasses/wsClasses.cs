using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace Threads
{
    [DataContract]
    public class wsCommunity
    {
        [DataMember]
        public long ID { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public long OwnerID { get; set; }

        [DataMember]
        public bool IsMember { get; set; }

        [DataMember]
        public DateTime CreateDate { get; set; }

        [DataMember]
        public long DefaultColumnID { get; set; }
    }

    [DataContract]
    public class wsEntrySaveState
    {
        public long ID { get; set; }
    }

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
        public long ColumnCommunity_ID { get; set; }

        [DataMember]
        public string ColumnCommunity_Name { get; set; }

        [DataMember]
        public string Entry_Text { get; set; }

        [DataMember]
        public DateTime Entry_CreateDate { get; set; }

    }

    [DataContract]
    public class wsMember
    {
        [DataMember]
        public long ID { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string UserName { get; set; }

        [DataMember]
        public string FullName { get; set; }

        [DataMember]
        public string About { get; set; }

        [DataMember]
        public DateTime JoinedDate { get; set; }
    }
}