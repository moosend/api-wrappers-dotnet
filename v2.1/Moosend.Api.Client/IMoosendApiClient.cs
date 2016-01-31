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
        Task<IEnumerable<Sender>> GetSendersAsync(CancellationToken token = default(CancellationToken));

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

        /// <summary>
        ///     Adds a new subscriber to the specified mailing list.
        ///     If there is already a subscriber with the specified email address in the list, an update will be performed instead.
        /// </summary>
        /// <param name="mailingListId"> The ID of the mailing list to add the new member. </param>
        /// <param name="member"> New member's parameters. </param>
        /// <param name="token"> Cancellation Token. </param>
        /// <returns> The new subscriber. </returns>
        Task<Subscriber> SubscribeMemberAsync(Guid mailingListId, SubscriberParams member, CancellationToken token = default(CancellationToken));

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
        Task<bool> UnsubscribeMemberAsync(Guid mailingListId, Guid campaignId, string email, CancellationToken token = default(CancellationToken));

        /// <summary>
        ///     This method allows you to add multiple subscribers in a mailing list with a single call. 
        ///     If some subscribers already exist with the given email addresses, they will be updated.
        ///     If you try to add a subscriber with an invalid email address, this attempt will be ignored, as the process will skip to the next subscriber automatically.
        /// </summary>
        /// <param name="mailingListId"> The ID of the mailing list to add subscribers to. </param>
        /// <param name="subscribers"> A list of subscribers to add to the mailing list. You may specify the email address, the name and the custom fields for each subscriber. </param>
        /// <param name="token"> Cancellation Token. </param>
        /// <returns></returns>
        Task<IEnumerable<Subscriber>> SubscribeManyAsync(Guid mailingListId, IList<SubscriberParams> subscribers, CancellationToken token = default(CancellationToken));

        /// <summary>
        ///     Removes a subscriber from the specified mailing list permanently (without moving to the supression list).
        /// </summary>
        /// <param name="mailingListId"> The ID of the mailing list to search the subscriber in. </param>
        /// <param name="email"> The email address of the subscriber being searched. </param>
        /// <param name="token"> Cancellation Token. </param>
        /// <returns></returns>
        Task<bool> RemoveMemberAsync(Guid mailingListId, string email, CancellationToken token = default(CancellationToken));

        /// <summary>
        ///     Removes a list of subscribers from the specified mailing list permanently (without putting them in the supression list). 
        ///     Any invalid email addresses specified will be ignored.
        /// </summary>
        /// <param name="mailingListId"> The ID of the mailing list to remove subscribers from. </param>
        /// <param name="emails"> A list of email addresses to be removed </param>
        /// <param name="token"> Cancellation Token. </param>
        /// <returns></returns>
        Task<bool> RemoveManyAsync(Guid mailingListId, IList<string> emails, CancellationToken token = default(CancellationToken));

        /// <summary>
        ///     Updates a subscriber in the specified mailing list. You can even update the subscribers email, if he has not unsubscribed.
        /// </summary>
        /// <param name="mailingListId"> The ID of the mailing list to add the new member. </param>
        /// <param name="subscriberId"> The id of the subscriber to be updated. </param>
        /// <param name="email"> The email address of the member. </param>
        /// <param name="customFields"> Name-value pairs that match the member's custom fields defined in the mailing list. </param>
        /// <param name="token"> Cancellation Token. </param>
        /// <returns></returns>
        Task<Subscriber> UpdateMemberAsync(Guid mailingListId, Guid subscriberId, SubscriberParams updatedMember, CancellationToken token = default(CancellationToken));

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
        Task<SegmentsResult> GetSegmentsForListAsync(Guid mailingListId, int page = 1, int pageSize = 100, CancellationToken token = default(CancellationToken));

        /// <summary>
        ///     Creates a new empty segment (without criteria) for the given mailing list. 
        ///     You may specify the name of the segment and the way the criteria will match together.
        /// </summary>
        /// <param name="mailingListId"> The ID of the mailing list where the segment belongs. </param>
        /// <param name="name"> The name of the segment. </param>
        /// <param name="matchType"> Specifies how the segment's criteria will match together. </param>
        /// <param name="token"> Cancellation Token. </param>
        /// <returns> New segment's ID. </returns>
        Task<int> CreateSegmentAsync(Guid mailingListId, string name, SegmentMatchType matchType = SegmentMatchType.All, CancellationToken token = default(CancellationToken));

        /// <summary>
        ///     Updates the properties of an existing segment. You may update the name and match type of the segment.
        /// </summary>
        /// <param name="mailingListId"> The ID of the mailing list where the segment belongs. </param>
        /// <param name="segmentId"> The ID of the segment to update. </param>
        /// <param name="name"> The name of the segment. </param>
        /// <param name="matchType"> Specifies how the segment's criteria will match together. </param>
        /// <param name="token"> Cancellation Token. </param>
        /// <returns> A boolean value indicating success. </returns>
        Task<bool> UpdateSegmentAsync(Guid mailingListId, int segmentId, string name, SegmentMatchType matchType = SegmentMatchType.All, CancellationToken token = default(CancellationToken));

        /// <summary>
        ///     Deletes a segment along with its criteria from the mailing list.
        ///     The subscribers of the mailing list that the segment returned are not deleted or affected in any way.
        /// </summary>
        /// <param name="mailingListId"> The ID of the mailing list where the segment belongs. </param>
        /// <param name="segmentId"> The ID of the segment to be deleted. </param>
        /// <param name="token"> Cancellation Token. </param>
        /// <returns> A boolean value indicating success. </returns>
        Task<bool> DeleteSegmentAsync(Guid mailingListId, int segmentId, CancellationToken token = default(CancellationToken));

        /// <summary> Adds a new criterion (a rule) to the specified segment. </summary>
        /// <param name="mailingListId"> The ID of the mailing list the criterion's segment belongs to. </param>
        /// <param name="segmentId"> The ID of the segment the criterion belongs to. </param>
        /// <param name="field">
        ///     The field of the criterion to filter the mailing list by. This must be one of the following values:
        ///     <ul>
        ///         <li><strong><pre>DateAdded</pre></strong> : filters subscribers by the date they where added to the mailing list</li>
        ///         <li><strong><pre>RecipientName</pre></strong> : filters subscribers by the recipient name</li>
        ///         <li><strong><pre>RecipientEmail</pre></strong> : filters subscribers by their email address</li>
        ///         <li><strong><pre>CampaignsOpened</pre></strong> : filters subscribers according to how many campaigns they have opened (within the past 60 days maximum)</li>
        ///         <li><strong><pre>LinksClicked</pre></strong> : filters subscribers according to how many links they have clicked from previous campaigns sent to them (within the past 60 days maximum)</li>
        ///         <li><strong><pre>CampaignName</pre></strong> : filters subscribers according to which campaigns they have opened, based on their names</li>
        ///         <li><strong><pre>LinkURL</pre></strong> : filters subscribers according to which links they have clicked, based on their urls</li>
        ///         <li>An ID of any custom field in the mailing list the segment belongs : filters the mailing list according to the value of the specified custom field for each subscriber</li>
        ///     </ul>
        /// </param>
        /// <param name="comparer">
        ///     An operator that defines the way to compare a criterion field with its value. This must be one of the following values:
        ///     <ul>
        ///         <li><strong><pre>Is</pre></strong> : to find subscribers where the field is exactly <u>equal to</u> the search term</li>
        ///         <li><strong><pre>IsNot</pre></strong> : to find subscribers where the field is <u>other than</u> the search term</li>
        ///         <li><strong><pre>Contains</pre></strong> : to find subscribers where the field <u>contains</u> the search term</li>
        ///         <li><strong><pre>DoesNotContain</pre></strong> : to find subscribers where the field <u>does not contain</u> the search term</li>
        ///         <li><strong><pre>StartsWith</pre></strong> : to find subscribers where the field <u>starts with</u> the search term</li>
        ///         <li><strong><pre>DoesNotStartWith</pre></strong> : to find subscribers where the field <u>does not start with</u> the search term</li>
        ///         <li><strong><pre>EndsWith</pre></strong> : to find subscribers where the field <u>ends with</u> the search term</li>
        ///         <li><strong><pre>DoesNotEndWith</pre></strong> : to find subscribers where the field <u>does not end with</u> the search term</li>
        ///         <li><strong><pre>IsGreaterThan</pre></strong> : to find subscribers where the field <u>is greater than</u> the search term</li>
        ///         <li><strong><pre>IsGreaterThanOrEqualTo</pre></strong> : to find subscribers where the field <u>is greater than or equal to</u> the search term</li>
        ///         <li><strong><pre>IsLessThan</pre></strong> : to find subscribers where the field <u>is less than</u> the search term</li>
        ///         <li><strong><pre>IsLessThanOrEqualTo</pre></strong> : to find subscribers where the field <u>is less than or equal to</u> the search term</li>
        ///         <li><strong><pre>IsBefore</pre></strong> : to find subscribers where the specified date field <u>is before</u> the specified date value</li>
        ///         <li><strong><pre>IsAfter</pre></strong> : to find subscribers where the specified date field <u>is after</u> the specified date value</li>
        ///         <li><strong><pre>IsEmpty</pre></strong> : to find subscribers where the field <u>contains no value</u></li>
        ///         <li><strong><pre>IsNotEmpty</pre></strong> : to find subscribers <u>excluding</u> those where the field <u>contains no value</u></li>
        ///         <li><strong><pre>IsTrue</pre></strong> : to find subscribers where the condition defined by the field <u>is true</u></li>
        ///         <li><strong><pre>IsFalse</pre></strong> : to find subscribers where the condition defined by the field <u>is false</u></li>
        ///     </ul>
        ///     If not specified, <pre>Is</pre> will be assumed.
        /// </param>
        /// <param name="value"> A search term to filter the specified field by. </param>
        /// <param name="dateFrom">
        ///     Provides an additional filter option to be combined with the following fields:
        ///     <ul>
        ///         <li><strong><pre>CampaignsOpened</pre></strong> : to search subscribers that opened campaigns that where sent since the specified date</li>
        ///         <li><strong><pre>LinksClicked</pre></strong> : to search subscribers that clicked on links in campaigns that where sent since the specified date</li>
        ///     </ul>
        /// </param>
        /// <param name="dateTo">
        ///     Provides an additional filter option to be combined with the following fields:
        ///     <ul>
        ///         <li><strong><pre>CampaignsOpened</pre></strong> : to search subscribers that opened campaigns that where sent up to the specified date</li>
        ///         <li><strong><pre>LinksClicked</pre></strong> : to search subscribers that clicked on links in campaigns that where sent up to the specified date</li>
        ///     </ul>
        /// </param>
        /// <param name="token"> Cancellation Token. </param>
        /// <returns> The ID of the new segment criteria. </returns>
        Task<int> AddSegmentCriteriaAsync(Guid mailingListId, int segmentId, string field, SegmentCriteriaComparer comparer, String value, DateTime? dateFrom = null, DateTime? dateTo = null, CancellationToken token = default(CancellationToken));

        /// <summary> Updates an existing criterion in the specified segment. </summary>
        /// <param name="mailingListId"> The ID of the mailing list the criterion's segment belongs to. </param>
        /// <param name="segmentId"> The ID of the segment the criterion belongs to. </param>
        /// <param name="criteriaId"> The ID of the criterion to process. </param>
        /// <param name="field">
        ///     The field of the criterion to filter the mailing list by. This must be one of the following values:
        ///     <ul>
        ///         <li><strong><pre>DateAdded</pre></strong> : filters subscribers by the date they where added to the mailing list</li>
        ///         <li><strong><pre>RecipientName</pre></strong> : filters subscribers by the recipient name</li>
        ///         <li><strong><pre>RecipientEmail</pre></strong> : filters subscribers by their email address</li>
        ///         <li><strong><pre>CampaignsOpened</pre></strong> : filters subscribers according to how many campaigns they have opened (within the past 60 days maximum)</li>
        ///         <li><strong><pre>LinksClicked</pre></strong> : filters subscribers according to how many links they have clicked from previous campaigns sent to them (within the past 60 days maximum)</li>
        ///         <li><strong><pre>CampaignName</pre></strong> : filters subscribers according to which campaigns they have opened, based on their names</li>
        ///         <li><strong><pre>LinkURL</pre></strong> : filters subscribers according to which links they have clicked, based on their urls</li>
        ///         <li>An ID of any custom field in the mailing list the segment belongs : filters the mailing list according to the value of the specified custom field for each subscriber</li>
        ///     </ul>
        /// </param>
        /// <param name="comparer">
        ///     An operator that defines the way to compare a criterion field with its value. This must be one of the following values:
        ///     <ul>
        ///         <li><strong><pre>Is</pre></strong> : to find subscribers where the field is exactly <u>equal to</u> the search term</li>
        ///         <li><strong><pre>IsNot</pre></strong> : to find subscribers where the field is <u>other than</u> the search term</li>
        ///         <li><strong><pre>Contains</pre></strong> : to find subscribers where the field <u>contains</u> the search term</li>
        ///         <li><strong><pre>DoesNotContain</pre></strong> : to find subscribers where the field <u>does not contain</u> the search term</li>
        ///         <li><strong><pre>StartsWith</pre></strong> : to find subscribers where the field <u>starts with</u> the search term</li>
        ///         <li><strong><pre>DoesNotStartWith</pre></strong> : to find subscribers where the field <u>does not start with</u> the search term</li>
        ///         <li><strong><pre>EndsWith</pre></strong> : to find subscribers where the field <u>ends with</u> the search term</li>
        ///         <li><strong><pre>DoesNotEndWith</pre></strong> : to find subscribers where the field <u>does not end with</u> the search term</li>
        ///         <li><strong><pre>IsGreaterThan</pre></strong> : to find subscribers where the field <u>is greater than</u> the search term</li>
        ///         <li><strong><pre>IsGreaterThanOrEqualTo</pre></strong> : to find subscribers where the field <u>is greater than or equal to</u> the search term</li>
        ///         <li><strong><pre>IsLessThan</pre></strong> : to find subscribers where the field <u>is less than</u> the search term</li>
        ///         <li><strong><pre>IsLessThanOrEqualTo</pre></strong> : to find subscribers where the field <u>is less than or equal to</u> the search term</li>
        ///         <li><strong><pre>IsBefore</pre></strong> : to find subscribers where the specified date field <u>is before</u> the specified date value</li>
        ///         <li><strong><pre>IsAfter</pre></strong> : to find subscribers where the specified date field <u>is after</u> the specified date value</li>
        ///         <li><strong><pre>IsEmpty</pre></strong> : to find subscribers where the field <u>contains no value</u></li>
        ///         <li><strong><pre>IsNotEmpty</pre></strong> : to find subscribers <u>excluding</u> those where the field <u>contains no value</u></li>
        ///         <li><strong><pre>IsTrue</pre></strong> : to find subscribers where the condition defined by the field <u>is true</u></li>
        ///         <li><strong><pre>IsFalse</pre></strong> : to find subscribers where the condition defined by the field <u>is false</u></li>
        ///     </ul>
        ///     If not specified, <pre>Is</pre> will be assumed.
        /// </param>
        /// <param name="value"> A search term to filter the specified field by. </param>
        /// <param name="dateFrom">
        ///     Provides an additional filter option to be combined with the following fields:
        ///     <ul>
        ///         <li><strong><pre>CampaignsOpened</pre></strong> : to search subscribers that opened campaigns that where sent since the specified date</li>
        ///         <li><strong><pre>LinksClicked</pre></strong> : to search subscribers that clicked on links in campaigns that where sent since the specified date</li>
        ///     </ul>
        /// </param>
        /// <param name="dateTo">
        ///     Provides an additional filter option to be combined with the following fields:
        ///     <ul>
        ///         <li><strong><pre>CampaignsOpened</pre></strong> : to search subscribers that opened campaigns that where sent up to the specified date</li>
        ///         <li><strong><pre>LinksClicked</pre></strong> : to search subscribers that clicked on links in campaigns that where sent up to the specified date</li>
        ///     </ul>
        /// </param>
        /// <param name="token"> Cancellation Token. </param>
        /// <returns> A boolean value indicating succeess. </returns>
        Task<bool> UpdateSegmentCriteriaAsync(Guid mailingListId, int segmentId, int criteriaId, string field, SegmentCriteriaComparer comparer, string value, DateTime? dateFrom = null, DateTime? dateTo = null, CancellationToken token = default(CancellationToken));

        /// <summary>
        ///     Gets detailed information on a specific segment and its criteria.
        ///     However, it does not include the subscribers returned by the segment.
        /// </summary>
        /// <param name="mailingListId"> The ID of the mailing list the specified segment belongs. </param>
        /// <param name="segmentId"> The ID of the segment to fetch results for. </param>
        /// <param name="token"> Cancellation Token. </param>
        Task<Segment> GetSegmentById(Guid mailingListId, int segmentId, CancellationToken token = default(CancellationToken));

        #endregion
    }
}