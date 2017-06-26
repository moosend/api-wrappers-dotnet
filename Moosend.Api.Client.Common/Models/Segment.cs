using System;
using System.Collections.Generic;

namespace Moosend.Api.Common.Models
{
    public class Segment
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public SegmentMatchType MatchType { get; set; }

        public IList<SegmentCriteria> Criteria { get; set; }

        public string CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        public string UpdatedBy { get; set; }

        public DateTime UpdatedOn { get; set; }

        public SegmentFetchType FetchType { get; set; }

        public int FetchValue { get; set; }

        public string Description { get; set; }
    }
}
