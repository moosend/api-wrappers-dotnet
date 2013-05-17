using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Moosend.API.Client.Models
{
    [Serializable]
    [DataContract(Namespace = "")]
    public class Subscriber
    {
        public Subscriber()
        {
            CustomFields = new List<CustomField>();
            SubscribeType = SubscribeType.Subscribed;
        }

        [DataMember]
        public virtual Guid ID
        {
            get;
            internal set;
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
        public virtual DateTime CreatedOn
        {
            get;
            internal set;
        }

        [DataMember]
        public virtual DateTime? UnsubscribedOn
        {
            get;
            internal set;
        }

        [DataMember]
        public virtual Guid? UnsubscribedFromID
        {
            get;
            internal set;
        }

        [DataMember]
        public virtual SubscribeType SubscribeType
        {
            get;
            internal set;
        }

        [DataMember]
        public virtual IList<CustomField> CustomFields
        {
            get;
            internal set;
        }
    }
}
