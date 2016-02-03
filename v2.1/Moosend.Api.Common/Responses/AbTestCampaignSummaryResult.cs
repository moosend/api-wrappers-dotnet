using System;
using Moosend.Api.Common.Models;

namespace Moosend.Api.Common.Responses
{
    public class AbTestCampaignSummaryResult
    {
        public Guid CampaignId { get; set; }
        public ABCampaign A { get; set; }
        public ABCampaign B { get; set; }
    }
}