using System;

namespace Moosend.Api.Common.Models
{
    public struct ContextAnalyticsNode
    {
        public string Context
        {
            get;
            internal set;
        }

        public string ContextName
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

        public string ContextDescription
        {
            get;
            internal set;
        }
    }
}
