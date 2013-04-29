using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Moosend.API.Client
{
    public class ApiException : Exception
    {
        public ApiException(string message) : base(message)
        {
        }
    }
}
