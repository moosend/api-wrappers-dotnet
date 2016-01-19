using System;
using Newtonsoft.Json;

namespace Moosend.Api.Common.Models
{
    public class CampaignSummary
    {
        public Guid Id
        {
            get;
            internal set;
        }

        public string Name
        {
            get;
            internal set;
        }

        public string Subject
        {
            get;
            internal set;
        }

        public string SiteName
        {
            get;
            internal set;
        }

        public string ConfirmationTo
        {
            get;
            internal set;
        }

        public DateTime CreatedOn
        {
            get;
            internal set;
        }

        public int? AbHoursToTest
        {
            get;
            internal set;
        }

        public AbCampaignType? AbCampaignType
        {
            get;
            internal set;
        }

        public AbWinnerSelectionType AbWinnerSelectionType
        {
            get;
            internal set;
        }

        public CampaignStatus Status
        {
            get;
            internal set;
        }

        public DateTime? DeliveredOn
        {
            get;
            internal set;
        }

        public DateTime? ScheduledFor
        {
            get;
            internal set;
        }

        public string ScheduledForTimezone
        {
            get;
            internal set;
        }

        public Guid? MailingListId
        {
            get;
            internal set;
        }

        public string MailingListName
        {
            get;
            internal set;
        }

        public int SegmentId
        {
            get;
            internal set;
        }

        public string SegmentName
        {
            get;
            internal set;
        }

        public MailingListStatus MailingListStatus
        {
            get;
            internal set;
        }

        public int TotalSent
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

        public double LinkClicksPercentage
        {
            get
            {
                if (RecipientsCount == 0) return 0;
                return ((double)UniqueLinkClicks / (double)RecipientsCount);
            }
        }

        public double OpenedPercentage
        {
            get
            {
                if (RecipientsCount == 0) return 0;
                return ((double)UniqueOpens / (double)RecipientsCount);
            }
        }

        public double BouncedPercentage
        {
            get
            {
                if (RecipientsCount == 0) return 0;
                return ((double)TotalBounces / (double)RecipientsCount);
            }
        }

        public int RecipientsCount
        {
            get;
            internal set;
        }

        public bool IsTransactional
        {
            get;
            internal set;
        }

    }
}
