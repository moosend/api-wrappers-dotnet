using System;

namespace Moosend.Api.Common.Models
{
    public class SegmentCriteria
    {
        public int Id { get; set; }

        public int SegmentId { get; set; }

        public SegmentCriteriaField Field { get; set; }

        public Guid? CustomFieldId { get; set; }

        public SegmentCriteriaComparer Comparer { get; set; }

        public string Value { get; set; }

        public DateTime? DateFrom { get; set; }

        public DateTime? DateTo { get; set; }
    }
}
