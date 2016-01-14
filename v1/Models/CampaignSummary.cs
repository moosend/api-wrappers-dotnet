using System;
using Newtonsoft.Json;

namespace Moosend.API.Client.Models
{
    public class CampaignSummary
    {
        [JsonProperty]
        public virtual Guid ID
        {
            get;
            internal set;
        }

        [JsonProperty]
        public virtual String Name
        {
            get;
            internal set;
        }

        [JsonProperty]
        public virtual String Subject
        {
            get;
            internal set;
        }

        [JsonProperty]
        public virtual string SiteName
        {
            get;
            internal set;
        }

        [JsonProperty]
        public virtual String ConfirmationTo
        {
            get;
            internal set;
        }

        [JsonProperty]
        public virtual DateTime CreatedOn
        {
            get;
            internal set;
        }

        [JsonProperty]
        public virtual int? ABHoursToTest
        {
            get;
            internal set;
        }

        [JsonProperty]
        public virtual ABCampaignType? ABCampaignType
        {
            get;
            internal set;
        }

        [JsonProperty]
        public virtual ABWinnerSelectionType ABWinnerSelectionType
        {
            get;
            internal set;
        }

        [JsonProperty]
        public virtual CampaignStatus Status
        {
            get;
            internal set;
        }

        [JsonProperty]
        public virtual DateTime? DeliveredOn
        {
            get;
            internal set;
        }

        [JsonProperty]
        public virtual DateTime? ScheduledFor
        {
            get;
            internal set;
        }

        [JsonProperty]
        public virtual String ScheduledForTimezone
        {
            get;
            internal set;
        }

        [JsonProperty]
        public virtual Guid? MailingListID
        {
            get;
            internal set;
        }

        [JsonProperty]
        public virtual String MailingListName
        {
            get;
            internal set;
        }

        [JsonProperty]
        public virtual int SegmentID
        {
            get;
            internal set;
        }

        [JsonProperty]
        public virtual String SegmentName
        {
            get;
            internal set;
        }

        [JsonProperty]
        public virtual MailingListStatus MailingListStatus
        {
            get;
            internal set;
        }

        [JsonProperty]
        public virtual int TotalSent
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

        public virtual double LinkClicksPercentage
        {
            get
            {
                if (RecipientsCount == 0) return 0;
                return ((double)UniqueLinkClicks / (double)RecipientsCount);
            }
        }

        public virtual double OpenedPercentage
        {
            get
            {
                if (RecipientsCount == 0) return 0;
                return ((double)UniqueOpens / (double)RecipientsCount);
            }
        }

        public virtual double BouncedPercentage
        {
            get
            {
                if (RecipientsCount == 0) return 0;
                return ((double)TotalBounces / (double)RecipientsCount);
            }
        }

        [JsonProperty]
        public virtual int RecipientsCount
        {
            get;
            internal set;
        }

        [JsonProperty]
        public virtual bool IsTransactional
        {
            get;
            internal set;
        }

    }
}
