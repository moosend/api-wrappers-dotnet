using System.Collections.Generic;
using Moosend.Api.Common.Models;

namespace Moosend.Api.Common.Responses
{
    public class CampaignsStatisticsResult : PagedResponse
    {
        public IList<AnalyticsDetails> Analytics { get; set; }
    }
}