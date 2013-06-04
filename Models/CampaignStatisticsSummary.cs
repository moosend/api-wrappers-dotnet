using System;
using Newtonsoft.Json;

namespace Moosend.API.Client.Models
{
    public class CampaignStatisticsSummary
    {
        [JsonProperty]
        public virtual int ID
        {
            get;
            internal set;
        }

        [JsonProperty]
        public virtual ABVersion ABVersion
        {
            get;
            internal set;
        }

        [JsonProperty]
        public virtual Guid CampaignID
        {
            get;
            internal set;
        }

        [JsonProperty]
        public virtual string CampaignName
        {
            get;
            internal set;
        }

        [JsonProperty]
        public virtual Guid MailingListID
        {
            get;
            internal set;
        }

        [JsonProperty]
        public virtual string MailingListName
        {
            get;
            internal set;
        }

        [JsonProperty]
        public virtual DateTime? CampaignDeliveredOn
        {
            get;
            internal set;
        }

        [JsonProperty]
        public virtual DateTime To
        {
            get;
            internal set;
        }

        [JsonProperty]
        public virtual DateTime From
        {
            get;
            internal set;
        }

        [JsonProperty]
        public virtual int TotalOpens
        {
            get;
            internal set;
        }

        [JsonProperty]
        public virtual int UniqueOpens
        {
            get;
            internal set;
        }

        [JsonProperty]
        public virtual int TotalBounces
        {
            get;
            internal set;
        }

        [JsonProperty]
        public virtual int TotalForwards
        {
            get;
            internal set;
        }

        [JsonProperty]
        public virtual int UniqueForwards
        {
            get;
            internal set;
        }

        [JsonProperty]
        public virtual int TotalUnsubscribes
        {
            get;
            internal set;
        }

        [JsonProperty]
        public virtual int TotalLinkClicks
        {
            get;
            internal set;
        }

        [JsonProperty]
        public virtual int UniqueLinkClicks
        {
            get;
            internal set;
        }

        [JsonProperty]
        public virtual int Sent
        {
            get;
            internal set;
        }

        public virtual double LinkClicksPercentage
        {
            get
            {
                if (Sent == 0) return 0;
                return ((double)UniqueLinkClicks / (double)Sent);
            }
        }

        public virtual double OpenedPercentage
        {
            get
            {
                if (Sent == 0) return 0;
                return ((double)UniqueOpens / (double)Sent);
            }
        }

        public virtual double BouncedPercentage
        {
            get
            {
                if (Sent == 0) return 0;
                return ((double)TotalBounces / (double)Sent);
            }
        }

        public virtual double UnsubscribedPercentage
        {
            get
            {
                if (Sent == 0) return 0;
                return ((double)TotalUnsubscribes / (double)Sent);
            }
        }

        public virtual double UndeliveredPercentage
        {
            get
            {
                if (Sent == 0) return 0;
                return ((double)TotalBounces / (double)Sent);
            }
        }

        public virtual int NotOpenedCount
        {
            get
            {
                return Sent - UniqueOpens - TotalBounces;
            }
        }
    }
}
