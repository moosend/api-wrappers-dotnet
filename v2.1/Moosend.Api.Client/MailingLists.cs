using System;
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
        ///     Gets details for a given mailing list. You may include subscriber statistics in your results or not. 
        ///     Any segments existing for the requested mailing list will not be included in the results.
        /// </summary>
        /// <param name="mailingListId"> The ID of the mailing list to be returned. </param>
        /// <param name="withStatistics">  Specifies whether to fetch statistics for the subscribers or not. If ommitted, results will be returned with statistics by default. </param>
        /// <param name="token"> Cancellation Token. </param>
        public async Task<MailingList> GetMailingListByIdAsync(Guid mailingListId, bool withStatistics = true, CancellationToken token = default(CancellationToken))
        {
            var parameters = new
            {
                WithStatistics = withStatistics,
            };
            return await SendAsync<MailingList>(HttpMethod.Get, string.Format("/lists/{0}/details", mailingListId), parameters, token).ConfigureAwait(false);
        }

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

            var path = status == null
                ? string.Format("/lists/{0}/subscribers", mailingListId)
                : string.Format("/lists/{0}/subscribers/{1}", mailingListId, status);

            return await SendAsync<SubscribersResult>(HttpMethod.Get, path, parameters, token).ConfigureAwait(false);
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
        public async Task<bool> UpdateCustomFieldAsync(Guid mailingListId, Guid customFieldId, string name, CustomFieldType customFieldType = CustomFieldType.Text, bool isRequired = false, string options = null, CancellationToken token = default(CancellationToken))
        {
            var parameters = new
            {
                Name = name,
                CustomFieldType = customFieldType,
                IsRequired = isRequired,
                Options = options
            };

            return await SendAsync<bool>(HttpMethod.Post, string.Format("/lists/{0}/customfields/{1}/update", mailingListId, customFieldId), parameters, token).ConfigureAwait(false);
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
    }
}