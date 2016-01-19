using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Moosend.Api.Common.Models;
using Moosend.Api.Common.Responses;
using Newtonsoft.Json;

namespace Moosend.Api.Client
{
    public class MoosendApiClient
    {
        private readonly HttpClient _httpClient;
        private readonly ServiceClientContext _serviceClientContext;

        public MoosendApiClient(ServiceClientContext context)
        {
            _serviceClientContext = context;
            _httpClient = context.Handler == null
                ? new HttpClient(new HttpClientHandler {AutomaticDecompression = DecompressionMethods.GZip})
                : new HttpClient(context.Handler);

            _httpClient.Timeout = context.Timeout;
            _httpClient.BaseAddress = context.Endpoint;
            _httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
            _httpClient.DefaultRequestHeaders.Add("User-Agent",
                string.Format("moosend-api-client-{0}-{1}", Environment.Version, Environment.OSVersion));
            _httpClient.DefaultRequestHeaders.Add("Keep-Alive", "false");
        }

        #region Campaigns

        public async Task<MailingList> FindByIdAsync(Guid campaignId,
            CancellationToken token = default(CancellationToken))
        {
            var path = string.Format("lists/{0}/detaijls.json", campaignId);
            var request = new HttpRequestMessageFactory()
                .Create(HttpMethod.Get, _serviceClientContext.Endpoint, path,
                    _serviceClientContext.ApiKey);

            var response = await _httpClient.SendAsync(request, token).ConfigureAwait(false);

            return await GetResponse<MailingList>(response);
        }

        #endregion

        public async Task<T> GetResponse<T>(HttpResponseMessage response)
        {
            var responseJson = await response.Content.ReadAsStringAsync();

            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    var result = JsonConvert.DeserializeObject<ApiResponse<T>>(responseJson);

                    if (result.Code == 0)
                    {
                        return result.Context;
                    }

                    throw new ApiClientException(result.Error, result.Code);
                case HttpStatusCode.BadRequest:
                    throw new ApiClientException("Bad Request", 400);
                case HttpStatusCode.NotFound:
                    throw new ApiClientException("Not Found", 404);
                default:
                    throw new ApiClientException("An error occurred", (int) response.StatusCode);
            }
        }
    }

    // TODO to seperate class
    public class BaseRequest<T>
    {
        public BaseRequest(T payload)
        {
            Payload = payload;
            QueryStringParams = new Dictionary<string, string>();
        }

        public Dictionary<string, string> QueryStringParams { get; private set; }
        public T Payload { get; private set; }
    }
}
