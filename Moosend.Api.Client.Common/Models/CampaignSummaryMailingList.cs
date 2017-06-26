using System;

namespace Moosend.Api.Common.Models
{
    public class CampaignSummaryMailingList
    {
        public Guid MailingListId { get; set; }
        public int SegmentId { get; set; }
    }
}