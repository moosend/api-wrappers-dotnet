using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Moosend.API.Client.Models
{
    public class SubscriberParams
    {
        public SubscriberParams()
        {
            CustomFields = new Dictionary<String, String>();
        }

        [JsonProperty]
        public virtual String Name
        {
            get;
            set;
        }

        [JsonProperty]
        public virtual String Email
        {
            get;
            set;
        }

        [JsonProperty]
        public virtual IDictionary<String, String> CustomFields
        {
            get;
            set;
        }
    }
}
