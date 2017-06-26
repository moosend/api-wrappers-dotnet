using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Moosend.Api.Tests
{
    public class TestHttpMessageHandler : HttpMessageHandler
    {
        private readonly HttpStatusCode _code;
        private readonly object _responseContent;

        public TestHttpMessageHandler(HttpStatusCode code = HttpStatusCode.NoContent, object responseContent = null)
        {
            _responseContent = responseContent;
            _code = code;
            Payloads = new List<string>();
            Requests = new List<HttpRequestMessage>();
        }

        public List<HttpRequestMessage> Requests { get; }

        public List<string> Payloads { get; }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            Requests.Add(request);
            if (request.Content != null) Payloads.Add(request.Content.ReadAsStringAsync().Result);

            return
                Task.FromResult(new HttpResponseMessage(_code)
                {
                    Content =
                        new StringContent(_responseContent == null
                            ? "{}"
                            : JsonConvert.SerializeObject(_responseContent))
                });
        }
    }
}