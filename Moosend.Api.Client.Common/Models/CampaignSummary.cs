using System;
using System.Collections.Generic;

namespace Moosend.Api.Common.Models
{
    public class CampaignSummary
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Subject { get; set; }
        public string SiteName { get; set; }
        public string ConfirmationTo { get; set; }
        public DateTime CreatedOn { get; set; }
        public int? AbHoursToTest { get; set; }
        public AbCampaignType? AbCampaignType { get; set; }
        public AbWinnerSelectionType? AbWinnerSelectionType { get; set; }
        public CampaignStatus Status { get; set; }
        public DateTime? DeliveredOn { get; set; }
        public DateTime? ScheduledFor { get; set; }
        public string ScheduledForTimezone { get; set; }
        public IList<CampaignMailingList> MailingLists { get; set; }
        public int TotalSent { get; set; }
        public int TotalOpens { get; set; }
        public int UniqueOpens { get; set; }
        public int TotalBounces { get; set; }
        public int TotalForwards { get; set; }
        public int TotalLinkClicks { get; set; }
        public int UniqueLinkClicks { get; set; }
        public AbWinner? AbWinner { get; set; }
        public int UniqueForwards { get; set; }
        public int TotalComplaints { get; set; }
        public int TotalUnsubscribes { get; set; }

        public double LinkClicksPercentage
        {
            get
            {
                if (RecipientsCount == 0) return 0;
                return UniqueLinkClicks / (double)RecipientsCount;
            }
        }

        public double OpenedPercentage
        {
            get
            {
                if (RecipientsCount == 0) return 0;
                return UniqueOpens / (double)RecipientsCount;
            }
        }

        public double BouncedPercentage
        {
            get
            {
                if (RecipientsCount == 0) return 0;
                return TotalBounces / (double)RecipientsCount;
            }
        }

        public int RecipientsCount { get; set; }

        public bool IsTransactional { get; set; }
    }
}