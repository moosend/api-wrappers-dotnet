using System;

namespace Moosend.Api.Common.Models
{
    public class CustomFieldDefinition
    {
        public Guid Id
        {
            get;
            internal set;
        }

        public Guid MailingListId
        {
            get;
            internal set;
        }

        public string Name
        {
            get;
            set;
        }

        public string Context
        {
            get;
            set;
        }

        public bool IsRequired
        {
            get;
            set;
        }

        public CustomFieldType Type
        {
            get;
            set;
        }
    }
}
