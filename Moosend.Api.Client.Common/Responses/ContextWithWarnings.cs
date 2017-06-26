using System;
using System.Collections.Generic;
using Moosend.Api.Common.Models;

namespace Moosend.Api.Common.Responses
{
    public class ContextWithWarnings
    {
        public IList<MessageWarning> Messages { get; set; }
    }
}