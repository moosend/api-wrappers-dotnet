using System;
using System.Collections.Generic;

namespace Moosend.Api.Common.Models
{
    public class Segment
    {
        public Segment()
        {
            Criteria = new List<SegmentCriteria>();
        }

        public Segment(Guid mailingListId)
        {
            mailingListId.CheckNotNull("mailingListID");

            Criteria = new List<SegmentCriteria>();
            MailingListId = mailingListId;
        }

        public Segment(MailingList mailingList, string name, SegmentMatchType matchType = SegmentMatchType.All)
        {
            Criteria = new List<SegmentCriteria>();
            MailingListId = mailingList.Id;
            Name = name;
            MatchType = matchType;
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public SegmentMatchType MatchType { get; set; }

        public IList<SegmentCriteria> Criteria { get; set; }

        public long MemberCount { get; set; }

        public string CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        public string UpdatedBy { get; set; }

        public DateTime UpdatedOn { get; set; }

        public Guid MailingListId { get; set; }

        public SegmentFetchType FetchType { get; set; }

        public int FetchValue { get; set; }

        public string Description { get; set; }
    }
}
