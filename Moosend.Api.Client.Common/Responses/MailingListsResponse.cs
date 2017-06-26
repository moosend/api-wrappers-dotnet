using System.Collections.Generic;
using Moosend.Api.Common.Models;

namespace Moosend.Api.Common.Responses
{
    public class MailingListsResponse : PagedResponse
    {
         public IEnumerable<MailingList> MailingLists { get; set; } 
    }
}