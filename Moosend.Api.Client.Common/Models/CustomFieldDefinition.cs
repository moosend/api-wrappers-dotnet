using System;

namespace Moosend.Api.Common.Models
{
    public class CustomFieldDefinition
    {
        /// <summary>
        ///     The id of the custom field.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        ///     The name of the custom field.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     The context of the custom field. This will be null if not singleSelectDropDown.
        /// </summary>
        public string Context { get; set; }

        /// <summary>
        ///     False if the custom field is not required, true if it is.
        /// </summary>
        public bool IsRequired { get; set; }

        /// <summary>
        ///     0 for text, 1 for decimal, 2 for dateTime, 3 for singleSelectDropDown, 4 for integer, 5 for checkbox.
        /// </summary>
        public CustomFieldType Type { get; set; }
    }
}
