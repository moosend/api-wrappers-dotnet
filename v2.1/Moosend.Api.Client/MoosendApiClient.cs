using System;
using System.Collections.Generic;
using System.Linq;
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
    public class MoosendApiClient : IMoosendApiClient
    {
        private readonly HttpClient _httpClient;
        private readonly Uri _endpoint;
        private readonly string _apiKey;

        public MoosendApiClient(string apiKey, ServiceClientContext context = null)
        {
            if (apiKey == null) throw new ArgumentNullException("");

            if (context == null)
            {
                context = new ServiceClientContext(new Uri("https://api.moosend.com/v3"));
            }

            _endpoint = context.Endpoint;
            _apiKey = apiKey;

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

        #endregion

        #region Mailing Lists

        /// <summary> Gets a list of your active mailing lists in your account. </summary>
        /// <param name="page"> The page number to display results for. If not specified, the first page will be returned. </param>
        /// <param name="pageSize"> The maximum number of results per page. If ommitted, 10 mailing lists will be returned per page. </param>
        /// <param name="token"> Cancellation Token. </param>
        public async Task<MailingListsResult> GetMailingListsAsync(int page = 1, int pageSize = 10, CancellationToken token = default(CancellationToken))
        {
            return await SendAsync<MailingListsResult>(HttpMethod.Get, string.Format("/lists/{0}/{1}", page, pageSize), null, token).ConfigureAwait(false);
        }

        /// <summary> Creates a new empty mailing list in your account. </summary>
        /// <param name="name"> The name of the new mailing list. </param>
        /// <param name="confirmationPage"> The URL of the page that will be displayed at the end of the subscription process. </param>
        /// <param name="redirectAfterUnsubscribePage"> The URL of the page that users will be redirected after unsubscribing from your mailing list. </param>
        /// <param name="token"> Cancellation Token. </param>
        /// <returns> The Guid of the mailing list. </returns>
        public async Task<Guid> CreateMailingListAsync(string name, string confirmationPage = null, string redirectAfterUnsubscribePage = null, CancellationToken token = default(CancellationToken))
        {
            var parameters = new
            {
                Name = name,
                ConfirmationPage = confirmationPage,
                RedirectAfterUnsubscribePage = redirectAfterUnsubscribePage
            };

            return await SendAsync<Guid>(HttpMethod.Post, "/lists/create", parameters, token).ConfigureAwait(false);
        }

        /// <summary> Updates the properties of an existing mailing list. </summary>
        /// <param name="listId"> The ID of the mailing list to update. </param>
        /// <param name="name"> The name of the new mailing list. </param>
        /// <param name="confirmationPage"> The URL of the page that will be displayed at the end of the subscription process. </param>
        /// <param name="redirectAfterUnsubscribePage"> The URL of the page that users will be redirected after unsubscribing from your mailing list. </param>
        /// <param name="token"> Cancellation Token. </param>
        /// <returns> The Guid of the mailing list. </returns>
        public async Task<Guid> UpdateMailingListAsync(Guid listId, string name, string confirmationPage = null, string redirectAfterUnsubscribePage = null, CancellationToken token = default(CancellationToken))
        {
            var parameters = new
            {
                Name = name,
                ConfirmationPage = confirmationPage,
                RedirectAfterUnsubscribePage = redirectAfterUnsubscribePage
            };

            return await SendAsync<Guid>(HttpMethod.Post, string.Format("/lists/{0}/update", listId), parameters, token).ConfigureAwait(false);
        }

        /// <summary>
        ///     Gets a list of all subscribers in a given mailing list.
        ///     You may filter the list by setting a date to fetch those subscribed since then and/or by their status.
        ///     Because the results from this call could be quite big, paging information is required as input.
        /// </summary>
        /// <param name="mailingListId"> The ID of the mailing list to fetch subscribers for. </param>
        /// <param name="status">
        ///     The status to filter the subscribers in the given mailing list. This must be one of the following values:
        ///         Subscribed : to fetch active subscribers only
        ///         Unsubscribed : to fetch unsubscribed subscribers only
        ///         Bounced : to fetch subscribers that have bounced on a previously sent campaign and are suspicious for not being valid
        ///         Removed : to fetch removed subscribers pending deletion from our database
        ///     If ommitted, all subscribers will be returned, no matter what their status is.
        /// </param>
        /// <param name="since">
        ///     A date to specify since when to fetch results, according to the date each subscriber was added to the mailing list.
        ///     The date must be formatted as YYYY-MM-DD (eg. 2012-12-31). 
        ///     If omitted, all subscribers will be returned, no matter what date they were added in the list.
        /// </param>
        /// <param name="page"> The page number to display results for. If not specified, the first page will be returned. </param>
        /// <param name="pageSize"> 
        ///     The maximum number of results per page. This must be a positive integer up to 1000. If not specified, 500 results per page will be returned. 
        ///     If a value greater than 1000 is specified, it will be treated as 1000. </param>
        /// <param name="token"> Cancellation Token. </param>
        public async Task<SubscribersResult> GetSubscribersAsync(Guid mailingListId, SubscribeType? status, DateTime? since = null, int page = 1, int pageSize = 500, CancellationToken token = default(CancellationToken))
        {
            var parameters = new
            {
                Since = since,
                Page = page,
                PageSize = pageSize
            };

            return await SendAsync<SubscribersResult>(HttpMethod.Get, string.Format("/lists/{0}/subscribers/{1}", mailingListId, status), parameters, token).ConfigureAwait(false);
        }

        /// <summary>
        ///     Gets details for a given mailing list. You may include subscriber statistics in your results or not. 
        ///     Any segments existing for the requested mailing list will not be included in the results.
        /// </summary>
        /// <param name="mailingListId"> The ID of the mailing list to be returned. </param>
        /// <param name="withStatistics"> 
        ///     Specifies whether to fetch statistics for the subscribers or not. If ommitted, results will be returned with statistics by default.
        ///     Specified value should be either 'true' of 'false' (without quotes). </param>
        /// <param name="token"> Cancellation Token. </param>
        public async Task<MailingList> GetMailingListByIdAsync(Guid mailingListId, bool withStatistics = true, CancellationToken token = default(CancellationToken))
        {
            var parameters = new
            {
                WithStatistics = withStatistics
            };
            return await SendAsync<MailingList>(HttpMethod.Get, string.Format("/lists/{0}/details", mailingListId), parameters, token).ConfigureAwait(false);
        }

        /// <summary> Deletes a mailing list from your account. </summary>
        /// <param name="mailingListId"> The ID of the mailing list to be deleted. </param>
        /// <param name="token"> Ceancellation Token. </param>
        /// <returns> A boolean value indicating success. </returns>
        public async Task<bool> DeleteMailingListAsync(Guid mailingListId, CancellationToken token = default(CancellationToken))
        {
            return await SendAsync<bool>(HttpMethod.Delete, string.Format("/lists/{0}/delete", mailingListId), null, token).ConfigureAwait(false);
        }

        /// <summary> Creates a new custom field in the specified mailing list. </summary>
        /// <param name="mailingListId"> The ID of the mailing list where the custom field will belong to. </param>
        /// <param name="name"> The name of the custom field </param>
        /// <param name="customFieldType"> Specifies the data type of the custom field. If ommitted, Text will be assumed. </param>
        /// <param name="isRequired">
        ///     Specify whether this is field will be mandatory on not, when being used in a subscription form. 
        ///     You should specify a value of either truetrue or false. 
        ///     If ommitted, false will be assumed. </param>
        /// <param name="options"> 
        ///     If you want to create a custom field of type SingleSelectDropdown, you must set this parameter to specify the available options for the user to choose from.
        ///     Use a comma (,) to seperate different options. </param>
        /// <param name="token"> Cancellation Token. </param>
        /// <returns> The Guid of new custom field. </returns>
        public async Task<Guid> CreateCustomFieldAsync(Guid mailingListId, string name, CustomFieldType customFieldType = CustomFieldType.Text, bool isRequired = false, string options = null, CancellationToken token = default(CancellationToken))
        {
            var parameters = new
            {
                Name = name,
                CustomFieldType = customFieldType,
                IsRequired = isRequired,
                Options = options
            };

            return await SendAsync<Guid>(HttpMethod.Post, string.Format("/lists/{0}/customfields/create", mailingListId), parameters, token).ConfigureAwait(false);
        }

        /// <summary> Updates the properties of an existing custom field in the specified mailing list. </summary>
        /// <param name="mailingListId"> The ID of the mailing list where the custom field belongs to. </param>
        /// <param name="customFieldId"> The ID of the custom field to be updated. </param>
        /// <param name="name"> The name of the custom field </param>
        /// <param name="customFieldType"> Specifies the data type of the custom field. If ommitted, Text will be assumed. </param>
        /// <param name="isRequired">
        ///     Specify whether this is field will be mandatory on not, when being used in a subscription form. 
        ///     You should specify a value of either truetrue or false. 
        ///     If ommitted, false will be assumed. </param>
        /// <param name="options"> 
        ///     If you want to create a custom field of type SingleSelectDropdown, you must set this parameter to specify the available options for the user to choose from.
        ///     Use a comma (,) to seperate different options. </param>
        /// <param name="token"> Cancellation Token. </param>
        /// <returns> A boolean indicating success. </returns>
        public async Task<Guid> UpdateCustomFieldAsync(Guid mailingListId, Guid customFieldId, string name, CustomFieldType customFieldType = CustomFieldType.Text, bool isRequired = false, string options = null, CancellationToken token = default(CancellationToken))
        {
            var parameters = new
            {
                Name = name,
                CustomFieldType = customFieldType,
                IsRequired = isRequired,
                Options = options
            };

            return await SendAsync<Guid>(HttpMethod.Post, string.Format("/lists/{0}/customfields/{1}/update", mailingListId, customFieldId), parameters, token).ConfigureAwait(false);
        }

        /// <summary> Removes a custom field definition from the specified mailing list. </summary>
        /// <param name="mailingListId"> The ID of the mailing list where the custom field belongs. </param>
        /// <param name="customFieldId"> The ID of the custom field to be removed. </param>
        /// <param name="token"> Cancellation Token. </param>
        /// <returns> A boolean indicating success. </returns>
        public async Task<bool> DeleteCustomFieldAsync(Guid mailingListId, Guid customFieldId, CancellationToken token = default(CancellationToken))
        {
            return await SendAsync<bool>(HttpMethod.Delete, string.Format("/lists/{0}/customfields/{1}/delete", mailingListId, customFieldId), null, token).ConfigureAwait(false);
        }

        #endregion

        #region Subscribers

        /// <summary>
        ///     Searches for a subscriber with the specified email address in the specified mailing list and returns detailed information such as id, name, date created, date unsubscribed, status and custom fields.
        /// </summary>
        /// <param name="mailingListId"> The ID of the mailing list to search the subscriber in. </param>
        /// <param name="email"> The email address of the subscriber being searched. </param>
        /// <param name="token"> CancellationToken. </param>
        public async Task<Subscriber> GetSubscriberByEmailAsync(Guid mailingListId, string email, CancellationToken token = default(CancellationToken))
        {
            return await SendAsync<Subscriber>(HttpMethod.Get, string.Format("/subscribers/{0}/view", mailingListId), new { Email = email }, token).ConfigureAwait(false);
        }

        /// <summary>
        ///     Adds a new subscriber to the specified mailing list.
        ///     If there is already a subscriber with the specified email address in the list, an update will be performed instead.
        /// </summary>
        /// <param name="mailingListId"> The ID of the mailing list to add the new member. </param>
        /// <param name="member"> New member's parameters. </param>
        /// <param name="token"> Cancellation Token. </param>
        /// <returns> The new subscriber. </returns>
        public async Task<Subscriber> SubscribeMemberAsync(Guid mailingListId, SubscriberParams member, CancellationToken token = default(CancellationToken))
        {
            var parameters = new
            {
                Name = member.Name,
                Email = member.Email,
                CustomFields = member.CustomFields.Select(c => c.Key + "=" + c.Value)
            };

            return await SendAsync<Subscriber>(HttpMethod.Post, string.Format("/subscribers/{0}/subscribe", mailingListId), parameters, token).ConfigureAwait(false);
        }

        /// <summary>
        ///     Updates a subscriber in the specified mailing list. You can even update the subscribers email, if he has not unsubscribed.
        /// </summary>
        /// <param name="mailingListId"> The ID of the mailing list to add the new member. </param>
        /// <param name="subscriberId"> The id of the subscriber to be updated. </param>
        /// <param name="email"> The email address of the member. </param>
        /// <param name="customFields"> Name-value pairs that match the member's custom fields defined in the mailing list. </param>
        /// <param name="token"> Cancellation Token. </param>
        /// <returns></returns>
        public async Task<Subscriber> UpdateMemberAsync(Guid mailingListId, Guid subscriberId, SubscriberParams updatedMember, CancellationToken token = default(CancellationToken))
        {
            var parameters = new
            {
                Name = updatedMember.Name,
                Email = updatedMember.Email,
                CustomFields = updatedMember.CustomFields.Select(c => c.Key + "=" + c.Value)
            };

            return await SendAsync<Subscriber>(HttpMethod.Post, string.Format("/subscribers/{0}/update/{1}", mailingListId, subscriberId), parameters, token).ConfigureAwait(false);
        }

        /// <summary>
        ///     This method allows you to add multiple subscribers in a mailing list with a single call. 
        ///     If some subscribers already exist with the given email addresses, they will be updated.
        ///     If you try to add a subscriber with an invalid email address, this attempt will be ignored, as the process will skip to the next subscriber automatically.
        /// </summary>
        /// <param name="mailingListId"> The ID of the mailing list to add subscribers to. </param>
        /// <param name="subscribers"> A list of subscribers to add to the mailing list. You may specify the email address, the name and the custom fields for each subscriber. </param>
        /// <param name="token"> Cancellation Token. </param>
        /// <returns></returns>
        public async Task<IEnumerable<Subscriber>> SubscribeManyAsync(Guid mailingListId, IList<SubscriberParams> subscribers, CancellationToken token = default(CancellationToken))
        {
            var parameters = new
            {
                Subscribers = subscribers.Select(m => new
                {
                    Name = m.Name,
                    Email = m.Email,
                    CustomFields = m.CustomFields.Select(c => c.Key + "=" + c.Value)
                })
            };

            return await SendAsync<IEnumerable<Subscriber>>(HttpMethod.Post, string.Format("/subscribers/{0}/subscribe_many", mailingListId), parameters, token).ConfigureAwait(false);
        }

        /// <summary>
        ///     Unsubscribes a subscriber from the specified mailing list and the specified campaign. 
        ///     The subscriber is not deleted, but moved to the supression list. 
        ///     This call will take into account the setting you have in "Supression list and unsubscribe settings" and will remove the subscriber from all other mailing lists or not accordingly.
        /// </summary>
        /// <param name="mailingListId">
        ///     The ID of the mailing list to unsubscribe the subscriber from. 
        ///     If also omitted, the email address of the subscriber will be unsubscribed from all mailing lists. </param>
        /// <param name="campaignId">
        ///     The ID of the campaign from which the subscriber unsubscribed. It can be omitted if no such information is available.
        /// </param>
        /// <param name="email"> The email address of the subscriber to be supressed. </param>
        /// <param name="token"> Cancellation Token. </param>
        /// <returns></returns>
        public async Task<bool> UnsubscribeMemberAsync(Guid mailingListId, Guid campaignId, string email, CancellationToken token = default(CancellationToken))
        {
            return await SendAsync<bool>(HttpMethod.Post, string.Format("/subscribers/{0}/{1}/unsubscribe", mailingListId, campaignId ), new { Email = email }, token).ConfigureAwait(false);
        }

        /// <summary>
        ///     Removes a subscriber from the specified mailing list permanently (without moving to the supression list).
        /// </summary>
        /// <param name="mailingListId"> The ID of the mailing list to search the subscriber in. </param>
        /// <param name="email"> The email address of the subscriber being searched. </param>
        /// <param name="token"> Cancellation Token. </param>
        /// <returns></returns>
        public async Task<bool> RemoveMemberAsync(Guid mailingListId, string email, CancellationToken token = default(CancellationToken))
        {
            return await SendAsync<bool>(HttpMethod.Post, string.Format("/subscribers/{0}/remove", mailingListId), new { Email = email }, token).ConfigureAwait(false);
        }

        /// <summary>
        ///     Removes a list of subscribers from the specified mailing list permanently (without putting them in the supression list). 
        ///     Any invalid email addresses specified will be ignored.
        /// </summary>
        /// <param name="mailingListId"> The ID of the mailing list to remove subscribers from. </param>
        /// <param name="emails"> A list of email addresses to be removed </param>
        /// <param name="token"> Cancellation Token. </param>
        /// <returns></returns>
        public async Task<bool> RemoveManyAsync(Guid mailingListId, IList<string> emails, CancellationToken token = default(CancellationToken))
        {
            return await SendAsync<bool>(HttpMethod.Post, string.Format("/subscribers/{0}/remove_many", mailingListId), new { emails = string.Join(",", emails.ToArray()) }, token).ConfigureAwait(false);
        }

        #endregion

        #region Segments

        /// <summary> Get a list of all segments with their criteria for the given mailing list. </summary>
        /// <param name="mailingListId"> The ID of the mailing list to retrieve the segments for. </param>
        /// <param name="page">
        ///     The page number to display results for. If not specified, the first page will be returned.
        /// </param>
        /// <param name="pageSize">
        ///     The maximum number of results per page. This must be a positive integer up to 100. If not specified, 100 results per page will be returned.
        ///     If a value greater than 100 is specified, it will be treated as 100.
        /// </param>
        /// <param name="token"> Cancellation Token. </param>
        public async Task<SegmentsResult> GetSegmentsForListAsync(Guid mailingListId, int page = 1, int pageSize = 100, CancellationToken token = default(CancellationToken))
        {
            var parameters = new
            {
                Page = page,
                PageSize = pageSize
            };

            var segments = await SendAsync<SegmentsResult>(HttpMethod.Get, string.Format("/lists/{0}/segments", mailingListId), parameters, token).ConfigureAwait(false);

            foreach (var s in segments.Segments)
            {
                s.MailingListId = mailingListId;
            }

            return segments;
        }

        /// <summary>
        ///     Creates a new empty segment (without criteria) for the given mailing list. 
        ///     You may specify the name of the segment and the way the criteria will match together.
        /// </summary>
        /// <param name="mailingListId"> The ID of the mailing list where the segment belongs. </param>
        /// <param name="name"> The name of the segment. </param>
        /// <param name="matchType"> Specifies how the segment's criteria will match together. </param>
        /// <param name="token"> Cancellation Token. </param>
        /// <returns> New segment's ID. </returns>
        public async Task<int> CreateSegmentAsync(Guid mailingListId, string name, SegmentMatchType matchType = SegmentMatchType.All, CancellationToken token = default(CancellationToken))
        {
            var parameters = new
            {
                Name = name,
                MatchType = matchType
            };

            return await SendAsync<int>(HttpMethod.Post, string.Format("/lists/{0}/segments/create", mailingListId), parameters, token).ConfigureAwait(false);
        }

        #endregion

        #region Generic API calling method and helpers

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

        private async Task<TModel> GetResponse<TModel>(HttpResponseMessage response)
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
