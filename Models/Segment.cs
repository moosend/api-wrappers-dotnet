using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Moosend.API.Client.Models
{
    [Serializable]
    [DataContract(Namespace = "")]
    public class Segment
    {
        public Segment()
        {
            Criteria = new List<SegmentCriteria>();
        }

        [DataMember]
        public virtual int ID
        {
            get;
            internal set;
        }

        [DataMember]
        public virtual String Name
        {
            get;
            set;
        }

        [DataMember]
        public virtual SegmentMatchType MatchType
        {
            get;
            set;
        }

        [DataMember]
        public virtual IList<SegmentCriteria> Criteria
        {
            get;
            internal set;
        }

        [DataMember]
        public virtual long MemberCount
        {
            get;
            internal set;
        }

        [DataMember]
        public virtual string CreatedBy
        {
            get;
            internal set;
        }

        [DataMember]
        public virtual DateTime CreatedOn
        {
            get;
            internal set;
        }

        [DataMember]
        public virtual string UpdatedBy
        {
            get;
            internal set;
        }

        [DataMember]
        public virtual DateTime UpdatedOn
        {
            get;
            internal set;
        }

    }
}
