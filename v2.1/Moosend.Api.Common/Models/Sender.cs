using System;
using Newtonsoft.Json;

namespace Moosend.Api.Common.Models
{
    public class Sender
    {
        public Guid Id
        {
            get;
            internal set;
        }

        public string Name
        {
            get;
            internal set;
        }

        public string Email
        {
            get;
            internal set;
        }

        public DateTime CreatedOn
        {
            get;
            internal set;
        }

        public bool IsEnabled
        {
            get;
            internal set;
        }

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
