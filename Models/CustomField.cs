using System;
using Newtonsoft.Json;

namespace Moosend.API.Client.Models
{
    public class CustomField
    {
        public virtual Guid SubscriberID
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
        public virtual String Value
        {
            get;
            set;
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            
            var t = obj as CustomField;
            
            if (t == null) return false;
            
            if (SubscriberID == t.SubscriberID && Name == t.Name) return true;
            
            return false;
        }

        public override int GetHashCode()
        {
            return (SubscriberID.ToString() + Name).GetHashCode();
        }  
    }
}
