using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Moosend.API.Client.Models
{
    public class Subscriber
    {
        public Subscriber()
        {
            CustomFields = new List<CustomField>();
            SubscribeType = SubscribeType.Subscribed;
        }

        [JsonProperty]
        public virtual Guid ID
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
        public virtual String Email
        {
            get;
            set;
        }

        [JsonProperty]
        public virtual DateTime CreatedOn
        {
            get;
            internal set;
        }

        [JsonProperty]
        public virtual DateTime? UnsubscribedOn
        {
            get;
            internal set;
        }

        [JsonProperty]
        public virtual Guid? UnsubscribedFromID
        {
            get;
            internal set;
        }

        [JsonProperty]
        public virtual SubscribeType SubscribeType
        {
            get;
            internal set;
        }

        [JsonProperty]
        public virtual IList<CustomField> CustomFields
        {
            get;
            internal set;
        }
    }
}
