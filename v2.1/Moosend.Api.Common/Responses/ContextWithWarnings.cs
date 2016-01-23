using System;
using System.Collections.Generic;
using Moosend.Api.Common.Models;

namespace Moosend.Api.Common.Responses
{
    public class ContextWithWarnings
    {
        public Guid Id { get; set; }
        public IList<MessageWarning> Messages { get; set; }
    }
}