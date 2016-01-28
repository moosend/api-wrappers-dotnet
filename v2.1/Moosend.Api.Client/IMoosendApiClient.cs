using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Moosend.Api.Common;
using Moosend.Api.Common.Models;
using Moosend.Api.Common.Responses;

namespace Moosend.Api.Client
{
    public interface IMoosendApiClient
    {
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
        Task<CampaignsResult> GetCampaignsAsync(int page = 1, int pageSize = 10, string sortBy = "CreatedOn", string sortMethod = "ASC", CancellationToken token = default(CancellationToken));

        /// <summary> Returns basic information for the specified sender identified by its email address. </summary>
        /// <param name="email"> The email address of the senders to get information for. </param>
        /// <param name="token"> Cancellation Token. </param>
        Task<Sender> GetSenderByEmailAsync(string email, CancellationToken token = default(CancellationToken));

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
        Task<CampaignsStatisticsResult> GetCampaignStatisticsAsync(Guid campaignId, MailStatus type = MailStatus.Sent, int page = 1, int pageSize = 50, DateTime? from = null, DateTime? to = null, CancellationToken token = default(CancellationToken));

        /// <summary> Returns a list with your campaign links and how many clicks have been made by your recipients, either unique or total. /// </summary>
        /// <param name="campaignId"> The ID of the requested campaign </param>
        /// <param name="token"> Cancellation Token. </param>
        Task<CampaignsStatisticsResult> GetCampaignLinkActivityAsync(Guid campaignId, CancellationToken token = default(CancellationToken));

        /// <summary> Returns a detailed report of your campaign activity (opens, clicks, etc) by country. </summary>
        /// <param name="campaignId"> The ID of the requested campaign. </param>
        /// <param name="token"></param>
        Task<CampaignsStatisticsResult> GetCampaignActivityByLocationAsync(Guid campaignId, CancellationToken token = default(CancellationToken));

        /// <summary> Updates properties of an existing draft campaign in your account. Non-draft campaigns cannot be updated. </summary>
        /// <param name="campaignId"> The ID of the draft campaign to update. </param>
        /// <param name="campaignParams"> Updated parameters to update in campaign. </param>
        /// <param name="token"> Cancellation Token. </param>
        /// <returns></returns>
        Task<bool> UpdateCampaignAsync(Guid campaignId, CampaignParams campaignParams, CancellationToken token = default(CancellationToken));

        #endregion

        #region Mailing Lists

        /// <summary> Gets a list of your active mailing lists in your account. </summary>
        /// <param name="page"> The page number to display results for. If not specified, the first page will be returned. </param>
        /// <param name="pageSize"> The maximum number of results per page. If ommitted, 10 mailing lists will be returned per page. </param>
        /// <param name="token"> Cancellation Token. </param>
        Task<MailingListsResult> GetMailingListsAsync(int page = 1, int pageSize = 10, CancellationToken token = default(CancellationToken));

        /// <summary> Creates a new empty mailing list in your account. </summary>
        /// <param name="name"> The name of the new mailing list. </param>
        /// <param name="confirmationPage"> The URL of the page that will be displayed at the end of the subscription process. </param>
        /// <param name="redirectAfterUnsubscribePage"> The URL of the page that users will be redirected after unsubscribing from your mailing list. </param>
        /// <param name="token"> Cancellation Token. </param>
        /// <returns></returns>
        Task<Guid> CreateMailingListAsync(string name, string confirmationPage = null, string redirectAfterUnsubscribePage = null, CancellationToken token = default(CancellationToken));

        /// <summary> Updates the properties of an existing mailing list. </summary>
        /// <param name="listId"> The ID of the mailing list to update. </param>
        /// <param name="name"> The name of the new mailing list. </param>
        /// <param name="confirmationPage"> The URL of the page that will be displayed at the end of the subscription process. </param>
        /// <param name="redirectAfterUnsubscribePage"> The URL of the page that users will be redirected after unsubscribing from your mailing list. </param>
        /// <param name="token"> Cancellation Token. </param>
        /// <returns> The Guid of the mailing list. </returns>
        Task<Guid> UpdateMailingListAsync(Guid listId, string name, string confirmationPage = null, string redirectAfterUnsubscribePage = null, CancellationToken token = default(CancellationToken));

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
        Task<SubscribersResult> GetSubscribersAsync(Guid mailingListId, SubscribeType? status, DateTime? since = null, int page = 1, int pageSize = 500, CancellationToken token = default(CancellationToken));

        /// <summary>
        ///     Gets details for a given mailing list. You may include subscriber statistics in your results or not. 
        ///     Any segments existing for the requested mailing list will not be included in the results.
        /// </summary>
        /// <param name="mailingListId"> The ID of the mailing list to be returned. </param>
        /// <param name="withStatistics"> 
        ///     Specifies whether to fetch statistics for the subscribers or not. If ommitted, results will be returned with statistics by default.
        ///     Specified value should be either 'true' of 'false' (without quotes). </param>
        /// <param name="token"> Cancellation Token. </param>
        Task<MailingList> GetMailingListByIdAsync(Guid mailingListId, bool withStatistics = true, CancellationToken token = default(CancellationToken));

        /// <summary> Deletes a mailing list from your account. </summary>
        /// <param name="mailingListId"> The ID of the mailing list to be deleted. </param>
        /// <param name="token"> Ceancellation Token. </param>
        /// <returns> A boolean value indicating success. </returns>
        Task<bool> DeleteMailingListAsync(Guid mailingListId, CancellationToken token = default(CancellationToken));

        /// <summary> Creates a new custom field in the specified mailing list. </summary>
        /// <param name="mailingListId"> The id of the mailing list where the custom field will belong to. </param>
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
        /// <returns> The Guid of the new  Custom Field. </returns>
        Task<Guid> CreateCustomFieldAsync(Guid mailingListId, string name, CustomFieldType customFieldType = CustomFieldType.Text, bool isRequired = false, string options = null, CancellationToken token = default(CancellationToken));

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
        /// <returns> Custom field's Guid. </returns>
        Task<Guid> UpdateCustomFieldAsync(Guid mailingListId, Guid customFieldId, string name, CustomFieldType customFieldType = CustomFieldType.Text, bool isRequired = false, string options = null, CancellationToken token = default(CancellationToken));

        /// <summary> Removes a custom field definition from the specified mailing list. </summary>
        /// <param name="mailingListId"> The ID of the mailing list where the custom field belongs. </param>
        /// <param name="customFieldId"> The ID of the custom field to be removed. </param>
        /// <param name="token"> Cancellation Token. </param>
        /// <returns> A boolean indicating success. </returns>
        Task<bool> DeleteCustomFieldAsync(Guid mailingListId, Guid customFieldId, CancellationToken token = default(CancellationToken));

        #endregion

        #region Subscribers

        /// <summary>
        ///     Searches for a subscriber with the specified email address in the specified mailing list and returns detailed information such as id, name, date created, date unsubscribed, status and custom fields.
        /// </summary>
        /// <param name="mailingListId"> The ID of the mailing list to search the subscriber in. </param>
        /// <param name="email"> The email address of the subscriber being searched. </param>
        /// <param name="token"> CancellationToken. </param>
        Task<Subscriber> GetSubscriberByEmailAsync(Guid mailingListId, string email, CancellationToken token = default(CancellationToken));

        #endregion
    }
}