using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Moosend.API.Client.Models
{
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

        [JsonProperty]
        public virtual int ID
        {
            get;
            internal set;
        }

        [JsonProperty]
        public virtual String Name
        {
            get;
            set;
        }

        [JsonProperty]
        public virtual SegmentMatchType MatchType
        {
            get;
            set;
        }

        [JsonProperty]
        public virtual IList<SegmentCriteria> Criteria
        {
            get;
            internal set;
        }

        [JsonProperty]
        public virtual long MemberCount
        {
            get;
            internal set;
        }

        [JsonProperty]
        public virtual string CreatedBy
        {
            get;
            internal set;
        }

        [JsonProperty]
        public virtual DateTime CreatedOn
        {
            get;
            internal set;
        }

        [JsonProperty]
        public virtual string UpdatedBy
        {
            get;
            internal set;
        }

        [JsonProperty]
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
