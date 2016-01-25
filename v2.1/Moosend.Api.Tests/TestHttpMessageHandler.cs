using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Moosend.Api.Tests
{
    public class TestHttpMessageHandler : HttpMessageHandler
    {
        private readonly HttpStatusCode _code;

        public TestHttpMessageHandler(HttpStatusCode code = HttpStatusCode.NoContent)
        {
            _code = code;
            Payloads = new List<string>();
            Requests = new List<HttpRequestMessage>();
        }

        public List<HttpRequestMessage> Requests { get; }

        public List<string> Payloads { get; }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            Requests.Add(request);
            if (request.Content != null) Payloads.Add(request.Content.ReadAsStringAsync().Result);

            return Task.FromResult(new HttpResponseMessage(_code) {Content = new StringContent("{}")});
        }
    }
}