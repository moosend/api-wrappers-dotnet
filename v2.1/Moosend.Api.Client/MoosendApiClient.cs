using System;
using System.Collections.Generic;
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
                ? new HttpClient(new HttpClientHandler { AutomaticDecompression = DecompressionMethods.GZip })
                : new HttpClient(context.Handler);

            _httpClient.Timeout = context.Timeout;
            _httpClient.BaseAddress = context.Endpoint;
            _httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
            _httpClient.DefaultRequestHeaders.Add("User-Agent", string.Format("moosend-api-client-{0}-{1}", Environment.Version, Environment.OSVersion));
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
        /// <param name="token"> Cancellation Token </param>
        public async Task<PagedCampaigns> GetAllCampaignsAsync(int page = 1, int pageSize = 10, string sortBy = "CreatedOn", string sortMethod = "ASC", CancellationToken token = default(CancellationToken))
        {
            var path = string.Format("/campaigns/{0}/{1}", page, pageSize);

            var queryParams = new
            {
                SortBy = sortBy,
                SortMethod = sortMethod
            };

            return await SendAsync<PagedCampaigns>(HttpMethod.Get, path, queryParams, token).ConfigureAwait(false);
        }

        /// <summary> Returns basic information for the specified sender identified by its email address. </summary>
        /// <param name="email"> The email address of the senders to get information for. </param>
        /// <param name="token"> Cancellation Token </param>
        /// <returns></returns>
        public async Task<Sender> GetSenderAsync(string email, CancellationToken token = default(CancellationToken))
        {
            return await SendAsync<Sender>(HttpMethod.Get, "/senders/find_one", new { Email = email }, token).ConfigureAwait(false);
        }

        /// <summary>
        ///     Gets a list of your active senders in your account. 
        ///     You may specify any email address of these senders when sending a campaign.
        /// </summary>
        /// <param name="token"> Cancellation Token </param>
        /// <returns></returns>
        public async Task<IList<Sender>> GetSendersAsync(CancellationToken token = default(CancellationToken))
        {
            return await SendAsync<IList<Sender>>(HttpMethod.Get, "/senders/find_all", null, token).ConfigureAwait(false);
        }

        /// <summary>
        ///     Creates a new campaign in your account. This method does not send the campaign, but rather creates it as a draft, ready for sending or testing.
        ///     You can choose to send either a regural campaign or an AB split campaign.
        ///     Campaign content must be specified from a web location.
        /// </summary>
        /// <param name="campaignParams"> Draft's content properties. You must specify at least Name, Subject and SenderEmail. </param>
        /// <param name="token"> Cancellation Token </param>
        /// <returns></returns>
        public async Task<Guid> CreateDraftAsync(CampaignParams campaignParams, CancellationToken token = default(CancellationToken))
        {
            return await SendAsync<Guid>(HttpMethod.Post, "/campaigns/create", campaignParams, token).ConfigureAwait(false);
        }

        /// <summary>
        ///     Sends a test email of a draft campaign to a list of email addresses you specify for previewing.
        /// </summary>
        /// <param name="campaignId"> The ID of the draft campaign to be tested. </param>
        /// <param name="emails"> A list of email addresses to send the preview to. </param>
        /// <param name="token"> Cancellation Token </param>
        /// <returns></returns>
        public async Task<bool> SendTestAsync(Guid campaignId, IList<string> emails, CancellationToken token = default(CancellationToken))
        {
            return await SendAsync<bool>(HttpMethod.Post, string.Format("/campaigns/{0}/send_test", campaignId), new { TestEmails = emails }, token).ConfigureAwait(false);
        }

        /// <summary>
        ///     Sends an existing draft campaign to all recipients specified in its mailing list. 
        ///     The campaign is sent immediatelly.
        /// </summary>
        /// <param name="campaignId"> The ID of the campaign to be sent. </param>
        /// <param name="token"> Cancellation Token </param>
        /// <returns></returns>
        public async Task<bool> SendCampaignAsync(Guid campaignId, CancellationToken token = default(CancellationToken))
        {
            return await SendAsync<bool>(HttpMethod.Post, string.Format("/campaigns/{0}/send", campaignId), null, token).ConfigureAwait(false);
        }

        /// <summary> Deletes a campaign from your account, draft or even sent. </summary>
        /// <param name="campaignId"> The ID of the campaign to be deleted. </param>
        /// <param name="token"> CAncellation Token </param>
        /// <returns></returns>
        public async Task<bool> DeleteCampaignAsync(Guid campaignId, CancellationToken token = default(CancellationToken))
        {
            return await SendAsync<bool>(HttpMethod.Delete, string.Format("/campaigns/{0}/delete", campaignId), null, token).ConfigureAwait(false);
        }

        /// <summary>
        ///     Returns a detailed list of statistics for a given campaign based on activity such as emails sent, opened, bounced, link clicked, etc. 
        ///     Because the results from this call could be quite big, paging information is required as input.
        /// </summary>
        /// <param name="campaignId"> The ID of the campaign to display statistics for. </param>
        /// <param name="type"></param>
        /// <param name="page"> The page number to display results for. If not specified, the first page will be returned. </param>
        /// <param name="pageSize">
        ///     The maximum number of results per page. This must be a positive integer up to 100. If not specified, 50 results per page will be returned. 
        ///     If a value greater than 100 is specified, it will be treated as 100. </param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<PagedCampaignStatisticsResponse> GetCampaignStatisticsAsync(Guid campaignId, MailStatus type = MailStatus.Sent, int page = 1, int pageSize = 50, DateTime? from = null, DateTime? to = null, CancellationToken token = default(CancellationToken))
        {
            return await SendAsync<PagedCampaignStatisticsResponse>(HttpMethod.Get, string.Format("/campaigns/{0}/stats/{1}", campaignId, type), null, token).ConfigureAwait(false);
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

            var response = await _httpClient.SendAsync(request, token).ConfigureAwait(false);

            return await GetResponse<TModel>(response).ConfigureAwait(false);
        }

        public async Task<TModel> GetResponse<TModel>(HttpResponseMessage response)
        {
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
                    throw new ApiClientException("An error occurred", (int)response.StatusCode);
            }
        }

        #endregion
    }
}
