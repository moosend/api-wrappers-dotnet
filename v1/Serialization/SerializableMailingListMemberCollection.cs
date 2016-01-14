using System;
using System.Collections.Generic;
using System.Web;
using Newtonsoft.Json;
using Moosend.API.Client.Models;

namespace Moosend.API.Client.Serialization
{
    internal class SerializableMailingListMemberCollection : SerializablePagedList<Subscriber>
    {
        [JsonProperty("Subscribers")]
        public new PagedList<Subscriber> PagedList
        {
            get { return base.PagedList; }
            set { base.PagedList = value; }
        }
    }
}
