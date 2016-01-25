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
    public interface IMoosendApiClient
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
        Task<PagedCampaigns> GetAllCampaignsAsync(int page = 1, int pageSize = 10, string sortBy = "CreatedOn", string sortMethod = "ASC", CancellationToken token = default(CancellationToken));

        /// <summary> Returns basic information for the specified sender identified by its email address. </summary>
        /// <param name="email"> The email address of the senders to get information for. </param>
        /// <param name="token"> Cancellation Token. </param>
        Task<Sender> GetSenderAsync(string email, CancellationToken token = default(CancellationToken));

        /// <summary>
        ///     Gets a list of your active senders in your account. 
        ///     You may specify any email address of these senders when sending a campaign.
        /// </summary>
        /// <param name="token"> Cancellation Token. </param>
        Task<IList<Sender>> GetSendersAsync(CancellationToken token = default(CancellationToken));

        /// <summary>
        ///     Creates a new campaign in your account. This method does not send the campaign, but rather creates it as a draft, ready for sending or testing.
        ///     You can choose to send either a regural campaign or an AB split campaign.
        ///     Campaign content must be specified from a web location.
        /// </summary>
        /// <param name="campaignParams"> Draft's content properties. You must specify at least Name, Subject and SenderEmail. </param>
        /// <param name="token"> Cancellation Token. </param>
        Task<Guid> CreateCampaignAsync(CampaignParams campaignParams, CancellationToken token = default(CancellationToken));

        /// <summary>
        ///     Sends a test email of a draft campaign to a list of email addresses you specify for previewing.
        /// </summary>
        /// <param name="campaignId"> The ID of the draft campaign to be tested. </param>
        /// <param name="emails"> A list of email addresses to send the preview to. </param>
        /// <param name="token"> Cancellation Token. </param>
        /// <returns></returns>
        Task<bool> SendTestAsync(Guid campaignId, IList<string> emails, CancellationToken token = default(CancellationToken));

        /// <summary>
        ///     Sends an existing draft campaign to all recipients specified in its mailing list. 
        ///     The campaign is sent immediatelly.
        /// </summary>
        /// <param name="campaignId"> The ID of the campaign to be sent. </param>
        /// <param name="token"> Cancellation Token. </param>
        Task<bool> SendCampaignAsync(Guid campaignId, CancellationToken token = default(CancellationToken));

        /// <summary> Deletes a campaign from your account, draft or even sent. </summary>
        /// <param name="campaignId"> The ID of the campaign to be deleted. </param>
        /// <param name="token"> Cancellation Token. </param>
        Task<bool> DeleteCampaignAsync(Guid campaignId, CancellationToken token = default(CancellationToken));

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
        Task<PagedAnalyticsResponse> GetCampaignStatisticsAsync(Guid campaignId, MailStatus type = MailStatus.Sent, int page = 1, int pageSize = 50, DateTime? from = null, DateTime? to = null, CancellationToken token = default(CancellationToken));

        /// <summary> Returns a list with your campaign links and how many clicks have been made by your recipients, either unique or total. /// </summary>
        /// <param name="campaignId"> The ID of the requested campaign </param>
        /// <param name="token"> Cancellation Token. </param>
        Task<PagedAnalyticsResponse> GetCampaignLinkActivityAsync(Guid campaignId, CancellationToken token = default(CancellationToken));

        /// <summary> Returns a detailed report of your campaign activity (opens, clicks, etc) by country. </summary>
        /// <param name="campaignId"> The ID of the requested campaign. </param>
        /// <param name="token"></param>
        Task<PagedAnalyticsResponse> GetCampaignActivityByLocationAsync(Guid campaignId, CancellationToken token = default(CancellationToken));

        /// <summary> Updates properties of an existing draft campaign in your account. Non-draft campaigns cannot be updated. </summary>
        /// <param name="campaignId"> The ID of the draft campaign to update. </param>
        /// <param name="campaignParams"> Updated parameters to update in campaign. </param>
        /// <param name="token"> Cancellation Token. </param>
        /// <returns></returns>
        Task<bool> UpdateCampaignAsync(Guid campaignId, CampaignParams campaignParams, CancellationToken token = default(CancellationToken));

        Task<TModel> SendAsync<TModel>(HttpMethod method, string path, object parameters = null, CancellationToken token = default(CancellationToken));
        Task<TModel> GetResponse<TModel>(HttpResponseMessage response);
    }
}