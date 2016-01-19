using System;

namespace Moosend.Api.Common.Models
{
    public class Campaign
    {
        public Guid Id
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }

        public string Subject
        {
            get;
            set;
        }

        public string WebLocation
        {
            get;
            set;
        }

        public string HtmlContent
        {
            get;
            set;
        }

        public string PlainContent
        {
            get;
            set;
        }

        public Sender Sender
        {
            get;
            set;
        }

        public DateTime? DeliveredOn
        {
            get;
            set;
        }

        public Sender ReplyToEmail
        {
            get;
            set;
        }

        public DateTime CreatedOn
        {
            get;
            internal set;
        }

        public DateTime UpdatedOn
        {
            get;
            internal set;
        }

        public DateTime? ScheduledFor
        {
            get;
            set;
        }

        public string Timezone
        {
            get;
            set;
        }

        public FormatType FormatType
        {
            get;
            set;
        }

        public AbCampaignData AbCampaignData
        {
            get;
            internal set;
        }

        public MailingList MailingList
        {
            get;
            set;
        }

        public string ConfirmationTo
        {
            get;
            set;
        }

        public CampaignStatus Status
        {
            get;
            internal set;
        }

        public Segment Segment
        {
            get;
            set;
        }

        public bool IsTransactional
        {
            get;
            internal set;
        }

    }
}
