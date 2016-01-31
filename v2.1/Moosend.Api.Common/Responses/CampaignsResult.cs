using System.Collections.Generic;
using Moosend.Api.Common.Models;

namespace Moosend.Api.Common.Responses
{
    public class CampaignsResult : PagedResponse
    {
        public IEnumerable<CampaignSummary> Campaigns { get; set; }
    }
}