using System;

namespace Moosend.Api.Common.Models
{
    public class Sender
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime CreatedOn { get; set; }
        public bool IsEnabled { get; set; }
        public bool SpfVerified { get; set; }
        public bool DkimVerified { get; set; }
        public string DkimPublic { get; set; }

        public string Display
        {
            get
            {
                if (string.IsNullOrEmpty(Name)) return Email;
                return string.Format("{0} ({1})", Name, Email);
            }
        }
    }
}
