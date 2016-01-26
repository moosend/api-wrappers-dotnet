using System;

namespace Moosend.Api.Common.Models
{
    public class CustomField
    {
        public Guid SubscriberId { get; set; }

        public string Name { get; set; }

        public string Value { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;

            var t = obj as CustomField;

            if (t == null) return false;

            if (SubscriberId == t.SubscriberId && Name == t.Name) return true;

            return false;
        }

        public override int GetHashCode()
        {
            return (SubscriberId + Name).GetHashCode();
        }
    }
}
