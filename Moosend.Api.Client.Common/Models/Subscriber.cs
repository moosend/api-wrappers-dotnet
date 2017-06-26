using System;
using System.Collections.Generic;

namespace Moosend.Api.Common.Models
{
    public class Subscriber
    {
        /// <summary>
        ///     The id of the subscriber.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        ///     The name of the subscriber.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     The email of the subscriber.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        ///     The date time on witch the subscriber was added to the requested mailing list.
        /// </summary>
        public DateTime CreatedOn { get; set; }

        /// <summary>
        ///     The date time on witch the subscriber was updated to the requested mailing list.
        /// </summary>
        public DateTime? UpdatedOn { get; set; }

        /// <summary>
        ///     The date time on witch the subscriber was unsubscribed from the requested mailing list. This will be null if not
        ///     unsubscribed.
        /// </summary>
        public DateTime? UnsubscribedOn { get; set; }

        /// <summary>
        ///     The ID from where the member was unsubscribed from.
        /// </summary>
        public Guid? UnsubscribedFromId { get; set; }

        /// <summary>
        ///     This will show the current status of the subscriber.
        /// </summary>
        public SubscribeType SubscribeType { get; set; }

        /// <summary>
        ///     The method from witch the member was subscribed to the mailing list. 0 for subscription form, 1 for file import, 2
        ///     for manually added.
        /// </summary>
        public SubscribeMethod? SubscribeMethod { get; set; }

        /// <summary>
        ///     A list with all the custom fields for the requested mailing list.
        /// </summary>
        public IList<CustomField> CustomFields { get; set; }

        /// <summary>
        /// The date time on witch the subscriber was removed from the requested mailing list. This will be null if not
        ///     removed.
        /// </summary>
        public DateTime? RemovedOn { get; set; }
    }
}
