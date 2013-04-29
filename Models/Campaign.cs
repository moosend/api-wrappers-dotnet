using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Moosend.API.Client.Models
{
    [Serializable]
    [DataContract(Namespace = "")]
    public class Campaign
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
            set;
        }

        [DataMember]
        public virtual String Subject
        {
            get;
            set;
        }

        [DataMember]
        public virtual String WebLocation
        {
            get;
            set;
        }

        [DataMember]
        public virtual String HTMLContent
        {
            get;
            set;
        }

        [DataMember]
        public virtual String PlainContent
        {
            get;
            set;
        }

        [DataMember]
        public virtual Sender Sender
        {
            get;
            set;
        }

        [DataMember]
        public virtual DateTime? DeliveredOn
        {
            get;
            set;
        }

        [DataMember]
        public virtual Sender ReplyToEmail
        {
            get;
            set;
        }

        [DataMember]
        public virtual DateTime CreatedOn
        {
            get;
            internal set;
        }

        [DataMember]
        public virtual DateTime UpdatedOn
        {
            get;
            internal set;
        }

        [DataMember]
        public virtual DateTime? ScheduledFor
        {
            get;
            set;
        }

        [DataMember]
        public virtual String Timezone
        {
            get;
            set;
        }

        [DataMember]
        public virtual FormatType FormatType
        {
            get;
            set;
        }

        [DataMember]
        public virtual ABCampaignData ABCampaignData
        {
            get;
            internal set;
        }

        [DataMember]
        public virtual MailingList MailingList
        {
            get;
            set;
        }

        [DataMember]
        public virtual String ConfirmationTo
        {
            get;
            set;
        }

        [DataMember]
        public virtual CampaignStatus Status
        {
            get;
            internal set;
        }

        [DataMember]
        public virtual Segment Segment
        {
            get;
            set;
        }

        [DataMember]
        public virtual bool IsTransactional
        {
            get;
            internal set;
        }

    }
}
