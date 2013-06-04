using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Moosend.API.Client.Models;

namespace Moosend.API.Client.Serialization
{
    internal class SerializableMailingListCollection : SerializablePagedList<MailingList>
    {
        [JsonProperty("MailingLists")]
        public new PagedList<MailingList> PagedList
        {
            get { return base.PagedList; }
            set { base.PagedList = value; }
        }
    }
}
