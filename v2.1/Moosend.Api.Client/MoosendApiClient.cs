using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Moosend.Api.Common;
using Moosend.Api.Common.Models;
using Moosend.Api.Common.Responses;
using Newtonsoft.Json;

namespace Moosend.Api.Client
{
    public class MoosendApiClient
    {
        private readonly HttpClient _httpClient;
        private readonly Uri _endpoint;
        private readonly string _apiKey;

        public MoosendApiClient(ServiceClientContext context)
        {
            _endpoint = context.Endpoint;
            _apiKey = context.ApiKey;

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

        /// <summary>
        ///     Returns a list of all campaigns in your account with detailed infomation.
        ///     Because the results from this call could be quite big, paging information is required as input.
        /// </summary>
        /// <param name="page">
        ///     The page number to display results for. If not specified, the first page will be returned.
        /// </param>
        /// <param name="pageSize">
        ///     The maximum number of results per page. This must be a positive integer up to 100. If not specified, 10 results per
        ///     page will be returned.
        ///     If a value greater than 100 is specified, it will be treated as 100.
        /// </param>
        /// <param name="sortBy">
        ///     The name of the campaign property to sort results by. If not specified, results will be sorted by the CreatedOn
        ///     property.
        /// </param>
        /// <param name="sortMethod">
        ///     The method to sort results: ASC for ascending, DESC for descending. If not specified, ASC will be assumed.
        /// </param>
        /// <param name="token">
        ///     Canellation Token.
        /// </param>
        /// <exception cref="ApiClientException">
        ///     Thrown when a non-
        ///     numeric value is assigned.
        /// </exception>
        public async Task<PagedCampaigns> FindAllCampaignsAsync(int page = 1, int pageSize = 10,
            string sortBy = "CreatedOn", string sortMethod = "ASC", CancellationToken token = default(CancellationToken))
        {
            var path = string.Format("/campaigns/{0}/{1}", page, pageSize);

            var queryParams = new
            {
                SortBy = sortBy,
                SortMethod = sortMethod
            };

            return await SendAsync<PagedCampaigns>(HttpMethod.Get, path, queryParams, token).ConfigureAwait(false);
        }

        /// <summary>
        ///     Returns basic information for the specified sender identified by its email address.
        /// </summary>
        /// <param name="email">
        ///     The email address of the senders to get information for.
        /// </param>
        /// <param name="token">
        ///     Cancellation Token.
        /// </param>
        /// <returns></returns>
        public async Task<Sender> FindSender(string email, CancellationToken token = default(CancellationToken))
        {
            return await SendAsync<Sender>(HttpMethod.Get, "/senders/find_one", new {Email = email}, token).ConfigureAwait(false);
        }

        #endregion

        #region Generic API calling method and helpers

        public async Task<TModel> SendAsync<TModel>(HttpMethod method, string path, object parameters = null, CancellationToken token = default(CancellationToken))
        {
            var request = new HttpRequestMessage();

            var sb = new StringBuilder(string.Format("{0}{1}.json?apiKey={2}",
                _endpoint,
                path,
                _apiKey));

            if (parameters != null && method == HttpMethod.Get)
            {
                sb.Append("&" + parameters.ToQueryString());
            }
            else if (parameters != null && method == HttpMethod.Post)
            {
                var json = JsonConvert.SerializeObject(parameters);

                if (!string.IsNullOrWhiteSpace(json))
                {
                    request.Content = new StringContent(json, Encoding.UTF8, "application/json");
                }
            }

            var uri = new Uri(_endpoint, sb.ToString());

            request.RequestUri = uri;
            request.Method = method;

            var response = await _httpClient.SendAsync(request).ConfigureAwait(false);

            return await GetResponse<TModel>(response);
        }

        public async Task<TModel> GetResponse<TModel>(HttpResponseMessage response)
        {
            var responseJson = await response.Content.ReadAsStringAsync();

            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    var apiResponse = JsonConvert.DeserializeObject<ApiResponse<TModel>>(responseJson);

                    if (apiResponse.Code == ApiCodes.SUCCESS)
                    {
                        return apiResponse.Context;
                    }

                    throw new ApiClientException(apiResponse.Error, apiResponse.Code);
                case HttpStatusCode.BadRequest:
                    throw new ApiClientException("Bad Request", 400);
                case HttpStatusCode.NotFound:
                    throw new ApiClientException("Not Found", 404);
                default:
                    throw new ApiClientException("An error occurred", (int) response.StatusCode);
            }
        }

        #endregion
    }
}
