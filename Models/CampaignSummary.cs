using System;
using System.Runtime.Serialization;

namespace Moosend.API.Client.Models
{
    [Serializable]
    [DataContract(Namespace = "")]
    public class CampaignSummary
    {
        [DataMember]
        public virtual Guid ID
        {
            get;
            internal set;
        }

        [DataMember]
        public virtual String Name
        {
            get;
            internal set;
        }

        [DataMember]
        public virtual String Subject
        {
            get;
            internal set;
        }

        [DataMember]
        public virtual string SiteName
        {
            get;
            internal set;
        }

        [DataMember]
        public virtual String ConfirmationTo
        {
            get;
            internal set;
        }

        [DataMember]
        public virtual DateTime CreatedOn
        {
            get;
            internal set;
        }

        [DataMember]
        public virtual int? ABHoursToTest
        {
            get;
            internal set;
        }

        [DataMember]
        public virtual ABCampaignType? ABCampaignType
        {
            get;
            internal set;
        }

        [DataMember]
        public virtual ABWinnerSelectionType ABWinnerSelectionType
        {
            get;
            internal set;
        }

        [DataMember]
        public virtual CampaignStatus Status
        {
            get;
            internal set;
        }

        [DataMember]
        public virtual DateTime? DeliveredOn
        {
            get;
            internal set;
        }

        [DataMember]
        public virtual DateTime? ScheduledFor
        {
            get;
            internal set;
        }

        [DataMember]
        public virtual String ScheduledForTimezone
        {
            get;
            internal set;
        }

        [DataMember]
        public virtual Guid? MailingListID
        {
            get;
            internal set;
        }

        [DataMember]
        public virtual String MailingListName
        {
            get;
            internal set;
        }

        [DataMember]
        public virtual int SegmentID
        {
            get;
            internal set;
        }

        [DataMember]
        public virtual String SegmentName
        {
            get;
            internal set;
        }

        [DataMember]
        public virtual MailingListStatus MailingListStatus
        {
            get;
            internal set;
        }

        [DataMember]
        public virtual int TotalSent
        {
            get;
            internal set;
        }

        [DataMember]
        public virtual int TotalOpens
        {
            get;
            internal set;
        }

        [DataMember]
        public virtual int UniqueOpens
        {
            get;
            internal set;
        }

        [DataMember]
        public virtual int TotalBounces
        {
            get;
            internal set;
        }

        [DataMember]
        public virtual int TotalForwards
        {
            get;
            internal set;
        }

        [DataMember]
        public virtual int TotalLinkClicks
        {
            get;
            internal set;
        }

        [DataMember]
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

        [DataMember]
        public virtual int RecipientsCount
        {
            get;
            internal set;
        }

        [DataMember]
        public virtual bool IsTransactional
        {
            get;
            internal set;
        }

    }
}
