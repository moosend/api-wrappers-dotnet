# Moosend.Wrappers.CSharpWrapper.Api.CampaignsApi

All URIs are relative to *https://api.moosend.com/v3*

Method | HTTP request | Description
------------- | ------------- | -------------
[**ABTestCampaignSummary**](CampaignsApi.md#abtestcampaignsummary) | **GET** /campaigns/{CampaignID}/view_ab_summary.{Format} | AB Test Campaign Summary
[**ActivityByLocation**](CampaignsApi.md#activitybylocation) | **GET** /campaigns/{CampaignID}/stats/countries.{Format} | Activity By Location
[**CampaignSummary**](CampaignsApi.md#campaignsummary) | **GET** /campaigns/{CampaignID}/view_summary.{Format} | Campaign Summary
[**CloningAnExistingCampaign**](CampaignsApi.md#cloninganexistingcampaign) | **POST** /campaigns/{CampaignID}/clone.{Format} | Cloning An Existing Campaign
[**CreatingADraftCampaign**](CampaignsApi.md#creatingadraftcampaign) | **POST** /campaigns/create.{Format} | Creating A Draft Campaign
[**DeletingACampaign**](CampaignsApi.md#deletingacampaign) | **DELETE** /campaigns/{CampaignID}/delete.{Format} | Deleting A Campaign
[**GetAllCampaigns**](CampaignsApi.md#getallcampaigns) | **GET** /campaigns.{Format} | Get All Campaigns
[**GetCampaignStatisticsWithPagingFiltered**](CampaignsApi.md#getcampaignstatisticswithpagingfiltered) | **GET** /campaigns/{CampaignID}/stats/{Type}.{Format} | Get Campaign Statistics With Paging &amp; Filtered
[**GetCampaignsByPage**](CampaignsApi.md#getcampaignsbypage) | **GET** /campaigns/{Page}.{Format} | Get Campaigns By Page
[**GetCampaignsByPageAndPagesize**](CampaignsApi.md#getcampaignsbypageandpagesize) | **GET** /campaigns/{Page}/{PageSize}.{Format} | Get Campaigns By Page And Pagesize
[**GettingAllYourSenders**](CampaignsApi.md#gettingallyoursenders) | **GET** /senders/find_all.{Format} | Getting All Your Senders
[**GettingCampaignDetails**](CampaignsApi.md#gettingcampaigndetails) | **GET** /campaigns/{CampaignID}/view.{Format} | Getting Campaign Details
[**GettingSenderDetails**](CampaignsApi.md#gettingsenderdetails) | **GET** /senders/find_one.{Format} | Getting Sender Details
[**LinkActivity**](CampaignsApi.md#linkactivity) | **GET** /campaigns/{CampaignID}/stats/links.{Format} | Link Activity
[**SchedulingACampaign**](CampaignsApi.md#schedulingacampaign) | **POST** /campaigns/{CampaignID}/schedule.{Format} | Scheduling A Campaign
[**SendingACampaign**](CampaignsApi.md#sendingacampaign) | **POST** /campaigns/{CampaignID}/send.{Format} | Sending a campaign
[**TestingACampaign**](CampaignsApi.md#testingacampaign) | **POST** /campaigns/{CampaignID}/send_test.{Format} | Testing a campaign
[**UnschedulingACampaign**](CampaignsApi.md#unschedulingacampaign) | **POST** /campaigns/{CampaignID}/unschedule.{Format} | Unscheduling a campaign
[**UpdatingADraftCampaign**](CampaignsApi.md#updatingadraftcampaign) | **POST** /campaigns/{CampaignID}/update.{Format} | Updating A Draft Campaign


<a name="abtestcampaignsummary"></a>
# **ABTestCampaignSummary**
> AbTestCampaignSummaryResponse ABTestCampaignSummary (string format, string apikey, string campaignID)

AB Test Campaign Summary

Provides a basic summary of the results for a sent AB test campaign, separately for each version (A and B), such as the number of recipients, opens, clicks, bounces, unsubscribes, forwards etc to date.

### Example
```csharp
using System;
using System.Diagnostics;
using Moosend.Wrappers.CSharpWrapper.Api;
using Moosend.Wrappers.CSharpWrapper.Client;
using Moosend.Wrappers.CSharpWrapper.Model;

namespace Example
{
    public class ABTestCampaignSummaryExample
    {
        public void main()
        {
            var apiInstance = new CampaignsApi();
            var format = format_example;  // string | 
            var apikey = apikey_example;  // string | You may find your API Key or generate a new one in your account settings.
            var campaignID = campaignID_example;  // string | The ID of the requested AB test campaign

            try
            {
                // AB Test Campaign Summary
                AbTestCampaignSummaryResponse result = apiInstance.ABTestCampaignSummary(format, apikey, campaignID);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling CampaignsApi.ABTestCampaignSummary: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **format** | **string**|  | 
 **apikey** | **string**| You may find your API Key or generate a new one in your account settings. | 
 **campaignID** | **string**| The ID of the requested AB test campaign | 

### Return type

[**AbTestCampaignSummaryResponse**](AbTestCampaignSummaryResponse.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="activitybylocation"></a>
# **ActivityByLocation**
> ActivityByLocationResponse ActivityByLocation (string format, string apikey, string campaignID)

Activity By Location

Returns a detailed report of your campaign opens (unique and total) by country.

### Example
```csharp
using System;
using System.Diagnostics;
using Moosend.Wrappers.CSharpWrapper.Api;
using Moosend.Wrappers.CSharpWrapper.Client;
using Moosend.Wrappers.CSharpWrapper.Model;

namespace Example
{
    public class ActivityByLocationExample
    {
        public void main()
        {
            var apiInstance = new CampaignsApi();
            var format = format_example;  // string | 
            var apikey = apikey_example;  // string | You may find your API Key or generate a new one in your account settings.
            var campaignID = campaignID_example;  // string | The ID of the requested campaign

            try
            {
                // Activity By Location
                ActivityByLocationResponse result = apiInstance.ActivityByLocation(format, apikey, campaignID);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling CampaignsApi.ActivityByLocation: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **format** | **string**|  | 
 **apikey** | **string**| You may find your API Key or generate a new one in your account settings. | 
 **campaignID** | **string**| The ID of the requested campaign | 

### Return type

[**ActivityByLocationResponse**](ActivityByLocationResponse.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="campaignsummary"></a>
# **CampaignSummary**
> CampaignSummaryResponse CampaignSummary (string format, string apikey, string campaignID)

Campaign Summary

Provides a basic summary of the results for any sent campaign such as the number of recipients, opens, clicks, bounces, unsubscribes, forwards etc. to date.

### Example
```csharp
using System;
using System.Diagnostics;
using Moosend.Wrappers.CSharpWrapper.Api;
using Moosend.Wrappers.CSharpWrapper.Client;
using Moosend.Wrappers.CSharpWrapper.Model;

namespace Example
{
    public class CampaignSummaryExample
    {
        public void main()
        {
            var apiInstance = new CampaignsApi();
            var format = format_example;  // string | 
            var apikey = apikey_example;  // string | You may find your API Key or generate a new one in your account settings.
            var campaignID = campaignID_example;  // string | The ID of the requested campaign

            try
            {
                // Campaign Summary
                CampaignSummaryResponse result = apiInstance.CampaignSummary(format, apikey, campaignID);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling CampaignsApi.CampaignSummary: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **format** | **string**|  | 
 **apikey** | **string**| You may find your API Key or generate a new one in your account settings. | 
 **campaignID** | **string**| The ID of the requested campaign | 

### Return type

[**CampaignSummaryResponse**](CampaignSummaryResponse.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="cloninganexistingcampaign"></a>
# **CloningAnExistingCampaign**
> CloningAnExistingCampaignResponse CloningAnExistingCampaign (string format, string campaignID, string apikey)

Cloning An Existing Campaign

Creates an exact copy of an existing campaign. The new campaign is created as a draft.

### Example
```csharp
using System;
using System.Diagnostics;
using Moosend.Wrappers.CSharpWrapper.Api;
using Moosend.Wrappers.CSharpWrapper.Client;
using Moosend.Wrappers.CSharpWrapper.Model;

namespace Example
{
    public class CloningAnExistingCampaignExample
    {
        public void main()
        {
            var apiInstance = new CampaignsApi();
            var format = format_example;  // string | 
            var campaignID = campaignID_example;  // string | 
            var apikey = apikey_example;  // string | You may find your API Key or generate a new one in your account settings.

            try
            {
                // Cloning An Existing Campaign
                CloningAnExistingCampaignResponse result = apiInstance.CloningAnExistingCampaign(format, campaignID, apikey);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling CampaignsApi.CloningAnExistingCampaign: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **format** | **string**|  | 
 **campaignID** | **string**|  | 
 **apikey** | **string**| You may find your API Key or generate a new one in your account settings. | 

### Return type

[**CloningAnExistingCampaignResponse**](CloningAnExistingCampaignResponse.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="creatingadraftcampaign"></a>
# **CreatingADraftCampaign**
> CreatingADraftCampaignResponse CreatingADraftCampaign (string format, string apikey, CreatingADraftCampaignRequest body)

Creating A Draft Campaign

Creates a new campaign in your account. This method does not send the campaign, but rather creates it as a draft, ready for sending or testing.  You can choose to send either a regular campaign or an AB split campaign. Campaign content must be specified from a web location.  Ignore ***(A/B Split Campaign Option)*** if you want to create a regular campaign.

### Example
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

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **format** | **string**|  | 
 **apikey** | **string**| You may find your API Key or generate a new one in your account settings. | 
 **body** | [**CreatingADraftCampaignRequest**](CreatingADraftCampaignRequest.md)|  | 

### Return type

[**CreatingADraftCampaignResponse**](CreatingADraftCampaignResponse.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="deletingacampaign"></a>
# **DeletingACampaign**
> DeletingACampaignResponse DeletingACampaign (string format, string apikey, string campaignID)

Deleting A Campaign

Deletes a campaign from your account, draft or even sent.

### Example
```csharp
using System;
using System.Diagnostics;
using Moosend.Wrappers.CSharpWrapper.Api;
using Moosend.Wrappers.CSharpWrapper.Client;
using Moosend.Wrappers.CSharpWrapper.Model;

namespace Example
{
    public class DeletingACampaignExample
    {
        public void main()
        {
            var apiInstance = new CampaignsApi();
            var format = format_example;  // string | 
            var apikey = apikey_example;  // string | You may find your API Key or generate a new one in your account settings.
            var campaignID = campaignID_example;  // string | The ID of the draft campaign to update.

            try
            {
                // Deleting A Campaign
                DeletingACampaignResponse result = apiInstance.DeletingACampaign(format, apikey, campaignID);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling CampaignsApi.DeletingACampaign: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **format** | **string**|  | 
 **apikey** | **string**| You may find your API Key or generate a new one in your account settings. | 
 **campaignID** | **string**| The ID of the draft campaign to update. | 

### Return type

[**DeletingACampaignResponse**](DeletingACampaignResponse.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="getallcampaigns"></a>
# **GetAllCampaigns**
> GetAllCampaignsResponse GetAllCampaigns (string format, string apikey)

Get All Campaigns

Returns a list of all campaigns in your account with detailed information. Because the results for this call could be quite big, paging information is required as input.

### Example
```csharp
using System;
using System.Diagnostics;
using Moosend.Wrappers.CSharpWrapper.Api;
using Moosend.Wrappers.CSharpWrapper.Client;
using Moosend.Wrappers.CSharpWrapper.Model;

namespace Example
{
    public class GetAllCampaignsExample
    {
        public void main()
        {
            var apiInstance = new CampaignsApi();
            var format = format_example;  // string | 
            var apikey = apikey_example;  // string | You may find your API Key or generate a new one in your account settings.

            try
            {
                // Get All Campaigns
                GetAllCampaignsResponse result = apiInstance.GetAllCampaigns(format, apikey);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling CampaignsApi.GetAllCampaigns: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **format** | **string**|  | 
 **apikey** | **string**| You may find your API Key or generate a new one in your account settings. | 

### Return type

[**GetAllCampaignsResponse**](GetAllCampaignsResponse.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="getcampaignstatisticswithpagingfiltered"></a>
# **GetCampaignStatisticsWithPagingFiltered**
> GetCampaignStatisticsWithPagingFilteredResponse GetCampaignStatisticsWithPagingFiltered (string format, string apikey, string campaignID, string type, string page = null, string pageSize = null, string from = null, string to = null)

Get Campaign Statistics With Paging & Filtered

Returns a detailed list of statistics for a given campaign based on activity such as emails sent, opened, bounced, link clicked, etc.  Because the results for this call could be quite big, paging information is required as input.

### Example
```csharp
using System;
using System.Diagnostics;
using Moosend.Wrappers.CSharpWrapper.Api;
using Moosend.Wrappers.CSharpWrapper.Client;
using Moosend.Wrappers.CSharpWrapper.Model;

namespace Example
{
    public class GetCampaignStatisticsWithPagingFilteredExample
    {
        public void main()
        {
            var apiInstance = new CampaignsApi();
            var format = format_example;  // string | 
            var apikey = apikey_example;  // string | You may find your API Key or generate a new one in your account settings.
            var campaignID = campaignID_example;  // string | The ID of the requested AB test campaign
            var type = type_example;  // string | The type of the activity to display results for. This must be one of the following values : * Sent : To get information about when and to which recipients the campaign was sent. * Opened : To get information about who opened the campaign. * LinkClicked : To get information about who clicked on which link. * Forward : To get information about who forwarded the campaign using the relevant link on the email body and when. * Unsubscribed : To get information about who unsubscribed from the campaign by clicking on the unsubscribe link and when. * Bounced : To get information about which email recipients failed to receive the campaign. If not specified, the value Sent will be used by default.
            var page = page_example;  // string | The page number to display results for. If not specified, the first page will be returned. (optional) 
            var pageSize = pageSize_example;  // string | The maximum number of results per page. This must be a positive integer up to 100. If not specified, 50 results per page will be returned.  If a value greater than 100 is specified, it will be treated as 100. (optional) 
            var from = from_example;  // string | A date value that specifies since when to start returning results. If omitted, results will be returned from the moment the campaign was sent. (optional) 
            var to = to_example;  // string | A date value that specifies up to when to return results. If omitted, results will be returned up to date. (optional) 

            try
            {
                // Get Campaign Statistics With Paging & Filtered
                GetCampaignStatisticsWithPagingFilteredResponse result = apiInstance.GetCampaignStatisticsWithPagingFiltered(format, apikey, campaignID, type, page, pageSize, from, to);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling CampaignsApi.GetCampaignStatisticsWithPagingFiltered: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **format** | **string**|  | 
 **apikey** | **string**| You may find your API Key or generate a new one in your account settings. | 
 **campaignID** | **string**| The ID of the requested AB test campaign | 
 **type** | **string**| The type of the activity to display results for. This must be one of the following values : * Sent : To get information about when and to which recipients the campaign was sent. * Opened : To get information about who opened the campaign. * LinkClicked : To get information about who clicked on which link. * Forward : To get information about who forwarded the campaign using the relevant link on the email body and when. * Unsubscribed : To get information about who unsubscribed from the campaign by clicking on the unsubscribe link and when. * Bounced : To get information about which email recipients failed to receive the campaign. If not specified, the value Sent will be used by default. | 
 **page** | **string**| The page number to display results for. If not specified, the first page will be returned. | [optional] 
 **pageSize** | **string**| The maximum number of results per page. This must be a positive integer up to 100. If not specified, 50 results per page will be returned.  If a value greater than 100 is specified, it will be treated as 100. | [optional] 
 **from** | **string**| A date value that specifies since when to start returning results. If omitted, results will be returned from the moment the campaign was sent. | [optional] 
 **to** | **string**| A date value that specifies up to when to return results. If omitted, results will be returned up to date. | [optional] 

### Return type

[**GetCampaignStatisticsWithPagingFilteredResponse**](GetCampaignStatisticsWithPagingFilteredResponse.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="getcampaignsbypage"></a>
# **GetCampaignsByPage**
> GetCampaignsByPageResponse GetCampaignsByPage (string format, string apikey, double? page)

Get Campaigns By Page

Returns a list of all campaigns in your account with detailed information. Because the results for this call could be quite big, paging information is required as input.

### Example
```csharp
using System;
using System.Diagnostics;
using Moosend.Wrappers.CSharpWrapper.Api;
using Moosend.Wrappers.CSharpWrapper.Client;
using Moosend.Wrappers.CSharpWrapper.Model;

namespace Example
{
    public class GetCampaignsByPageExample
    {
        public void main()
        {
            var apiInstance = new CampaignsApi();
            var format = format_example;  // string | 
            var apikey = apikey_example;  // string | You may find your API Key or generate a new one in your account settings.
            var page = 1.2;  // double? | The page number to display results for.

            try
            {
                // Get Campaigns By Page
                GetCampaignsByPageResponse result = apiInstance.GetCampaignsByPage(format, apikey, page);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling CampaignsApi.GetCampaignsByPage: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **format** | **string**|  | 
 **apikey** | **string**| You may find your API Key or generate a new one in your account settings. | 
 **page** | **double?**| The page number to display results for. | 

### Return type

[**GetCampaignsByPageResponse**](GetCampaignsByPageResponse.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="getcampaignsbypageandpagesize"></a>
# **GetCampaignsByPageAndPagesize**
> GetCampaignsByPageAndPagesizeResponse GetCampaignsByPageAndPagesize (string format, string apikey, double? page, double? pageSize, string shortBy = null, string sortMethod = null)

Get Campaigns By Page And Pagesize

Returns a list of all campaigns in your account with detailed information. Because the results for this call could be quite big, paging information is required as input.

### Example
```csharp
using System;
using System.Diagnostics;
using Moosend.Wrappers.CSharpWrapper.Api;
using Moosend.Wrappers.CSharpWrapper.Client;
using Moosend.Wrappers.CSharpWrapper.Model;

namespace Example
{
    public class GetCampaignsByPageAndPagesizeExample
    {
        public void main()
        {
            var apiInstance = new CampaignsApi();
            var format = format_example;  // string | 
            var apikey = apikey_example;  // string | You may find your API Key or generate a new one in your account settings.
            var page = 1.2;  // double? | The page number to display results for.
            var pageSize = 1.2;  // double? | The maximum number of results per page.  This must be a positive integer up to 100. If not specified, 50 results per page will be returned.  If a value greater than 100 is specified, it will be treated as 100.
            var shortBy = shortBy_example;  // string | The name of the campaign property to sort results by. If not specified, results will be sorted by the CreatedOn property (optional) 
            var sortMethod = sortMethod_example;  // string | The method to sort results: ASC for ascending, DESC for descending. If not specified, `ASC` will be assumed (optional) 

            try
            {
                // Get Campaigns By Page And Pagesize
                GetCampaignsByPageAndPagesizeResponse result = apiInstance.GetCampaignsByPageAndPagesize(format, apikey, page, pageSize, shortBy, sortMethod);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling CampaignsApi.GetCampaignsByPageAndPagesize: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **format** | **string**|  | 
 **apikey** | **string**| You may find your API Key or generate a new one in your account settings. | 
 **page** | **double?**| The page number to display results for. | 
 **pageSize** | **double?**| The maximum number of results per page.  This must be a positive integer up to 100. If not specified, 50 results per page will be returned.  If a value greater than 100 is specified, it will be treated as 100. | 
 **shortBy** | **string**| The name of the campaign property to sort results by. If not specified, results will be sorted by the CreatedOn property | [optional] 
 **sortMethod** | **string**| The method to sort results: ASC for ascending, DESC for descending. If not specified, &#x60;ASC&#x60; will be assumed | [optional] 

### Return type

[**GetCampaignsByPageAndPagesizeResponse**](GetCampaignsByPageAndPagesizeResponse.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="gettingallyoursenders"></a>
# **GettingAllYourSenders**
> GettingAllYourSendersResponse GettingAllYourSenders (string format, string apikey)

Getting All Your Senders

Gets a list of your active senders in your account. You may specify any email address of these senders when sending a campaign.

### Example
```csharp
using System;
using System.Diagnostics;
using Moosend.Wrappers.CSharpWrapper.Api;
using Moosend.Wrappers.CSharpWrapper.Client;
using Moosend.Wrappers.CSharpWrapper.Model;

namespace Example
{
    public class GettingAllYourSendersExample
    {
        public void main()
        {
            var apiInstance = new CampaignsApi();
            var format = format_example;  // string | 
            var apikey = apikey_example;  // string | You may find your API Key or generate a new one in your account settings.

            try
            {
                // Getting All Your Senders
                GettingAllYourSendersResponse result = apiInstance.GettingAllYourSenders(format, apikey);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling CampaignsApi.GettingAllYourSenders: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **format** | **string**|  | 
 **apikey** | **string**| You may find your API Key or generate a new one in your account settings. | 

### Return type

[**GettingAllYourSendersResponse**](GettingAllYourSendersResponse.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="gettingcampaigndetails"></a>
# **GettingCampaignDetails**
> GettingCampaignDetailsResponse GettingCampaignDetails (string format, string apikey, string campaignID)

Getting Campaign Details

Returns a complete set of properties that describe the requested campaign in detail. No statistics are included in the result.

### Example
```csharp
using System;
using System.Diagnostics;
using Moosend.Wrappers.CSharpWrapper.Api;
using Moosend.Wrappers.CSharpWrapper.Client;
using Moosend.Wrappers.CSharpWrapper.Model;

namespace Example
{
    public class GettingCampaignDetailsExample
    {
        public void main()
        {
            var apiInstance = new CampaignsApi();
            var format = format_example;  // string | 
            var apikey = apikey_example;  // string | You may find your API Key or generate a new one in your account settings.
            var campaignID = campaignID_example;  // string | The ID of the requested campaign

            try
            {
                // Getting Campaign Details
                GettingCampaignDetailsResponse result = apiInstance.GettingCampaignDetails(format, apikey, campaignID);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling CampaignsApi.GettingCampaignDetails: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **format** | **string**|  | 
 **apikey** | **string**| You may find your API Key or generate a new one in your account settings. | 
 **campaignID** | **string**| The ID of the requested campaign | 

### Return type

[**GettingCampaignDetailsResponse**](GettingCampaignDetailsResponse.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="gettingsenderdetails"></a>
# **GettingSenderDetails**
> GettingSenderDetailsResponse GettingSenderDetails (string format, string email, string apikey)

Getting Sender Details

Returns basic information for the specified sender identified by its email address.

### Example
```csharp
using System;
using System.Diagnostics;
using Moosend.Wrappers.CSharpWrapper.Api;
using Moosend.Wrappers.CSharpWrapper.Client;
using Moosend.Wrappers.CSharpWrapper.Model;

namespace Example
{
    public class GettingSenderDetailsExample
    {
        public void main()
        {
            var apiInstance = new CampaignsApi();
            var format = format_example;  // string | 
            var email = email_example;  // string | 
            var apikey = apikey_example;  // string | You may find your API Key or generate a new one in your account settings.

            try
            {
                // Getting Sender Details
                GettingSenderDetailsResponse result = apiInstance.GettingSenderDetails(format, email, apikey);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling CampaignsApi.GettingSenderDetails: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **format** | **string**|  | 
 **email** | **string**|  | 
 **apikey** | **string**| You may find your API Key or generate a new one in your account settings. | 

### Return type

[**GettingSenderDetailsResponse**](GettingSenderDetailsResponse.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="linkactivity"></a>
# **LinkActivity**
> LinkActivityResponse LinkActivity (string format, string apikey, string campaignID)

Link Activity

Returns a list with your campaign links and how many clicks have been made by your recipients, either unique or total.

### Example
```csharp
using System;
using System.Diagnostics;
using Moosend.Wrappers.CSharpWrapper.Api;
using Moosend.Wrappers.CSharpWrapper.Client;
using Moosend.Wrappers.CSharpWrapper.Model;

namespace Example
{
    public class LinkActivityExample
    {
        public void main()
        {
            var apiInstance = new CampaignsApi();
            var format = format_example;  // string | 
            var apikey = apikey_example;  // string | You may find your API Key or generate a new one in your account settings.
            var campaignID = campaignID_example;  // string | The ID of the requested campaign

            try
            {
                // Link Activity
                LinkActivityResponse result = apiInstance.LinkActivity(format, apikey, campaignID);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling CampaignsApi.LinkActivity: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **format** | **string**|  | 
 **apikey** | **string**| You may find your API Key or generate a new one in your account settings. | 
 **campaignID** | **string**| The ID of the requested campaign | 

### Return type

[**LinkActivityResponse**](LinkActivityResponse.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="schedulingacampaign"></a>
# **SchedulingACampaign**
> SchedulingACampaignResponse SchedulingACampaign (string format, string apikey, string campaignID, SchedulingACampaignRequest body)

Scheduling A Campaign

Assigns a scheduled date and time at which the campaign will be delivered.

### Example
```csharp
using System;
using System.Diagnostics;
using Moosend.Wrappers.CSharpWrapper.Api;
using Moosend.Wrappers.CSharpWrapper.Client;
using Moosend.Wrappers.CSharpWrapper.Model;

namespace Example
{
    public class SchedulingACampaignExample
    {
        public void main()
        {
            var apiInstance = new CampaignsApi();
            var format = format_example;  // string | 
            var apikey = apikey_example;  // string | You may find your API Key or generate a new one in your account settings.
            var campaignID = campaignID_example;  // string | The ID of the campaign to be scheduled
            var body = new SchedulingACampaignRequest(); // SchedulingACampaignRequest | 

            try
            {
                // Scheduling A Campaign
                SchedulingACampaignResponse result = apiInstance.SchedulingACampaign(format, apikey, campaignID, body);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling CampaignsApi.SchedulingACampaign: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **format** | **string**|  | 
 **apikey** | **string**| You may find your API Key or generate a new one in your account settings. | 
 **campaignID** | **string**| The ID of the campaign to be scheduled | 
 **body** | [**SchedulingACampaignRequest**](SchedulingACampaignRequest.md)|  | 

### Return type

[**SchedulingACampaignResponse**](SchedulingACampaignResponse.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="sendingacampaign"></a>
# **SendingACampaign**
> SendingACampaignResponse SendingACampaign (string format, string apikey, string campaignID)

Sending a campaign

Sends an existing draft campaign to all recipients specified in its mailing list. The campaign is sent immediatelly.

### Example
```csharp
using System;
using System.Diagnostics;
using Moosend.Wrappers.CSharpWrapper.Api;
using Moosend.Wrappers.CSharpWrapper.Client;
using Moosend.Wrappers.CSharpWrapper.Model;

namespace Example
{
    public class SendingACampaignExample
    {
        public void main()
        {
            var apiInstance = new CampaignsApi();
            var format = format_example;  // string | 
            var apikey = apikey_example;  // string | You may find your API Key or generate a new one in your account settings.
            var campaignID = campaignID_example;  // string | The ID of the draft campaign to be sent.

            try
            {
                // Sending a campaign
                SendingACampaignResponse result = apiInstance.SendingACampaign(format, apikey, campaignID);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling CampaignsApi.SendingACampaign: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **format** | **string**|  | 
 **apikey** | **string**| You may find your API Key or generate a new one in your account settings. | 
 **campaignID** | **string**| The ID of the draft campaign to be sent. | 

### Return type

[**SendingACampaignResponse**](SendingACampaignResponse.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="testingacampaign"></a>
# **TestingACampaign**
> TestingACampaignResponse TestingACampaign (string format, string apikey, string campaignID, TestingACampaignRequest body)

Testing a campaign

Sends a test email of a draft campaign to a list of email addresses you specify for previewing.

### Example
```csharp
using System;
using System.Diagnostics;
using Moosend.Wrappers.CSharpWrapper.Api;
using Moosend.Wrappers.CSharpWrapper.Client;
using Moosend.Wrappers.CSharpWrapper.Model;

namespace Example
{
    public class TestingACampaignExample
    {
        public void main()
        {
            var apiInstance = new CampaignsApi();
            var format = format_example;  // string | 
            var apikey = apikey_example;  // string | You may find your API Key or generate a new one in your account settings.
            var campaignID = campaignID_example;  // string | The ID of the draft campaign to be tested.
            var body = new TestingACampaignRequest(); // TestingACampaignRequest | 

            try
            {
                // Testing a campaign
                TestingACampaignResponse result = apiInstance.TestingACampaign(format, apikey, campaignID, body);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling CampaignsApi.TestingACampaign: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **format** | **string**|  | 
 **apikey** | **string**| You may find your API Key or generate a new one in your account settings. | 
 **campaignID** | **string**| The ID of the draft campaign to be tested. | 
 **body** | [**TestingACampaignRequest**](TestingACampaignRequest.md)|  | 

### Return type

[**TestingACampaignResponse**](TestingACampaignResponse.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="unschedulingacampaign"></a>
# **UnschedulingACampaign**
> UnschedulingACampaignResponse UnschedulingACampaign (string format, string apikey, string campaignID)

Unscheduling a campaign

Removes a previously defined scheduled date and time from a campaign, so that it will be delivered immediately if already queued or when sent.

### Example
```csharp
using System;
using System.Diagnostics;
using Moosend.Wrappers.CSharpWrapper.Api;
using Moosend.Wrappers.CSharpWrapper.Client;
using Moosend.Wrappers.CSharpWrapper.Model;

namespace Example
{
    public class UnschedulingACampaignExample
    {
        public void main()
        {
            var apiInstance = new CampaignsApi();
            var format = format_example;  // string | 
            var apikey = apikey_example;  // string | You may find your API Key or generate a new one in your account settings.
            var campaignID = campaignID_example;  // string | The ID of the campaign to be scheduled

            try
            {
                // Unscheduling a campaign
                UnschedulingACampaignResponse result = apiInstance.UnschedulingACampaign(format, apikey, campaignID);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling CampaignsApi.UnschedulingACampaign: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **format** | **string**|  | 
 **apikey** | **string**| You may find your API Key or generate a new one in your account settings. | 
 **campaignID** | **string**| The ID of the campaign to be scheduled | 

### Return type

[**UnschedulingACampaignResponse**](UnschedulingACampaignResponse.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="updatingadraftcampaign"></a>
# **UpdatingADraftCampaign**
> UpdatingADraftCampaignResponse UpdatingADraftCampaign (string format, string apikey, string campaignID, UpdatingADraftCampaignRequest body)

Updating A Draft Campaign

Updates properties of an existing draft A/B campaign in your account. Non-draft campaigns cannot be updated. Ignore ***(A/B Split Campaign Option)*** if you want to create a regular campaign.

### Example
```csharp
using System;
using System.Diagnostics;
using Moosend.Wrappers.CSharpWrapper.Api;
using Moosend.Wrappers.CSharpWrapper.Client;
using Moosend.Wrappers.CSharpWrapper.Model;

namespace Example
{
    public class UpdatingADraftCampaignExample
    {
        public void main()
        {
            var apiInstance = new CampaignsApi();
            var format = format_example;  // string | 
            var apikey = apikey_example;  // string | You may find your API Key or generate a new one in your account settings.
            var campaignID = campaignID_example;  // string | The ID of the draft campaign to update.
            var body = new UpdatingADraftCampaignRequest(); // UpdatingADraftCampaignRequest | 

            try
            {
                // Updating A Draft Campaign
                UpdatingADraftCampaignResponse result = apiInstance.UpdatingADraftCampaign(format, apikey, campaignID, body);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling CampaignsApi.UpdatingADraftCampaign: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **format** | **string**|  | 
 **apikey** | **string**| You may find your API Key or generate a new one in your account settings. | 
 **campaignID** | **string**| The ID of the draft campaign to update. | 
 **body** | [**UpdatingADraftCampaignRequest**](UpdatingADraftCampaignRequest.md)|  | 

### Return type

[**UpdatingADraftCampaignResponse**](UpdatingADraftCampaignResponse.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

