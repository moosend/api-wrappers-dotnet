using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Moosend.API.Client.Models
{
    public struct ContextAnalyticsNode
    {
        public String Context
        {
            get;
            internal set;
        }

        public String ContextName
        {
            get;
            internal set;
        }

        public int TotalCount
        {
            get;
            internal set;
        }

        public int UniqueCount
        {
            get;
            internal set;
        }

        public String ContextDescription
        {
            get;
            internal set;
        }
    }
}
