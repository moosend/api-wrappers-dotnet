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

        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? UnsubscribedOn { get; set; }

        public Guid? UnsubscribedFromId { get; set; }

        public SubscribeType SubscribeType { get; set; }

        public IList<CustomField> CustomFields { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public SubscribeMethod SubscribeMethod { get; set; }
        public DateTime? RemovedOn { get; set; }
    }
}
