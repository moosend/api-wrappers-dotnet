using System;
using System.Collections.Generic;

namespace Moosend.Api.Common.Models
{
    public class CampaignSummaryMailingListDetails
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int ActiveMemberCount { get; set; }
        public int BouncedMemberCount { get; set; }
        public int RemovedMemberCount { get; set; }
        public int UnsubscribedMemberCount { get; set; }
        public int Status { get; set; }
        // TODO set the type. Probably is always empty list or the same type as the corresponding property in MailingList obj
        public IList<object> CustomFieldsDefinition { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string UpdatedBy { get; set; }   
        public DateTime UpdatedOn { get; set; }
        // TODO set the type
        public object ImportOperation { get; set; }
    }
}