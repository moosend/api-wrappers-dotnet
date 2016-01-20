using System;
using System.Collections.Generic;

namespace Moosend.Api.Common.Models
{
    public class CampaignMailingListSummary
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int ActiveMemberCount { get; set; }
        public int BouncedMemberCount { get; set; }
        public int RemovedMemberCount { get; set; }
        public int UnsubscribedMemberCount { get; set; }
        public int Status { get; set; }
        public List<object> CustomFieldsDefinition { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime UpdatedOn { get; set; }
        public object ImportOperation { get; set; }
    }
}