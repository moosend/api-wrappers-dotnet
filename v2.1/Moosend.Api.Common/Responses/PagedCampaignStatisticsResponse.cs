using System.Collections.Generic;
using Moosend.Api.Common.Models;

namespace Moosend.Api.Common.Responses
{
    public class PagedCampaignStatisticsResponse : PagedResponse
    {
        public IList<Analytics> Analytics { get; set; }
    }
}