using System;

namespace Moosend.Api.Common.Requests
{
    public class CreateCustomFieldRequest
    {
        private const int MaxLength = 80;
        private string _name;

        public CreateCustomFieldRequest(string name, CustomFieldType customFieldType, string options, bool isRequired)
        {
            if (name.Length > MaxLength) throw new ArgumentException(string.Format("Custom Field's name length cannot be greater than {0} characters.", MaxLength));

            Name = name;
            CustomFieldType = customFieldType;
            Options = options;
            IsRequired = isRequired;
        }

        /// <summary>
        /// The name of the custom field
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Specifies the data type of the custom field. This must be one of the following values:
        /// <ul>
        /// <li><strong><pre>Text</pre></strong> : to accept any text as input</li>
        /// <li><strong><pre>Number</pre></strong> : to accept only integer or decimal values as input</li>
        /// <li><strong><pre>DateTime</pre></strong> : to accept only date values as input, with or without time</li>
        /// <li><strong><pre>SingleSelectDropdown</pre></strong> : to accept only values explicitly defined in a list</li>
        /// <li><strong><pre>CheckBox</pre></strong> : to accept only values of true or false</li>
        /// </ul>
        /// If ommitted, <pre>Text</pre> will be assumed.
        /// </summary>
        public CustomFieldType CustomFieldType { get; private set; }

        /// <summary>
        /// If you want to create a custom field of type <pre>SingleSelectDropdown</pre>, you must set this parameter to specify the available options for the user to choose from.
        /// Use a comma (,) to seperate different options.
        /// </summary>
        public string Options { get; private set; }

        /// <summary>
        /// Specify whether this is field will be mandatory on not, when being used in a subscription form. You should specify a value of either <pre>true</pre>true or <pre>false</pre>. If ommitted, <pre>false</pre> will be assumed.
        /// </summary>
        public bool IsRequired { get; private set; }
    }
}