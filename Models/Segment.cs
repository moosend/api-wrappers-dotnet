using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Moosend.API.Client.Models
{
    [Serializable]
    [DataContract(Namespace = "")]
    public class Segment
    {
        internal Segment()
        {
            Criteria = new List<SegmentCriteria>();
        }

        public Segment(Guid mailingListID)
        {
            mailingListID.CheckNotNull("mailingListID");

            Criteria = new List<SegmentCriteria>();
            this.MailingListID = mailingListID;
        }

        public Segment(MailingList mailingList, String name, SegmentMatchType matchType = SegmentMatchType.All)
        {
            Criteria = new List<SegmentCriteria>();
            this.MailingListID = mailingList.ID;
            this.Name = name;
            this.MatchType = matchType;
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

        public virtual Guid MailingListID
        {
            get;
            internal set;
        }

    }
}
