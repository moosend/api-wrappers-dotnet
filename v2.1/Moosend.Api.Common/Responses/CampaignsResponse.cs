﻿using System.Collections.Generic;
using Moosend.Api.Common.Models;

namespace Moosend.Api.Common.Responses
{
    public class PagedCampaigns : PagedResponse
    {
        public IList<CampaignSummary> Campaigns { get; set; }
    }
}