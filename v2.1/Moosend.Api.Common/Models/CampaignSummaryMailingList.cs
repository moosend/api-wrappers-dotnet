namespace Moosend.Api.Common.Models
{
    public class CampaignSummaryMailingList
    {
        public Campaign Campaign { get; set; }
        public CampaignMailingListSummary MailingList { get; set; }
        public Segment Segment { get; set; }
    }
}