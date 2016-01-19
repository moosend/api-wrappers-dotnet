using System;
using Newtonsoft.Json;

namespace Moosend.Api.Common.Models
{
    public class CampaignStatisticsSummary
    {
        public int Id
        {
            get;
            internal set;
        }

        public AbVersion AbVersion
        {
            get;
            internal set;
        }

        public Guid CampaignId
        {
            get;
            internal set;
        }

        public string CampaignName
        {
            get;
            internal set;
        }

        public Guid MailingListId
        {
            get;
            internal set;
        }

        public string MailingListName
        {
            get;
            internal set;
        }

        public DateTime? CampaignDeliveredOn
        {
            get;
            internal set;
        }

        public DateTime To
        {
            get;
            internal set;
        }

        public DateTime From
        {
            get;
            internal set;
        }

        public int TotalOpens
        {
            get;
            internal set;
        }

        public int UniqueOpens
        {
            get;
            internal set;
        }

        public int TotalBounces
        {
            get;
            internal set;
        }

        public int TotalForwards
        {
            get;
            internal set;
        }

        public int UniqueForwards
        {
            get;
            internal set;
        }

        public int TotalUnsubscribes
        {
            get;
            internal set;
        }

        public int TotalLinkClicks
        {
            get;
            internal set;
        }

        public int UniqueLinkClicks
        {
            get;
            internal set;
        }

        public int Sent
        {
            get;
            internal set;
        }

        public double LinkClicksPercentage
        {
            get
            {
                if (Sent == 0) return 0;
                return ((double)UniqueLinkClicks / (double)Sent);
            }
        }

        public double OpenedPercentage
        {
            get
            {
                if (Sent == 0) return 0;
                return ((double)UniqueOpens / (double)Sent);
            }
        }

        public double BouncedPercentage
        {
            get
            {
                if (Sent == 0) return 0;
                return ((double)TotalBounces / (double)Sent);
            }
        }

        public double UnsubscribedPercentage
        {
            get
            {
                if (Sent == 0) return 0;
                return ((double)TotalUnsubscribes / (double)Sent);
            }
        }

        public double UndeliveredPercentage
        {
            get
            {
                if (Sent == 0) return 0;
                return ((double)TotalBounces / (double)Sent);
            }
        }

        public int NotOpenedCount
        {
            get
            {
                return Sent - UniqueOpens - TotalBounces;
            }
        }
    }
}
