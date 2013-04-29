using System;
using System.Runtime.Serialization;

namespace Moosend.API.Client.Models
{
    [Serializable]
    [DataContract(Namespace = "")]
    public class CustomField
    {
        public virtual Guid MailingListMemberID
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
        public virtual String Value
        {
            get;
            set;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            var t = obj as CustomField;
            if (t == null)
                return false;
            if (MailingListMemberID == t.MailingListMemberID && Name == t.Name)
                return true;
            return false;
        }

        public override int GetHashCode()
        {
            return (MailingListMemberID.ToString() + Name).GetHashCode();
        }  
    }
}
