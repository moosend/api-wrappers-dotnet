namespace Moosend.Api.Common.Models
{
    public class CampaignMailingList
    {
        public Campaign Campaign { get; set; }
        public MailingList MailingList { get; set; }
        public Segment Segment { get; set; }
    }
}