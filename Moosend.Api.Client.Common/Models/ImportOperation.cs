using System;

namespace Moosend.Api.Common.Models
{
    public class ImportOperation
    {
        /// <summary>
        ///     The id of the import operation.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        ///     A guid id for the import operation.
        /// </summary>
        public Guid DataHash { get; set; }

        /// <summary>
        ///     The mappings that where used for the specific import operation.
        /// </summary>
        public string Mappings { get; set; }

        /// <summary>
        ///     Null if the import operation notification email was not selected to be sent.
        /// </summary>
        public string EmailNotify { get; set; }

        /// <summary>
        ///     The date time that the import operation was created.
        /// </summary>
        public DateTime CreatedOn { get; set; }

        /// <summary>
        ///     The date time that the import operation was started.
        /// </summary>
        public DateTime StartedOn { get; set; }

        /// <summary>
        ///     The date time that the import operation was completed.
        /// </summary>
        public DateTime CompletedOn { get; set; }

        /// <summary>
        ///     The number of the total inserted emails.
        /// </summary>
        public int TotalInserted { get; set; }

        /// <summary>
        ///     The number of the total emails that where updated.
        /// </summary>
        public int TotalUpdated { get; set; }

        /// <summary>
        ///     The number of the total emails that where unsubscribed.
        /// </summary>
        public int TotalUnsubscribed { get; set; }

        /// <summary>
        ///     The number of the total invalid emails.
        /// </summary>
        public int TotalInvalid { get; set; }

        /// <summary>
        ///     The number of the total ignored emails.
        /// </summary>
        public int TotalIgnored { get; set; }

        /// <summary>
        ///     The number of the total duplicate emails.
        /// </summary>
        public int TotalDuplicate { get; set; }

        /// <summary>
        ///     The number of the total members that where selected to be imported.
        /// </summary>
        public int TotalMembers { get; set; }

        /// <summary>
        ///     The message of the import operation. This will be null if successful.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        ///     This will be true if successful.
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        ///     This will be false if the skip new members option was not selected before the import operation began.
        /// </summary>
        public string SkipNewMembers { get; set; }
    }
}