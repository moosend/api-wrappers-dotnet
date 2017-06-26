using System;

namespace Moosend.Api.Common.Models
{
    public class CustomField
    {
        /// <summary>
        ///     The id of the custom field.
        /// </summary>
        public Guid CustomFieldId { get; set; }

        /// <summary>
        ///     The value defined for each email for the specific custom field.
        /// </summary>
        public string Value { get; set; }
    }
}