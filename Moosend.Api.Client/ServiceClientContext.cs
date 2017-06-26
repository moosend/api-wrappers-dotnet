using System;
using System.Net.Http;

namespace Moosend.Api.Client
{
    public class ServiceClientContext
    {
        public ServiceClientContext(Uri endpoint = null)
        {
            Endpoint = endpoint ?? new Uri("https://api.moosend.com/v3");
            Timeout = TimeSpan.FromSeconds(10);
        }

        public Uri Endpoint { get; private set; }
        public TimeSpan Timeout { get; set; }
        public HttpMessageHandler Handler { get; set; }
    }
}
