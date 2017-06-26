using System.Collections.Generic;

namespace Moosend.Api.Common.Models
{
    public class SubscriberParams
    {
        public SubscriberParams()
        {
            CustomFields = new Dictionary<string, string>();
        }

        /// <summary>
        /// The name of the member.
        /// </summary>
        public string Name
        {
            get;
            set;
        }

        /// <summary>
        /// The email address of the member.
        /// </summary>
        public string Email
        {
            get;
            set;
        }

        /// <summary>
        ///     Name-value pairs that match the member's custom fields defined in the mailing list.
        ///     For example, if you have two custom fields named Age and Country, you should specify values for them as following:
        ///     CustomFields = new Dictionary&lt;string, string>()
        ///        {
        ///            {"Age", "30"},
        ///            {"Country", "GREECE" }
        ///        }
        /// </summary>
        public IDictionary<string, string> CustomFields
        {
            get;
            set;
        }
    }
}
