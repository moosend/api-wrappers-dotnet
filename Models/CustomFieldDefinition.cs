using System;
using Newtonsoft.Json;

namespace Moosend.API.Client.Models
{
    public class CustomFieldDefinition
    {
        [JsonProperty]
        public virtual Guid ID
        {
            get;
            internal set;
        }

        public virtual Guid MailingListID
        {
            get;
            internal set;
        }

        [JsonProperty]
        public virtual String Name
        {
            get;
            set;
        }

        [JsonProperty]
        public virtual String Context
        {
            get;
            set;
        }

        [JsonProperty]
        public virtual Boolean IsRequired
        {
            get;
            set;
        }

        [JsonProperty]
        public virtual CustomFieldType Type
        {
            get;
            set;
        }
    }
}
