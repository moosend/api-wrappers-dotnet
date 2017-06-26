using System;
using System.Collections.Generic;

namespace Moosend.Api.Common.Models
{
    public class MailingList
    {
        /// <summary>
        ///     The id of the mailing list.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        ///     The name of the mailing list.
        /// </summary>
        public string Name { get; set; }

        public long AllMemberCount
        {
            get { return ActiveMemberCount + BouncedMemberCount + RemovedMemberCount + UnsubscribedMemberCount; }
        }

        /// <summary>
        ///     The number of the active members for this mailing list.
        /// </summary>
        public long ActiveMemberCount { get; set; }

        /// <summary>
        ///     The number of the bounced emails for this mailing list.
        /// </summary>
        public long BouncedMemberCount { get; set; }

        /// <summary>
        ///     The number of the removed emails for this mailing list.
        /// </summary>
        public long RemovedMemberCount { get; set; }

        /// <summary>
        ///     The number of the unsubscribed emails for this mailing list.
        /// </summary>
        public long UnsubscribedMemberCount { get; set; }

        /// <summary>
        ///     The status of the mailing list. For created this will be 0. For imported it will be 1. For importing it will be 2
        ///     is for and 3 for deleted.
        /// </summary>
        public MailingListStatus Status { get; set; }

        /// <summary>
        ///     The details of the custom fields for the requested mailing list.
        /// </summary>
        public IList<CustomFieldDefinition> CustomFieldsDefinition { get; set; }

        /// <summary>
        ///     The ip from where the requested mailing list was created
        /// </summary>
        public string CreatedBy { get; set; }

        /// <summary>
        ///     The date time that the requested mailing list was created
        /// </summary>
        public DateTime CreatedOn { get; set; }

        /// <summary>
        ///     The ip from where the requested mailing list was updated
        /// </summary>
        public string UpdatedBy { get; set; }

        /// <summary>
        ///     The date time that the requested mailing list was updated
        /// </summary>
        public DateTime UpdatedOn { get; set; }

        /// <summary>
        ///     The details of the latest import operation that was performed in the requested mailing list. This will be blank if
        ///     no import operation was performed for this list.
        /// </summary>
        public ImportOperation ImportOperation { get; set; }
    }
}