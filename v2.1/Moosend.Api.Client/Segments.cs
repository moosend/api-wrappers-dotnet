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

        /// <summary>
        ///     Updates the properties of an existing segment. You may update the name and match type of the segment.
        /// </summary>
        /// <param name="mailingListId"> The ID of the mailing list where the segment belongs. </param>
        /// <param name="segmentId"> The ID of the segment to update. </param>
        /// <param name="name"> The name of the segment. </param>
        /// <param name="matchType"> Specifies how the segment's criteria will match together. </param>
        /// <param name="token"> Cancellation Token. </param>
        /// <returns> A boolean value indicating success. </returns>
        public async Task<bool> UpdateSegmentAsync(Guid mailingListId, int segmentId, string name, SegmentMatchType matchType = SegmentMatchType.All, CancellationToken token = default(CancellationToken))
        {
            var parameters = new
            {
                Name = name,
                MatchType = matchType
            };

            return await SendAsync<bool>(HttpMethod.Post, string.Format("/lists/{0}/segments/{1}/update", mailingListId, segmentId), parameters, token).ConfigureAwait(false);
        }

        /// <summary>
        ///     Deletes a segment along with its criteria from the mailing list.
        ///     The subscribers of the mailing list that the segment returned are not deleted or affected in any way.
        /// </summary>
        /// <param name="mailingListId"> The ID of the mailing list where the segment belongs. </param>
        /// <param name="segmentId"> The ID of the segment to be deleted. </param>
        /// <param name="token"> Cancellation Token. </param>
        /// <returns> A boolean value indicating success. </returns>
        public async Task<bool> DeleteSegmentAsync(Guid mailingListId, int segmentId, CancellationToken token = default(CancellationToken))
        {
            return await SendAsync<bool>(HttpMethod.Delete, string.Format("/lists/{0}/segments/{1}/delete", mailingListId, segmentId), null, token).ConfigureAwait(false);
        }

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
        public async Task<int> AddSegmentCriteriaAsync(Guid mailingListId, int segmentId, string field, SegmentCriteriaComparer comparer, string value, DateTime? dateFrom = null, DateTime? dateTo = null, CancellationToken token = default(CancellationToken))
        {
            var parameters = new
            {
                Field = field,
                Comparer = comparer,
                Value = value,
                // TODO check for bug in API as I cannot set the right date times. Maybe is a format issue.
                DateFrom = dateFrom.HasValue ? dateFrom.Value.ToString("dd-MM-yy") : null,
                DateTo = dateTo.HasValue ? dateTo.Value.ToString("dd-MM-yy") : null
            };

            return await SendAsync<int>(HttpMethod.Post, string.Format("/lists/{0}/segments/{1}/criteria/add", mailingListId, segmentId), parameters, token).ConfigureAwait(false);
        }

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
        public async Task<bool> UpdateSegmentCriteriaAsync(Guid mailingListId, int segmentId, int criteriaId, string field, SegmentCriteriaComparer comparer, string value, DateTime? dateFrom = null, DateTime? dateTo = null, CancellationToken token = default(CancellationToken))
        {
            var parameters = new
            {
                Field = field,
                Comparer = comparer,
                Value = value,
                DateFrom = dateFrom,
                DateTo = dateTo
            };

            return await SendAsync<bool>(HttpMethod.Post, string.Format("/lists/{0}/segments/{1}/criteria/{2}/update", mailingListId, segmentId, criteriaId), parameters, token).ConfigureAwait(false);
        }

        /// <summary>
        ///     Gets detailed information on a specific segment and its criteria.
        ///     However, it does not include the subscribers returned by the segment.
        /// </summary>
        /// <param name="mailingListId"> The ID of the mailing list the specified segment belongs. </param>
        /// <param name="segmentId"> The ID of the segment to fetch results for. </param>
        /// <param name="token"> Cancellation Token. </param>
        public async Task<Segment> GetSegmentById(Guid mailingListId, int segmentId, CancellationToken token = default(CancellationToken))
        {
            return await SendAsync<Segment>(HttpMethod.Get, string.Format("/lists/{0}/segments/{1}/details", mailingListId, segmentId), null, token).ConfigureAwait(false);
        }

        /// <summary>
        ///     Gets a list of the subscribers that the specified segment returns according to its criteria. 
        ///     Because the results from this call could be quite big, paging information is required as input.
        /// </summary>
        /// <param name="mailingListId"> The ID of the mailing list the specified segment belongs. </param>
        /// <param name="segmentId"> The ID of the segment to fetch results for. </param>
        /// <param name="status">
        ///     Specifies which subscribers to fetch, according to their status. 
        ///     If ommitted, only active subscribers will be returned.</param>
        /// <param name="page"> The page number to display results for. If not specified, the first page will be returned. </param>
        /// <param name="pageSize"> 
        ///     The maximum number of results per page. This must be a positive integer up to 1000. If not specified, 500 results per page will be returned. 
        ///     If a value greater than 1000 is specified, it will be treated as 1000. </param>
        /// <param name="token"> Cancellation Token. </param>
        /// <returns></returns>
        public async Task<SubscribersResult> GetSegmentSubscribersAsync(Guid mailingListId, int segmentId, SubscribeType? status, int page = 1, int pageSize = 100, CancellationToken token = default(CancellationToken))
        {
            var parameters = new
            {
                Page = page,
                PageSize = pageSize,
                Status = status
            };

            return await SendAsync<SubscribersResult>(HttpMethod.Get, string.Format("/lists/{0}/segments/{1}/members", mailingListId, segmentId), parameters, token).ConfigureAwait(false);
        }
    }
}