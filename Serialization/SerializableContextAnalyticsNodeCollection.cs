using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Moosend.API.Client.Models;

namespace Moosend.API.Client.Serialization
{
    internal class SerializableContextAnalyticsNodeCollection : SerializablePagedList<ContextAnalyticsNode>
    {
        [JsonProperty("Analytics")]
        public new PagedList<ContextAnalyticsNode> PagedList
        {
            get { return base.PagedList; }
            set { base.PagedList = value; }
        }
    }
}
