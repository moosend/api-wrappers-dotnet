using System;
using System.Collections.Generic;
using System.Web;
using Newtonsoft.Json;
using Moosend.API.Client.Models;

namespace Moosend.API.Client.Serialization
{
    internal class SerializableCampaignSummaryCollection : SerializablePagedList<CampaignSummary>
    {
        [JsonProperty("Campaigns")]
        public new PagedList<CampaignSummary> PagedList
        {
            get { return base.PagedList; }
            set { base.PagedList = value; }
        }
    }

}