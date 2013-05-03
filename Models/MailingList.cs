using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Moosend.API.Client.Models
{
    [Serializable]
    [DataContract(Namespace = "")]
    public class MailingList
    {
        public MailingList()
        {
            CustomFieldsDefinition = new List<CustomFieldDefinition>();
        }

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

        public virtual long AllMemberCount
        {
            get
            {
                return ActiveMemberCount + BouncedMemberCount + RemovedMemberCount + UnsubscribedMemberCount;
            }
        }

        [DataMember]
        public virtual long ActiveMemberCount
        {
            get;
            internal set;
        }

        [DataMember]
        public virtual long BouncedMemberCount
        {
            get;
            internal set;
        }

        [DataMember]
        public virtual long RemovedMemberCount
        {
            get;
            internal set;
        }

        [DataMember]
        public virtual long UnsubscribedMemberCount
        {
            get;
            internal set;
        }

        [DataMember]
        public virtual MailingListStatus Status
        {
            get;
            internal set;
        }

        [DataMember]
        public virtual IList<CustomFieldDefinition> CustomFieldsDefinition
        {
            get;
            private set;
        }

        [DataMember]
        public virtual string CreatedBy
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
        public virtual string UpdatedBy
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
