using System;
using System.Net.Http;

namespace Moosend.Api.Client
{
    public class ServiceClientContext
    {
        public ServiceClientContext(Uri endpoint)
        {
            if (endpoint == null) throw new ArgumentNullException("endpoint");

            Endpoint = endpoint;
            // TODO set right timeout
            Timeout = TimeSpan.FromSeconds(10);
        }

        public Uri Endpoint { get; private set; }
        public TimeSpan Timeout { get; set; }
        public HttpMessageHandler Handler { get; set; }
    }
}
