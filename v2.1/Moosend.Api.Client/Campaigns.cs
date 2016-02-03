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

        /// <summary> Assigns a scheduled date and time at which the campaign will be delivered. </summary>
        /// <param name="campaignId"> The ID of the campaign to be scheduled. </param>
        /// <param name="dateTime"> The date and time at which the campaign will be delivered. </param>
        /// <param name="timezone">
        /// /// <summary>
        /// The timezone the specified date and time refers to. By default the timezone in your settings panel will be used. If specified, one of the following values must be used:
        /// <ul>
        /// <li>Dateline Standard Time</li>
        /// <li>Samoa Standard Time</li>
        /// <li>Hawaiian Standard Time</li>
        /// <li>Alaskan Standard Time</li>
        /// <li>Pacific Standard Time</li>
        /// <li>Pacific Standard Time (Mexico)</li>
        /// <li>US Mountain Standard Time</li>
        /// <li>Mountain Standard Time (Mexico)</li>
        /// <li>Mountain Standard Time</li>
        /// <li>Central Standard Time</li>
        /// <li>Central Standard Time (Mexico)</li>
        /// <li>Canada Central Standard Time</li>
        /// <li>SA Pacific Standard Time</li>
        /// <li>US Eastern Standard Time</li>
        /// <li>Eastern Standard Time</li>
        /// <li>Venezuela Standard Time</li>
        /// <li>Atlantic Standard Time</li>
        /// <li>SA Western Standard Time</li>
        /// <li>Central Brazilian Standard Time</li>
        /// <li>Pacific SA Standard Time</li>
        /// <li>Newfoundland Standard Time</li>
        /// <li>E. South America Standard Time</li>
        /// <li>Argentina Standard Time</li>
        /// <li>SA Eastern Standard Time</li>
        /// <li>Greenland Standard Time</li>
        /// <li>Montevideo Standard Time</li>
        /// <li>Mid-Atlantic Standard Time</li>
        /// <li>Azores Standard Time</li>
        /// <li>Cape Verde Standard Time</li>
        /// <li>Greenwich Standard Time</li>
        /// <li>GMT Standard Time</li>
        /// <li>Morocco Standard Time</li>
        /// <li>W. Central Africa Standard Time</li>
        /// <li>Central European Standard Time</li>
        /// <li>Romance Standard Time</li>
        /// <li>W. Europe Standard Time</li>
        /// <li>Namibia Standard Time</li>
        /// <li>E. Europe Standard Time</li>
        /// <li>Israel Standard Time</li>
        /// <li>FLE Standard Time</li>
        /// <li>South Africa Standard Time</li>
        /// <li>Egypt Standard Time</li>
        /// <li>Middle East Standard Time</li>
        /// <li>GTB Standard Time</li>
        /// <li>Jordan Standard Time</li>
        /// <li>Iran Standard Time</li>
        /// <li>Georgian Standard Time</li>
        /// <li>E. Africa Standard Time</li>
        /// <li>Russian Standard Time</li>
        /// <li>Arab Standard Time</li>
        /// <li>Arabic Standard Time</li>
        /// <li>Caucasus Standard Time</li>
        /// <li>Mauritius Standard Time</li>
        /// <li>Azerbaijan Standard Time</li>
        /// <li>Arabian Standard Time</li>
        /// <li>Afghanistan Standard Time</li>
        /// <li>West Asia Standard Time</li>
        /// <li>Pakistan Standard Time</li>
        /// <li>Ekaterinburg Standard Time</li>
        /// <li>Sri Lanka Standard Time</li>
        /// <li>India Standard Time</li>
        /// <li>Nepal Standard Time</li>
        /// <li>N. Central Asia Standard Time</li>
        /// <li>Central Asia Standard Time</li>
        /// <li>Myanmar Standard Time</li>
        /// <li>North Asia Standard Time</li>
        /// <li>SE Asia Standard Time</li>
        /// <li>Taipei Standard Time</li>
        /// <li>W. Australia Standard Time</li>
        /// <li>Singapore Standard Time</li>
        /// <li>North Asia East Standard Time</li>
        /// <li>China Standard Time</li>
        /// <li>Yakutsk Standard Time</li>
        /// <li>Korea Standard Time</li>
        /// <li>Tokyo Standard Time</li>
        /// <li>AUS Central Standard Time</li>
        /// <li>Cen. Australia Standard Time</li>
        /// <li>AUS Eastern Standard Time</li>
        /// <li>West Pacific Standard Time</li>
        /// <li>Tasmania Standard Time</li>
        /// <li>Vladivostok Standard Time</li>
        /// <li>Central Pacific Standard Time</li>
        /// <li>New Zealand Standard Time</li>
        /// <li>Tonga Standard Time</li>
        /// </ul>
        /// </summary>
        /// </param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<bool> ScheduleCampaignAsync(Guid campaignId, DateTime dateTime, string timezone, CancellationToken token = default(CancellationToken))
        {
            var parameters = new
            {
                DateTime = dateTime.ToString("dd/MM/yyy HH:mm:ss"),
                Timezone = timezone
            };

            return await SendAsync<bool>(HttpMethod.Post, string.Format("/campaigns/{0}/schedule", campaignId), parameters, token).ConfigureAwait(false);
        }

        /// <summary>
        ///     Removes a previously defined scheduled date and time from a campaign, so that it will be delivered immediately if already queued or when sent.
        /// </summary>
        /// <param name="campaignId"> The ID of the campaign to be unscheduled. </param>
        /// <param name="token"> Cancellation Token. </param>
        /// <returns> A boolena value indicating success. </returns>
        public async Task<bool> UnscheduleCampaignAsync(Guid campaignId, CancellationToken token = default(CancellationToken))
        {
            return await SendAsync<bool>(HttpMethod.Post, string.Format("/campaigns/{0}/unschedule", campaignId), null, token).ConfigureAwait(false);
        }

        /// <summary>
        ///     Provides a basic summary of the results for a sent AB test campaign, separately for each version (A and B), such as the number of recipients, opens, clicks, bounces, unsubscribes, forwards etc to date.
        /// </summary>
        /// <param name="campaignId"> The ID of the requested AB test campaign. </param>
        /// <param name="token"> Cancellation Token. </param>
        public async Task<AbTestCampaignSummaryResult> GetAbTestCampaignSummary(Guid campaignId, CancellationToken token = default(CancellationToken))
        {
            return await SendAsync<AbTestCampaignSummaryResult>(HttpMethod.Get, string.Format("/campaigns/{0}/view_ab_summary", campaignId), null, token).ConfigureAwait(false);
        }
    }
}