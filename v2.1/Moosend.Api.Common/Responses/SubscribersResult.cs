using System.Collections.Generic;
using Moosend.Api.Common.Models;

namespace Moosend.Api.Common.Responses
{
    public class SubscribersResult : PagedResponse
    {
        public IList<Subscriber> Subscribers { get; set; }
    }
}