using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;
using Moosend.API.Client.Models;

namespace Moosend.API.Client.Serialization
{
    [Serializable]
    [DataContract(Namespace = "")]
    public class SerializableCampaignSummaryCollection : SerializablePagedList<CampaignSummary>
    {
        [DataMember(Name = "Campaigns")]
        public new PagedList<CampaignSummary> PagedList
        {
            get { return base.PagedList; }
            set { base.PagedList = value; }
        }
    }

}