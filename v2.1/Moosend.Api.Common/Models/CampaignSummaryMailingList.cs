namespace Moosend.Api.Common.Models
{
    public class CampaignSummaryMailingList
    {
        public Campaign Campaign { get; set; }
        public CampaignSummaryMailingListDetails MailingList { get; set; }
        public Segment Segment { get; set; }
    }
}