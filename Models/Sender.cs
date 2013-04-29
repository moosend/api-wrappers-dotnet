using System;
using System.Runtime.Serialization;

namespace Moosend.API.Client.Models
{
    [Serializable]
    [DataContract(Namespace = "")]
    public class Sender
    {
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
            internal set;
        }

        [DataMember]
        public virtual String Email
        {
            get;
            internal set;
        }

        [DataMember]
        public virtual DateTime CreatedOn
        {
            get;
            internal set;
        }

        [DataMember]
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
