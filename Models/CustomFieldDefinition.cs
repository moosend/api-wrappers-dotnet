using System;
using System.Runtime.Serialization;

namespace Moosend.API.Client.Models
{
    [Serializable]
    [DataContract(Namespace = "")]
    public class CustomFieldDefinition
    {
        [DataMember]
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

        [DataMember]
        public virtual String Name
        {
            get;
            set;
        }

        [DataMember]
        public virtual String Context
        {
            get;
            set;
        }

        [DataMember]
        public virtual Boolean IsRequired
        {
            get;
            set;
        }

        [DataMember]
        public virtual CustomFieldType Type
        {
            get;
            set;
        }
    }
}
