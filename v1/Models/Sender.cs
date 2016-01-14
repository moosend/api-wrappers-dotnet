using System;
using Newtonsoft.Json;

namespace Moosend.API.Client.Models
{
    public class Sender
    {
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
            internal set;
        }

        [JsonProperty]
        public virtual String Email
        {
            get;
            internal set;
        }

        [JsonProperty]
        public virtual DateTime CreatedOn
        {
            get;
            internal set;
        }

        [JsonProperty]
        public virtual Boolean IsEnabled
        {
            get;
            internal set;
        }

        public virtual String Display
        {
            get
            {
                if (String.IsNullOrEmpty(Name)) return Email;
                return String.Format("{0} ({1})", Name, Email);
            }
        }
    }
}
