using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Moosend.API.Client.Models
{
    public class MailingList
    {
        public MailingList()
        {
            CustomFieldsDefinition = new List<CustomFieldDefinition>();
        }

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

        public virtual long AllMemberCount
        {
            get
            {
                return ActiveMemberCount + BouncedMemberCount + RemovedMemberCount + UnsubscribedMemberCount;
            }
        }

        [JsonProperty]
        public virtual long ActiveMemberCount
        {
            get;
            internal set;
        }

        [JsonProperty]
        public virtual long BouncedMemberCount
        {
            get;
            internal set;
        }

        [JsonProperty]
        public virtual long RemovedMemberCount
        {
            get;
            internal set;
        }

        [JsonProperty]
        public virtual long UnsubscribedMemberCount
        {
            get;
            internal set;
        }

        [JsonProperty]
        public virtual MailingListStatus Status
        {
            get;
            internal set;
        }

        [JsonProperty]
        public virtual IList<CustomFieldDefinition> CustomFieldsDefinition
        {
            get;
            private set;
        }

        [JsonProperty]
        public virtual string CreatedBy
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
        public virtual string UpdatedBy
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

        public virtual String ConfirmationPage
        {
            get;
            set;
        }

        public virtual String RedirectAfterUnsubscribePage
        {
            get;
            set;
        }
    }
}
