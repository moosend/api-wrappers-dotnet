using System;

namespace Moosend.Api.Common.Models
{
    public class CampaignStatisticsSummary
    {
        public int Id { get; set; }

        public AbVersion AbVersion { get; set; }

        public Guid CampaignId { get; set; }

        public string CampaignName { get; set; }

        public Guid MailingListId { get; set; }

        public string MailingListName { get; set; }

        public DateTime? CampaignDeliveredOn { get; set; }

        public DateTime To { get; set; }

        public DateTime From { get; set; }

        public int TotalOpens { get; set; }

        public int UniqueOpens { get; set; }

        public int TotalBounces { get; set; }

        public int TotalForwards { get; set; }

        public int UniqueForwards { get; set; }

        public int TotalUnsubscribes { get; set; }

        public int TotalLinkClicks { get; set; }

        public int UniqueLinkClicks { get; set; }

        public int Sent { get; set; }

        public double LinkClicksPercentage
        {
            get
            {
                if (Sent == 0) return 0;
                return UniqueLinkClicks/(double) Sent;
            }
        }

        public double OpenedPercentage
        {
            get
            {
                if (Sent == 0) return 0;
                return UniqueOpens/(double) Sent;
            }
        }

        public double BouncedPercentage
        {
            get
            {
                if (Sent == 0) return 0;
                return TotalBounces/(double) Sent;
            }
        }

        public double UnsubscribedPercentage
        {
            get
            {
                if (Sent == 0) return 0;
                return TotalUnsubscribes/(double) Sent;
            }
        }

        public double UndeliveredPercentage
        {
            get
            {
                if (Sent == 0) return 0;
                return TotalBounces/(double) Sent;
            }
        }

        public int NotOpenedCount
        {
            get { return Sent - UniqueOpens - TotalBounces; }
        }
    }
}
