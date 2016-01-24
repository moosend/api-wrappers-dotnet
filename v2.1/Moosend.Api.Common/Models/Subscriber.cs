using System;
using System.Collections.Generic;

namespace Moosend.Api.Common.Models
{
    public class Subscriber
    {
        public Subscriber()
        {
            CustomFields = new List<CustomField>();
            SubscribeType = SubscribeType.Subscribed;
        }

        public Guid Id
        {
            get;
            internal set;
        }

        public string Name
        {
            get;
            set;
        }

        public string Email
        {
            get;
            set;
        }

        public DateTime CreatedOn
        {
            get;
            internal set;
        }

        public DateTime? UnsubscribedOn
        {
            get;
            internal set;
        }

        public Guid? UnsubscribedFromId
        {
            get;
            internal set;
        }

        public SubscribeType SubscribeType
        {
            get;
            internal set;
        }

        public IList<CustomField> CustomFields
        {
            get;
            internal set;
        }
    }
}
