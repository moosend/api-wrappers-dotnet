# Moosend C# Wrapper

The following project is a C# implementation of the Moosend V3 API.
You can find the API documentation at http://docs.moosendapp.apiary.io/#

<a name="frameworks-supported"></a>
## Frameworks supported
- .NET 4.0 or later

<a name="installation"></a>
## Installation

Just type in the Package Manage Console
```
Install-Package Moosend.Wrappers.CSharpWrapper
```

*OR*

Download it directly from the `NuGet Package Manager`

<a name="getting-started"></a>
## Getting Started

```csharp
using System;
using System.Diagnostics;
using Moosend.Wrappers.CSharpWrapper.Api;
using Moosend.Wrappers.CSharpWrapper.Client;
using Moosend.Wrappers.CSharpWrapper.Model;

namespace Example
{
    public class CreatingADraftCampaignExample
    {
        public void main()
        {
            var apiInstance = new CampaignsApi();
            var format = format_example;  // string | 
            var apikey = apikey_example;  // string | You may find your API Key or generate a new one in your account settings.
            var body = new CreatingADraftCampaignRequest(); // CreatingADraftCampaignRequest | 

            try
            {
                // Creating A Draft Campaign
                CreatingADraftCampaignResponse result = apiInstance.CreatingADraftCampaign(format, apikey, body);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling CampaignsApi.CreatingADraftCampaign: " + e.Message );
            }
        }
    }
}
```

<a name="documentation-for-api-endpoints"></a>
## Documentation for API Endpoints

## *CampaignsApi*
Class | Method 
------------ | ------------- 
[**GetAllCampaigns**](docs/CampaignsApi.md#getallcampaigns) | Returns a list of all campaigns in your account with detailed information.  
[**GetCampaignsByPage**](docs/CampaignsApi.md#getcampaignsbypage) | Returns a list of all campaigns in your account with detailed information, paging information is required as input.
[**GetCampaignsByPageAndPagesize**](docs/CampaignsApi.md#getcampaignsbypageandpagesize) | Returns a list of all campaigns in your account with detailed information, paging information is required as input.
[**GettingCampaignDetails**](docs/CampaignsApi.md#gettingcampaigndetails) | Returns a complete set of properties that describe the requested campaign in detail.  
[**GettingSenderDetails**](docs/CampaignsApi.md#gettingsenderdetails) | Returns basic information for the specified sender identified by its email address.
[**CloningAnExistingCampaign**](docs/CampaignsApi.md#cloninganexistingcampaign) | Creates an exact copy of an existing campaign. The new campaign is created as a draft.
[**CreatingADraftCampaign**](docs/CampaignsApi.md#creatingadraftcampaign) | Creates a new campaign in your account. This method does not send the campaign, but rather creates it as a draft, ready for sending or testing. 
[**UpdatingADraftCampaign**](docs/CampaignsApi.md#updatingadraftcampaign) | Updates properties of an existing draft A/B campaign in your account. Non-draft campaigns cannot be updated. 
[**DeletingACampaign**](docs/CampaignsApi.md#deletingacampaign) | Deletes a campaign from your account, draft or even sent.
[**TestingACampaign**](docs/CampaignsApi.md#testingacampaign) | Sends a test email of a draft campaign to a list of email addresses you specify for previewing.
[**SendingACampaign**](docs/CampaignsApi.md#sendingacampaign) | Sends an existing draft campaign to all recipients specified in its mailing list. The campaign is sent immediatelly.
[**ABTestCampaignSummary**](docs/CampaignsApi.md#abtestcampaignsummary) |  Provides a basic summary of the results for a sent AB test campaign, separately for each version (A and B), such as the number of recipients, opens, clicks, bounces, unsubscribes, forwards etc to date.
[**ActivityByLocation**](docs/CampaignsApi.md#activitybylocation) |  Returns a detailed report of your campaign opens (unique and total) by country.
[**CampaignSummary**](docs/CampaignsApi.md#campaignsummary) | Provides a basic summary of the results for any sent campaign such as the number of recipients, opens, clicks, bounces, unsubscribes, forwards etc. to date.
[**GettingAllYourSenders**](docs/CampaignsApi.md#gettingallyoursenders) | Gets a list of your active senders in your account. You may specify any email address of these senders when sending a campaign.
[**LinkActivity**](docs/CampaignsApi.md#linkactivity) | Returns a list with your campaign links and how many clicks have been made by your recipients, either unique or total.
[**SchedulingACampaign**](docs/CampaignsApi.md#schedulingacampaign) | Assigns a scheduled date and time at which the campaign will be delivered.
[**UnschedulingACampaign**](docs/CampaignsApi.md#unschedulingacampaign) | Removes a previously defined scheduled date and time from a campaign, so that it will be delivered immediately if already queued or when sent.

## *MailingListsApi*
Class | Method 
------------ | ------------- 
[**CreatingACustomField**](docs/MailingListsApi.md#creatingacustomfield) | Creates a new custom field in the specified mailing list.
[**CreatingAMailingList**](docs/MailingListsApi.md#creatingamailinglist) | Creates a new empty mailing list in your account.
[**DeletingAMailingList**](docs/MailingListsApi.md#deletingamailinglist) | Deletes a mailing list from your account.
[**GettingAllActiveMailingLists**](docs/MailingListsApi.md#gettingallactivemailinglists) | Gets a list of your active mailing lists in your account.
[**GettingAllActiveMailingListsWithPaging**](docs/MailingListsApi.md#gettingallactivemailinglistswithpaging) | Gets a list of your active mailing lists in your account. Because the results for this call could be quite big, paging information is required as input.
[**GettingMailingListDetails**](docs/MailingListsApi.md#gettingmailinglistdetails) | Gets details for a given mailing list. You may include subscriber statistics in your results or not. Any segments existing for the requested mailing list will not be included in the results.
[**RemovingACustomField**](docs/MailingListsApi.md#removingacustomfield) | Removes a custom field definition from the specified mailing list.
[**UpdatingACustomField**](docs/MailingListsApi.md#updatingacustomfield) | Updates the properties of an existing custom field in the specified mailing list.
[**UpdatingAMailingList**](docs/MailingListsApi.md#updatingamailinglist) | Updates the properties of an existing mailing list.

## *SegmentsApi*
Class | Method 
------------ | ------------- 
[**GettingSegments**](docs/SegmentsApi.md#gettingsegments) | Get a list of all segments with their criteria for the given mailing list.
[**GettingSegmentDetails**](docs/SegmentsApi.md#gettingsegmentdetails) | Gets detailed information on a specific segment and its criteria. However, it does not include the subscribers returned by the segment.
[**GettingSegmentSubscribers**](docs/SegmentsApi.md#gettingsegmentsubscribers) | Gets a list of the subscribers that the specified segment returns according to its criteria. Because the results for this call could be quite big, paging information is required as input.
[**CreatingANewSegment**](docs/SegmentsApi.md#creatinganewsegment) | Creates a new empty segment (without criteria) for the given mailing list. You may specify the name of the segment and the way the criteria will match together.
[**UpdatingASegment**](docs/SegmentsApi.md#updatingasegment) | Updates the properties of an existing segment. You may update the name and match type of the segment.
[**AddingCriteriaToSegments**](docs/SegmentsApi.md#addingcriteriatosegments) | Adds a new criterion (a rule) to the specified segment.
[**UpdatingSegmentCriteria**](docs/SegmentsApi.md#updatingsegmentcriteria) | Updates an existing criterion in the specified segment.
[**DeletingASegment**](docs/SegmentsApi.md#deletingasegment) | Deletes a segment along with its criteria from the mailing list. The subscribers of the mailing list that the segment returned are not deleted or affected in any way.

## *SubscribersApi*
Class | Method 
------------ | ------------- 
[**GettingSubscribers**](docs/SubscribersApi.md#gettingsubscribers) | Gets a list of all subscribers in a given mailing list. You may filter the list by setting a date to fetch those subscribed since then and/or by their status. 
[**GetSubscriberByEmailAddress**](docs/SubscribersApi.md#getsubscriberbyemailaddress) | Searches for a subscriber with the specified email address in the specified mailing list.
[**GetSubscriberById**](docs/SubscribersApi.md#getsubscriberbyid) | Searches for a subscriber with the specified unique id in the specified mailing list
[**AddingSubscribers**](docs/SubscribersApi.md#addingsubscribers) | Adds a new subscriber to the specified mailing list. If there is already a subscriber with the specified email address in the list, an update will be performed instead.
[**AddingMultipleSubscribers**](docs/SubscribersApi.md#addingmultiplesubscribers) | This method allows you to add multiple subscribers in a mailing list with a single call. If some subscribers already exist with the given email addresses, they will be updated. 
[**UpdatingASubscriber**](docs/SubscribersApi.md#updatingasubscriber) | Updates a subscriber in the specified mailing list. You can even update the subscribers email, if he has not unsubscribed.
[**UnsubscribingSubscribersFromAccount**](docs/SubscribersApi.md#unsubscribingsubscribersfromaccount) | Unsubscribes a subscriber from the account.
[**UnsubscribingSubscribersFromMailingList**](docs/SubscribersApi.md#unsubscribingsubscribersfrommailinglist) | Unsubscribes a subscriber from the specified mailing list. The subscriber is not deleted, but moved to the suppression list.
[**UnsubscribingSubscribersFromMailingListAndASpecifiedCampaign**](docs/SubscribersApi.md#unsubscribingsubscribersfrommailinglistandaspecifiedcampaign) | Unsubscribes a subscriber from the specified mailing list and the specified campaign. The subscriber is not deleted, but moved to the suppression list. 
[**RemovingASubscriber**](docs/SubscribersApi.md#removingasubscriber) | Removes a subscriber from the specified mailing list permanently (without moving to the suppression list).
[**RemovingMultipleSubscribers**](docs/SubscribersApi.md#removingmultiplesubscribers) | Removes a list of subscribers from the specified mailing list permanently (without putting them in the suppression list). Any invalid email addresses specified will be ignored.


<a name="documentation-for-models"></a>
## Documentation for Models

 - [Model.A](docs/A.md)
 - [Model.ABCampaignData](docs/ABCampaignData.md)
 - [Model.AbTestCampaignSummaryResponse](docs/AbTestCampaignSummaryResponse.md)
 - [Model.ActivityByLocationResponse](docs/ActivityByLocationResponse.md)
 - [Model.AddingCriteriaToSegmentsRequest](docs/AddingCriteriaToSegmentsRequest.md)
 - [Model.AddingCriteriaToSegmentsResponse](docs/AddingCriteriaToSegmentsResponse.md)
 - [Model.AddingMultipleSubscribersRequest](docs/AddingMultipleSubscribersRequest.md)
 - [Model.AddingMultipleSubscribersResponse](docs/AddingMultipleSubscribersResponse.md)
 - [Model.AddingSubscribersRequest](docs/AddingSubscribersRequest.md)
 - [Model.AddingSubscribersResponse](docs/AddingSubscribersResponse.md)
 - [Model.Analytic](docs/Analytic.md)
 - [Model.B](docs/B.md)
 - [Model.Campaign](docs/Campaign.md)
 - [Model.CampaignSummaryResponse](docs/CampaignSummaryResponse.md)
 - [Model.CloningAnExistingCampaignResponse](docs/CloningAnExistingCampaignResponse.md)
 - [Model.Context](docs/Context.md)
 - [Model.Context110](docs/Context110.md)
 - [Model.Context118](docs/Context118.md)
 - [Model.Context132](docs/Context132.md)
 - [Model.Context140](docs/Context140.md)
 - [Model.Context145](docs/Context145.md)
 - [Model.Context148](docs/Context148.md)
 - [Model.Context17](docs/Context17.md)
 - [Model.Context32](docs/Context32.md)
 - [Model.Context37](docs/Context37.md)
 - [Model.Context52](docs/Context52.md)
 - [Model.Context64](docs/Context64.md)
 - [Model.Context66](docs/Context66.md)
 - [Model.Context72](docs/Context72.md)
 - [Model.Context84](docs/Context84.md)
 - [Model.Context89](docs/Context89.md)
 - [Model.Context93](docs/Context93.md)
 - [Model.CreatingACustomFieldRequest](docs/CreatingACustomFieldRequest.md)
 - [Model.CreatingACustomFieldResponse](docs/CreatingACustomFieldResponse.md)
 - [Model.CreatingADraftCampaignRequest](docs/CreatingADraftCampaignRequest.md)
 - [Model.CreatingADraftCampaignResponse](docs/CreatingADraftCampaignResponse.md)
 - [Model.CreatingAMailingListRequest](docs/CreatingAMailingListRequest.md)
 - [Model.CreatingAMailingListResponse](docs/CreatingAMailingListResponse.md)
 - [Model.CreatingANewSegmentRequest](docs/CreatingANewSegmentRequest.md)
 - [Model.CreatingANewSegmentResponse](docs/CreatingANewSegmentResponse.md)
 - [Model.Criterion](docs/Criterion.md)
 - [Model.CustomField](docs/CustomField.md)
 - [Model.CustomField53](docs/CustomField53.md)
 - [Model.CustomFieldsDefinition](docs/CustomFieldsDefinition.md)
 - [Model.DeletingACampaignResponse](docs/DeletingACampaignResponse.md)
 - [Model.DeletingAMailingListResponse](docs/DeletingAMailingListResponse.md)
 - [Model.DeletingASegmentResponse](docs/DeletingASegmentResponse.md)
 - [Model.Format](docs/Format.md)
 - [Model.GetAllCampaignsResponse](docs/GetAllCampaignsResponse.md)
 - [Model.GetCampaignStatisticsResponse](docs/GetCampaignStatisticsResponse.md)
 - [Model.GetCampaignStatisticsWithPagingFilteredResponse](docs/GetCampaignStatisticsWithPagingFilteredResponse.md)
 - [Model.GetCampaignsByPageAndPagesizeResponse](docs/GetCampaignsByPageAndPagesizeResponse.md)
 - [Model.GetCampaignsByPageResponse](docs/GetCampaignsByPageResponse.md)
 - [Model.GetSubscriberByEmailAddressResponse](docs/GetSubscriberByEmailAddressResponse.md)
 - [Model.GetSubscriberByIdResponse](docs/GetSubscriberByIdResponse.md)
 - [Model.GettingAllActiveMailingListsResponse](docs/GettingAllActiveMailingListsResponse.md)
 - [Model.GettingAllActiveMailingListsWithPagingResponse](docs/GettingAllActiveMailingListsWithPagingResponse.md)
 - [Model.GettingAllYourSendersResponse](docs/GettingAllYourSendersResponse.md)
 - [Model.GettingCampaignDetailsResponse](docs/GettingCampaignDetailsResponse.md)
 - [Model.GettingMailingListDetailsResponse](docs/GettingMailingListDetailsResponse.md)
 - [Model.GettingSegmentDetailsResponse](docs/GettingSegmentDetailsResponse.md)
 - [Model.GettingSegmentSubscribersResponse](docs/GettingSegmentSubscribersResponse.md)
 - [Model.GettingSegmentsResponse](docs/GettingSegmentsResponse.md)
 - [Model.GettingSenderDetailsResponse](docs/GettingSenderDetailsResponse.md)
 - [Model.GettingSubscribersResponse](docs/GettingSubscribersResponse.md)
 - [Model.ImportOperation](docs/ImportOperation.md)
 - [Model.ImportOperation19](docs/ImportOperation19.md)
 - [Model.LinkActivityResponse](docs/LinkActivityResponse.md)
 - [Model.MailingList](docs/MailingList.md)
 - [Model.MailingList68](docs/MailingList68.md)
 - [Model.MailingList69](docs/MailingList69.md)
 - [Model.MailingList85](docs/MailingList85.md)
 - [Model.MailingLists](docs/MailingLists.md)
 - [Model.MailingLists119](docs/MailingLists119.md)
 - [Model.MailingLists134](docs/MailingLists134.md)
 - [Model.Paging](docs/Paging.md)
 - [Model.Paging76](docs/Paging76.md)
 - [Model.RemovingACustomFieldResponse](docs/RemovingACustomFieldResponse.md)
 - [Model.RemovingASubscriberRequest](docs/RemovingASubscriberRequest.md)
 - [Model.RemovingASubscriberResponse](docs/RemovingASubscriberResponse.md)
 - [Model.RemovingMultipleSubscribersRequest](docs/RemovingMultipleSubscribersRequest.md)
 - [Model.RemovingMultipleSubscribersResponse](docs/RemovingMultipleSubscribersResponse.md)
 - [Model.ReplyToEmail](docs/ReplyToEmail.md)
 - [Model.SchedulingACampaignRequest](docs/SchedulingACampaignRequest.md)
 - [Model.SchedulingACampaignResponse](docs/SchedulingACampaignResponse.md)
 - [Model.Segment](docs/Segment.md)
 - [Model.Sender](docs/Sender.md)
 - [Model.SendingACampaignResponse](docs/SendingACampaignResponse.md)
 - [Model.ShortBy](docs/ShortBy.md)
 - [Model.SortMethod](docs/SortMethod.md)
 - [Model.Status](docs/Status.md)
 - [Model.Subscriber](docs/Subscriber.md)
 - [Model.Subscribers](docs/Subscribers.md)
 - [Model.Subscribers150](docs/Subscribers150.md)
 - [Model.TestingACampaignRequest](docs/TestingACampaignRequest.md)
 - [Model.TestingACampaignResponse](docs/TestingACampaignResponse.md)
 - [Model.Type](docs/Type.md)
 - [Model.UnschedulingACampaignResponse](docs/UnschedulingACampaignResponse.md)
 - [Model.UnsubscribingSubscribersFromAccountRequest](docs/UnsubscribingSubscribersFromAccountRequest.md)
 - [Model.UnsubscribingSubscribersFromAccountResponse](docs/UnsubscribingSubscribersFromAccountResponse.md)
 - [Model.UnsubscribingSubscribersFromMailingListAndASpecifiedCampaignRequest](docs/UnsubscribingSubscribersFromMailingListAndASpecifiedCampaignRequest.md)
 - [Model.UnsubscribingSubscribersFromMailingListAndASpecifiedCampaignResponse](docs/UnsubscribingSubscribersFromMailingListAndASpecifiedCampaignResponse.md)
 - [Model.UnsubscribingSubscribersFromMailingListRequest](docs/UnsubscribingSubscribersFromMailingListRequest.md)
 - [Model.UnsubscribingSubscribersFromMailingListResponse](docs/UnsubscribingSubscribersFromMailingListResponse.md)
 - [Model.UpdatingACustomFieldRequest](docs/UpdatingACustomFieldRequest.md)
 - [Model.UpdatingACustomFieldResponse](docs/UpdatingACustomFieldResponse.md)
 - [Model.UpdatingADraftCampaignRequest](docs/UpdatingADraftCampaignRequest.md)
 - [Model.UpdatingADraftCampaignResponse](docs/UpdatingADraftCampaignResponse.md)
 - [Model.UpdatingAMailingListRequest](docs/UpdatingAMailingListRequest.md)
 - [Model.UpdatingAMailingListResponse](docs/UpdatingAMailingListResponse.md)
 - [Model.UpdatingASegmentRequest](docs/UpdatingASegmentRequest.md)
 - [Model.UpdatingASegmentResponse](docs/UpdatingASegmentResponse.md)
 - [Model.UpdatingASubscriberRequest](docs/UpdatingASubscriberRequest.md)
 - [Model.UpdatingASubscriberResponse](docs/UpdatingASubscriberResponse.md)
 - [Model.UpdatingSegmentCriteriaRequest](docs/UpdatingSegmentCriteriaRequest.md)
 - [Model.UpdatingSegmentCriteriaResponse](docs/UpdatingSegmentCriteriaResponse.md)
 - [Model.WithStatistics](docs/WithStatistics.md)


<a name="documentation-for-authorization"></a>
## Documentation for Authorization

All endpoints do not require authorization.
