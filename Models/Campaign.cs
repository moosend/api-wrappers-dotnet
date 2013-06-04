using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Moosend.API.Client.Models
{
    public class Campaign
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
            set;
        }

        [JsonProperty]
        public virtual String Subject
        {
            get;
            set;
        }

        [JsonProperty]
        public virtual String WebLocation
        {
            get;
            set;
        }

        [JsonProperty]
        public virtual String HTMLContent
        {
            get;
            set;
        }

        [JsonProperty]
        public virtual String PlainContent
        {
            get;
            set;
        }

        [JsonProperty]
        public virtual Sender Sender
        {
            get;
            set;
        }

        [JsonProperty]
        public virtual DateTime? DeliveredOn
        {
            get;
            set;
        }

        [JsonProperty]
        public virtual Sender ReplyToEmail
        {
            get;
            set;
        }

        [JsonProperty]
        public virtual DateTime CreatedOn
        {
            get;
            internal set;
        }

        [JsonProperty]
        public virtual DateTime UpdatedOn
        {
            get;
            internal set;
        }

        [JsonProperty]
        public virtual DateTime? ScheduledFor
        {
            get;
            set;
        }

        [JsonProperty]
        public virtual String Timezone
        {
            get;
            set;
        }

        [JsonProperty]
        public virtual FormatType FormatType
        {
            get;
            set;
        }

        [JsonProperty]
        public virtual ABCampaignData ABCampaignData
        {
            get;
            internal set;
        }

        [JsonProperty]
        public virtual MailingList MailingList
        {
            get;
            set;
        }

        [JsonProperty]
        public virtual String ConfirmationTo
        {
            get;
            set;
        }

        [JsonProperty]
        public virtual CampaignStatus Status
        {
            get;
            internal set;
        }

        [JsonProperty]
        public virtual Segment Segment
        {
            get;
            set;
        }

        [JsonProperty]
        public virtual bool IsTransactional
        {
            get;
            internal set;
        }

    }
}
