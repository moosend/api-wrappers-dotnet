using System.Collections.Generic;
using Moosend.Api.Common.Models;

namespace Moosend.Api.Common.Responses
{
    public class SegmentsResponse : PagedResponse
    {
         public IEnumerable<Segment> Segments { get; set; } 
    }
}