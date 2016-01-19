using System;
using System.Collections.Generic;

namespace Moosend.Api.Common.Models
{
    public class MailingList
    {
        public MailingList()
        {
            CustomFieldsDefinition = new List<CustomFieldDefinition>();
        }

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

        public long AllMemberCount
        {
            get
            {
                return ActiveMemberCount + BouncedMemberCount + RemovedMemberCount + UnsubscribedMemberCount;
            }
        }

        public long ActiveMemberCount
        {
            get;
            set;
        }

        public long BouncedMemberCount
        {
            get;
            set;
        }

        public long RemovedMemberCount
        {
            get;
            set;
        }

        public long UnsubscribedMemberCount
        {
            get;
            set;
        }

        public MailingListStatus Status
        {
            get;
            set;
        }

        public IList<CustomFieldDefinition> CustomFieldsDefinition
        {
            get;
            set;
        }

        public string CreatedBy
        {
            get;
            set;
        }

        public DateTime CreatedOn
        {
            get;
            set;
        }

        public string UpdatedBy
        {
            get;
            set;
        }

        public DateTime UpdatedOn
        {
            get;
            set;
        }

        public string ConfirmationPage
        {
            get;
            set;
        }

        public string RedirectAfterUnsubscribePage
        {
            get;
            set;
        }
    }
}
