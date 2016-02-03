using System;

namespace Moosend.Api.Common.Models
{
    public class CampaignMailingListForm
    {
        public Guid MailingListId { get; set; }
        public int SegmentId { get; set; }
    }
}