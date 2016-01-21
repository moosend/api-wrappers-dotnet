using System;
using System.Net.Http;

namespace Moosend.Api.Client
{
    public class ServiceClientContext
    {
        // TODO maybe set default endpoint api v3 or make it a const in MoosendApiClient
        public ServiceClientContext(Uri endpoint, string apiKey)
        {
            if (endpoint == null) throw new ArgumentNullException("endpoint");
            if (apiKey == null) throw new ArgumentNullException("apiKey");

            Endpoint = endpoint;
            ApiKey = apiKey;
            // TODO set right timeout
            Timeout = TimeSpan.FromSeconds(10);
        }

        public Uri Endpoint { get; private set; }
        public TimeSpan Timeout { get; set; }
        public HttpMessageHandler Handler { get; set; }
        public string ApiKey { get; private set; }
    }
}
