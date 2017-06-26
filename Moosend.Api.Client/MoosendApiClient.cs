using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Moosend.Api.Common;
using Moosend.Api.Common.Responses;
using Newtonsoft.Json;

namespace Moosend.Api.Client
{
    public partial class MoosendApiClient : IMoosendApiClient
    {
        private readonly HttpClient _httpClient;
        private readonly Uri _endpoint;
        private readonly Guid _apiKey;

        public MoosendApiClient(Guid apiKey, ServiceClientContext context = null)
        {
            if (apiKey == Guid.Empty) throw new ArgumentNullException("apiKey");

            if (context == null) context = new ServiceClientContext();

            _endpoint = context.Endpoint;
            _apiKey = apiKey;

            _httpClient = context.Handler == null
                ? new HttpClient(new HttpClientHandler())
                : new HttpClient(context.Handler);

            _httpClient.Timeout = context.Timeout;
            _httpClient.BaseAddress = context.Endpoint;
            _httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
            _httpClient.DefaultRequestHeaders.Add("User-Agent", string.Format("moosend-api-client-{0}-{1}", Environment.Version, Environment.OSVersion));
        }

        private async Task<TModel> SendAsync<TModel>(HttpMethod method, string path, object parameters = null, CancellationToken token = default(CancellationToken))
        {
            var request = new HttpRequestMessage();

            var relativeUrl = string.Format("{0}.json?apikey={1}", path, _apiKey);

            if (parameters != null && method == HttpMethod.Get)
            {
                relativeUrl += "&" + parameters.ToQueryString();
            }
            else if (parameters != null && method == HttpMethod.Post)
            {
                var json = JsonConvert.SerializeObject(parameters);

                if (!string.IsNullOrWhiteSpace(json))
                {
                    request.Content = new StringContent(json, Encoding.UTF8, "application/json");
                }
            }

            var uri = new Uri(_endpoint + relativeUrl);

            request.RequestUri = uri;
            request.Method = method;

            var response = await _httpClient.SendAsync(request, token).ConfigureAwait(false);

            return await GetResponse<TModel>(response).ConfigureAwait(false);
        }

        internal async Task<TModel> GetResponse<TModel>(HttpResponseMessage response)
        {
            if (response.Content == null) throw new ApiClientException("Response content was empty.");

            var responseJson = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:

                    // deserialize as a generic api result and check if result is an error
                    var result = JsonConvert.DeserializeObject<ApiResponse<object>>(responseJson);

                    if (result.Code == 0)
                    {
                        if (typeof(TModel) == typeof(bool))
                        {
                            return (TModel)(object)true;
                        }

                        // deserialize again to get the expected object
                        return JsonConvert.DeserializeObject<ApiResponse<TModel>>(responseJson).Context;
                    }

                    if (result.Code == -2)
                    {
                        var responseWithWarnings = JsonConvert.DeserializeObject<ApiResponse<ContextWithWarnings>>(responseJson);

                        // create exception message from messages
                        string errorMessage = null;
                        foreach (var message in responseWithWarnings.Context.Messages)
                        {
                            errorMessage += string.Format(" {0},", message.Message);
                        }

                        if (errorMessage != null)
                        {
                            errorMessage = errorMessage.Remove(errorMessage.Length - 1).Trim();
                        }

                        throw new ApiClientException(errorMessage, responseWithWarnings.Code);
                    }

                    throw new ApiClientException(result.Error, result.Code);
                case HttpStatusCode.BadRequest:
                    throw new ApiClientException("Bad Request", 400);
                case HttpStatusCode.NotFound:
                    throw new ApiClientException("Not Found", 404);
                default:
                    throw new ApiClientException("An error occurred.", (int)response.StatusCode);
            }
        }
    }
}
