using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Moosend.Api.Common.Models
{
    public class Segment
    {
        internal Segment()
        {
            Criteria = new List<SegmentCriteria>();
        }

        public Segment(Guid mailingListId)
        {
            mailingListId.CheckNotNull("mailingListID");

            Criteria = new List<SegmentCriteria>();
            this.MailingListId = mailingListId;
        }

        public Segment(MailingList mailingList, string name, SegmentMatchType matchType = SegmentMatchType.All)
        {
            Criteria = new List<SegmentCriteria>();
            this.MailingListId = mailingList.Id;
            this.Name = name;
            this.MatchType = matchType;
        }

        public int Id
        {
            get;
            internal set;
        }

        public string Name
        {
            get;
            set;
        }

        public SegmentMatchType MatchType
        {
            get;
            set;
        }

        public IList<SegmentCriteria> Criteria
        {
            get;
            internal set;
        }

        public long MemberCount
        {
            get;
            internal set;
        }

        public string CreatedBy
        {
            get;
            internal set;
        }

        public DateTime CreatedOn
        {
            get;
            internal set;
        }

        public string UpdatedBy
        {
            get;
            internal set;
        }

        public DateTime UpdatedOn
        {
            get;
            internal set;
        }

        public Guid MailingListId
        {
            get;
            internal set;
        }

    }
}
