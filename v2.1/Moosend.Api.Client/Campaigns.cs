using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Moosend.Api.Common;
using Moosend.Api.Common.Models;
using Moosend.Api.Common.Responses;

namespace Moosend.Api.Client
{
    public partial class MoosendApiClient
    {
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
        /// <param name="token"> Cancellation Token. </param>
        public async Task<CampaignsResult> GetCampaignsAsync(int page = 1, int pageSize = 10, string sortBy = "CreatedOn", string sortMethod = "ASC", CancellationToken token = default(CancellationToken))
        {
            var path = string.Format("/campaigns/{0}/{1}", page, pageSize);

            var queryParams = new
            {
                SortBy = sortBy,
                SortMethod = sortMethod
            };

            return await SendAsync<CampaignsResult>(HttpMethod.Get, path, queryParams, token).ConfigureAwait(false);
        }

        /// <summary> Returns basic information for the specified sender identified by its email address. </summary>
        /// <param name="email"> The email address of the senders to get information for. </param>
        /// <param name="token"> Cancellation Token. </param>
        public async Task<Sender> GetSenderByEmailAsync(string email, CancellationToken token = default(CancellationToken))
        {
            return await SendAsync<Sender>(HttpMethod.Get, "/senders/find_one", new { Email = email }, token).ConfigureAwait(false);
        }

        /// <summary>
        ///     Gets a list of your active senders in your account. 
        ///     You may specify any email address of these senders when sending a campaign.
        /// </summary>
        /// <param name="token"> Cancellation Token. </param>
        public async Task<IEnumerable<Sender>> GetSendersAsync(CancellationToken token = default(CancellationToken))
        {
            return await SendAsync<IEnumerable<Sender>>(HttpMethod.Get, "/senders/find_all", null, token).ConfigureAwait(false);
        }

        /// <summary>
        ///     Creates a new campaign in your account. This method does not send the campaign, but rather creates it as a draft, ready for sending or testing.
        ///     You can choose to send either a regural campaign or an AB split campaign.
        ///     Campaign content must be specified from a web location.
        /// </summary>
        /// <param name="campaignParams"> Draft's content properties. You must specify at least Name, Subject and SenderEmail. </param>
        /// <param name="token"> Cancellation Token. </param>
        public async Task<Guid> CreateCampaignAsync(CampaignParams campaignParams, CancellationToken token = default(CancellationToken))
        {
            return await SendAsync<Guid>(HttpMethod.Post, "/campaigns/create", campaignParams, token).ConfigureAwait(false);
        }

        /// <summary>
        ///     Sends a test email of a draft campaign to a list of email addresses you specify for previewing.
        /// </summary>
        /// <param name="campaignId"> The ID of the draft campaign to be tested. </param>
        /// <param name="emails"> A list of email addresses to send the preview to. </param>
        /// <param name="token"> Cancellation Token. </param>
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
        /// <param name="token"> Cancellation Token. </param>
        public async Task<bool> SendCampaignAsync(Guid campaignId, CancellationToken token = default(CancellationToken))
        {
            return await SendAsync<bool>(HttpMethod.Post, string.Format("/campaigns/{0}/send", campaignId), null, token).ConfigureAwait(false);
        }

        /// <summary> Deletes a campaign from your account, draft or even sent. </summary>
        /// <param name="campaignId"> The ID of the campaign to be deleted. </param>
        /// <param name="token"> Cancellation Token. </param>
        public async Task<bool> DeleteCampaignAsync(Guid campaignId, CancellationToken token = default(CancellationToken))
        {
            return await SendAsync<bool>(HttpMethod.Delete, string.Format("/campaigns/{0}/delete", campaignId), null, token).ConfigureAwait(false);
        }

        /// <summary>
        ///     Returns a detailed list of statistics for a given campaign based on activity such as emails sent, opened, bounced, link clicked, etc. 
        ///     Because the results from this call could be quite big, paging information is required as input.
        /// </summary>
        /// <param name="campaignId"> The ID of the campaign to display statistics for. </param>
        /// <param name="type">
        ///     The type of the activity to display results for. This must be one of the following values:
        ///         Sent : to get information about when and to which recipients the campaign was sent.
        ///         Opened : to get information about who opened the campaign and when.
        ///         LinkClicked : to get information about who clicked on which link and when.
        ///         Forwarded : to get information about who forwarded the campaign using the relevant link on the email body and when.
        ///         Unsubscribed : to get information about who unsubscribed from the campaign by clicking on the unsubscribe link and when.
        ///         Bounced : to get information about which email recipients failed to receive the campaign.
        ///     If not specified, the value Sent will be used by default.
        /// </param>
        /// <param name="page"> The page number to display results for. If not specified, the first page will be returned. </param>
        /// <param name="pageSize">
        ///     The maximum number of results per page. This must be a positive integer up to 100. If not specified, 50 results per page will be returned. 
        ///     If a value greater than 100 is specified, it will be treated as 100. </param>
        /// <param name="from"> A date value that specifies since when to start returning results. If ommitted, results will be returned from the moment the campaign was sent. </param>
        /// <param name="to"> A date value that specifies up to when to return results. If ommitted, results will be returned up to date. </param>
        /// <param name="token"> Cancellation Token. </param>
        public async Task<CampaignsStatisticsResult> GetCampaignStatisticsAsync(Guid campaignId, MailStatus status = MailStatus.Sent, int page = 1, int pageSize = 50, DateTime? from = null, DateTime? to = null, CancellationToken token = default(CancellationToken))
        {
            var parameters = new
            {
                Page = page,
                PageSize = pageSize,
                From = from,
                To = to
            };

            return await SendAsync<CampaignsStatisticsResult>(HttpMethod.Get, string.Format("/campaigns/{0}/stats/{1}", campaignId, status), parameters, token).ConfigureAwait(false);
        }

        /// <summary> Returns a list with your campaign links and how many clicks have been made by your recipients, either unique or total. /// </summary>
        /// <param name="campaignId"> The ID of the requested campaign </param>
        /// <param name="token"> Cancellation Token. </param>
        public async Task<CampaignsStatisticsResult> GetCampaignLinkActivityAsync(Guid campaignId, CancellationToken token = default(CancellationToken))
        {
            return await SendAsync<CampaignsStatisticsResult>(HttpMethod.Get, string.Format("/campaigns/{0}/stats/links", campaignId), null, token).ConfigureAwait(false);
        }

        /// <summary> Returns a detailed report of your campaign activity (opens, clicks, etc) by country. </summary>
        /// <param name="campaignId"> The ID of the requested campaign. </param>
        /// <param name="token"></param>
        public async Task<CampaignsStatisticsResult> GetCampaignActivityByLocationAsync(Guid campaignId, CancellationToken token = default(CancellationToken))
        {
            return await SendAsync<CampaignsStatisticsResult>(HttpMethod.Get, string.Format("/campaigns/{0}/stats/countries", campaignId), null, token).ConfigureAwait(false);
        }

        /// <summary> Updates properties of an existing draft campaign in your account. Non-draft campaigns cannot be updated. </summary>
        /// <param name="campaignId"> The ID of the draft campaign to update. </param>
        /// <param name="campaignParams"> Updated parameters to update in campaign. </param>
        /// <param name="token"> Cancellation Token. </param>
        /// <returns></returns>
        public async Task<bool> UpdateCampaignAsync(Guid campaignId, CampaignParams campaignParams, CancellationToken token = default(CancellationToken))
        {
            return await SendAsync<bool>(HttpMethod.Post, string.Format("/campaigns/{0}/update", campaignId), campaignParams, token).ConfigureAwait(false);
        }

        /// <summary>
        ///     Removes a previously defined scheduled date and time from a campaign, so that it will be delivered immediately if already queued or when sent.
        /// </summary>
        /// <param name="campaignId"> The ID of the campaign to be unscheduled. </param>
        /// <param name="token"> Cancellation Token. </param>
        /// <returns></returns>
        public async Task<bool> UnscheduleCampaignAsync(Guid campaignId, CancellationToken token = default(CancellationToken))
        {
            return await SendAsync<bool>(HttpMethod.Post, string.Format("/campaigns/{0}/unschedule", campaignId), null, token).ConfigureAwait(false);
        }
    }
}