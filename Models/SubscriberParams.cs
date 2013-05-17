using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Moosend.API.Client.Models
{
    public class SubscriberParams
    {
        public SubscriberParams()
        {
            CustomFields = new Dictionary<String, String>();
        }

        [DataMember]
        public virtual String Name
        {
            get;
            set;
        }

        [DataMember]
        public virtual String Email
        {
            get;
            set;
        }

        [DataMember]
        public virtual IDictionary<String, String> CustomFields
        {
            get;
            set;
        }
    }
}
