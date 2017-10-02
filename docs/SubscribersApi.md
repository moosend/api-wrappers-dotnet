# Moosend.Wrappers.CSharpWrapper.Api.SubscribersApi

All URIs are relative to *https://api.moosend.com/v3*

Method | HTTP request | Description
------------- | ------------- | -------------
[**AddingMultipleSubscribers**](SubscribersApi.md#addingmultiplesubscribers) | **POST** /subscribers/{MailingListID}/subscribe_many.{Format} | Adding multiple subscribers
[**AddingSubscribers**](SubscribersApi.md#addingsubscribers) | **POST** /subscribers/{MailingListID}/subscribe.{Format} | Adding subscribers
[**GetSubscriberByEmailAddress**](SubscribersApi.md#getsubscriberbyemailaddress) | **GET** /subscribers/{MailingListID}/view.{Format} | Get subscriber by email address
[**GetSubscriberById**](SubscribersApi.md#getsubscriberbyid) | **GET** /subscribers/{MailingListID}/find/{SubscriberID}.{Format} | Get subscriber by id
[**GettingSubscribers**](SubscribersApi.md#gettingsubscribers) | **GET** /lists/{MailingListID}/subscribers/{Status}.{Format} | Getting subscribers
[**RemovingASubscriber**](SubscribersApi.md#removingasubscriber) | **POST** /subscribers/{MailingListID}/remove.{Format} | Removing a subscriber
[**RemovingMultipleSubscribers**](SubscribersApi.md#removingmultiplesubscribers) | **POST** /subscribers/{MailingListID}/remove_many.{Format} | Removing multiple subscribers
[**UnsubscribingSubscribersFromAccount**](SubscribersApi.md#unsubscribingsubscribersfromaccount) | **POST** /subscribers/unsubscribe.{Format} | Unsubscribing subscribers from account
[**UnsubscribingSubscribersFromMailingList**](SubscribersApi.md#unsubscribingsubscribersfrommailinglist) | **POST** /subscribers/{MailingListID}/unsubscribe.{Format} | Unsubscribing subscribers from mailing list
[**UnsubscribingSubscribersFromMailingListAndASpecifiedCampaign**](SubscribersApi.md#unsubscribingsubscribersfrommailinglistandaspecifiedcampaign) | **POST** /subscribers/{MailingListID}/{CampaignID}/unsubscribe.{Format} | Unsubscribing subscribers from mailing list and a specified campaign
[**UpdatingASubscriber**](SubscribersApi.md#updatingasubscriber) | **POST** /subscribers/{MailingListID}/update/{SubscriberID}.{Format} | Updating a subscriber


<a name="addingmultiplesubscribers"></a>
# **AddingMultipleSubscribers**
> AddingMultipleSubscribersResponse AddingMultipleSubscribers (string format, string apikey, string mailingListID, AddingMultipleSubscribersRequest body)

Adding multiple subscribers

This method allows you to add multiple subscribers in a mailing list with a single call. If some subscribers already exist with the given email addresses, they will be updated. If you try to add a subscriber with an invalid email address, this attempt will be ignored, as the process will skip to the next subscriber automatically.

### Example
```csharp
using System;
using System.Diagnostics;
using Moosend.Wrappers.CSharpWrapper.Api;
using Moosend.Wrappers.CSharpWrapper.Client;
using Moosend.Wrappers.CSharpWrapper.Model;

namespace Example
{
    public class AddingMultipleSubscribersExample
    {
        public void main()
        {
            var apiInstance = new SubscribersApi();
            var format = format_example;  // string | 
            var apikey = apikey_example;  // string | You may find your API Key or generate a new one in your account settings.
            var mailingListID = mailingListID_example;  // string | The ID of the mailing list to add subscribers to.
            var body = new AddingMultipleSubscribersRequest(); // AddingMultipleSubscribersRequest | 

            try
            {
                // Adding multiple subscribers
                AddingMultipleSubscribersResponse result = apiInstance.AddingMultipleSubscribers(format, apikey, mailingListID, body);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling SubscribersApi.AddingMultipleSubscribers: " + e.Message );
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
 **mailingListID** | **string**| The ID of the mailing list to add subscribers to. | 
 **body** | [**AddingMultipleSubscribersRequest**](AddingMultipleSubscribersRequest.md)|  | 

### Return type

[**AddingMultipleSubscribersResponse**](AddingMultipleSubscribersResponse.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="addingsubscribers"></a>
# **AddingSubscribers**
> AddingSubscribersResponse AddingSubscribers (string format, string mailingListID, string apikey, AddingSubscribersRequest body)

Adding subscribers

Adds a new subscriber to the specified mailing list. If there is already a subscriber with the specified email address in the list, an update will be performed instead.

### Example
```csharp
using System;
using System.Diagnostics;
using Moosend.Wrappers.CSharpWrapper.Api;
using Moosend.Wrappers.CSharpWrapper.Client;
using Moosend.Wrappers.CSharpWrapper.Model;

namespace Example
{
    public class AddingSubscribersExample
    {
        public void main()
        {
            var apiInstance = new SubscribersApi();
            var format = format_example;  // string | 
            var mailingListID = mailingListID_example;  // string | The ID of the mailing list to add the new member.
            var apikey = apikey_example;  // string | You may find your API Key or generate a new one in your account settings.
            var body = new AddingSubscribersRequest(); // AddingSubscribersRequest | 

            try
            {
                // Adding subscribers
                AddingSubscribersResponse result = apiInstance.AddingSubscribers(format, mailingListID, apikey, body);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling SubscribersApi.AddingSubscribers: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **format** | **string**|  | 
 **mailingListID** | **string**| The ID of the mailing list to add the new member. | 
 **apikey** | **string**| You may find your API Key or generate a new one in your account settings. | 
 **body** | [**AddingSubscribersRequest**](AddingSubscribersRequest.md)|  | 

### Return type

[**AddingSubscribersResponse**](AddingSubscribersResponse.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="getsubscriberbyemailaddress"></a>
# **GetSubscriberByEmailAddress**
> GetSubscriberByEmailAddressResponse GetSubscriberByEmailAddress (string format, string apikey, string mailingListID, string email)

Get subscriber by email address

Searches for a subscriber with the specified email address in the specified mailing list and returns detailed information such as id, name, date created, date unsubscribed, status and custom fields

### Example
```csharp
using System;
using System.Diagnostics;
using Moosend.Wrappers.CSharpWrapper.Api;
using Moosend.Wrappers.CSharpWrapper.Client;
using Moosend.Wrappers.CSharpWrapper.Model;

namespace Example
{
    public class GetSubscriberByEmailAddressExample
    {
        public void main()
        {
            var apiInstance = new SubscribersApi();
            var format = format_example;  // string | 
            var apikey = apikey_example;  // string | You may find your API Key or generate a new one in your account settings.
            var mailingListID = mailingListID_example;  // string | The ID of the mailing list where the subscriber belongs.
            var email = email_example;  // string | The email of the subscriber.

            try
            {
                // Get subscriber by email address
                GetSubscriberByEmailAddressResponse result = apiInstance.GetSubscriberByEmailAddress(format, apikey, mailingListID, email);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling SubscribersApi.GetSubscriberByEmailAddress: " + e.Message );
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
 **mailingListID** | **string**| The ID of the mailing list where the subscriber belongs. | 
 **email** | **string**| The email of the subscriber. | 

### Return type

[**GetSubscriberByEmailAddressResponse**](GetSubscriberByEmailAddressResponse.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="getsubscriberbyid"></a>
# **GetSubscriberById**
> GetSubscriberByIdResponse GetSubscriberById (string format, string apikey, string mailingListID, string subscriberID)

Get subscriber by id

Searches for a subscriber with the specified unique id in the specified mailing list and returns detailed information such as email, name, date created, date unsubscribed, status and custom fields.

### Example
```csharp
using System;
using System.Diagnostics;
using Moosend.Wrappers.CSharpWrapper.Api;
using Moosend.Wrappers.CSharpWrapper.Client;
using Moosend.Wrappers.CSharpWrapper.Model;

namespace Example
{
    public class GetSubscriberByIdExample
    {
        public void main()
        {
            var apiInstance = new SubscribersApi();
            var format = format_example;  // string | 
            var apikey = apikey_example;  // string | You may find your API Key or generate a new one in your account settings.
            var mailingListID = mailingListID_example;  // string | The ID of the mailing list to search the subscriber in.
            var subscriberID = subscriberID_example;  // string | The id of the subscriber being searched.

            try
            {
                // Get subscriber by id
                GetSubscriberByIdResponse result = apiInstance.GetSubscriberById(format, apikey, mailingListID, subscriberID);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling SubscribersApi.GetSubscriberById: " + e.Message );
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
 **mailingListID** | **string**| The ID of the mailing list to search the subscriber in. | 
 **subscriberID** | **string**| The id of the subscriber being searched. | 

### Return type

[**GetSubscriberByIdResponse**](GetSubscriberByIdResponse.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="gettingsubscribers"></a>
# **GettingSubscribers**
> GettingSubscribersResponse GettingSubscribers (string format, string mailingListID, string apikey, string status, double? page = null, double? pageSize = null)

Getting subscribers

Gets a list of all subscribers in a given mailing list. You may filter the list by setting a date to fetch those subscribed since then and/or by their status. Because the results for this call could be quite big, paging information is required as input.

### Example
```csharp
using System;
using System.Diagnostics;
using Moosend.Wrappers.CSharpWrapper.Api;
using Moosend.Wrappers.CSharpWrapper.Client;
using Moosend.Wrappers.CSharpWrapper.Model;

namespace Example
{
    public class GettingSubscribersExample
    {
        public void main()
        {
            var apiInstance = new SubscribersApi();
            var format = format_example;  // string | 
            var mailingListID = mailingListID_example;  // string | The ID of the mailing list where the subscribers belong.
            var apikey = apikey_example;  // string | You may find your API Key or generate a new one in your account settings.
            var status = status_example;  // string | Specifies what type of subscriber statistics results will be returned.
            var page = 1.2;  // double? | Specifies the page of subscriber statistics results will be returned. (optional) 
            var pageSize = 1.2;  // double? | Specifies the page size of subscriber statistics results will be returned. (optional) 

            try
            {
                // Getting subscribers
                GettingSubscribersResponse result = apiInstance.GettingSubscribers(format, mailingListID, apikey, status, page, pageSize);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling SubscribersApi.GettingSubscribers: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **format** | **string**|  | 
 **mailingListID** | **string**| The ID of the mailing list where the subscribers belong. | 
 **apikey** | **string**| You may find your API Key or generate a new one in your account settings. | 
 **status** | **string**| Specifies what type of subscriber statistics results will be returned. | 
 **page** | **double?**| Specifies the page of subscriber statistics results will be returned. | [optional] 
 **pageSize** | **double?**| Specifies the page size of subscriber statistics results will be returned. | [optional] 

### Return type

[**GettingSubscribersResponse**](GettingSubscribersResponse.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="removingasubscriber"></a>
# **RemovingASubscriber**
> RemovingASubscriberResponse RemovingASubscriber (string format, string apikey, string mailingListID, RemovingASubscriberRequest body)

Removing a subscriber

Removes a subscriber from the specified mailing list permanently (without moving to the suppression list).

### Example
```csharp
using System;
using System.Diagnostics;
using Moosend.Wrappers.CSharpWrapper.Api;
using Moosend.Wrappers.CSharpWrapper.Client;
using Moosend.Wrappers.CSharpWrapper.Model;

namespace Example
{
    public class RemovingASubscriberExample
    {
        public void main()
        {
            var apiInstance = new SubscribersApi();
            var format = format_example;  // string | 
            var apikey = apikey_example;  // string | You may find your API Key or generate a new one in your account settings.
            var mailingListID = mailingListID_example;  // string | The ID of the mailing list to remove the subscriber from.
            var body = new RemovingASubscriberRequest(); // RemovingASubscriberRequest | 

            try
            {
                // Removing a subscriber
                RemovingASubscriberResponse result = apiInstance.RemovingASubscriber(format, apikey, mailingListID, body);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling SubscribersApi.RemovingASubscriber: " + e.Message );
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
 **mailingListID** | **string**| The ID of the mailing list to remove the subscriber from. | 
 **body** | [**RemovingASubscriberRequest**](RemovingASubscriberRequest.md)|  | 

### Return type

[**RemovingASubscriberResponse**](RemovingASubscriberResponse.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="removingmultiplesubscribers"></a>
# **RemovingMultipleSubscribers**
> RemovingMultipleSubscribersResponse RemovingMultipleSubscribers (string format, string apikey, string mailingListID, RemovingMultipleSubscribersRequest body)

Removing multiple subscribers

Removes a list of subscribers from the specified mailing list permanently (without putting them in the suppression list). Any invalid email addresses specified will be ignored.

### Example
```csharp
using System;
using System.Diagnostics;
using Moosend.Wrappers.CSharpWrapper.Api;
using Moosend.Wrappers.CSharpWrapper.Client;
using Moosend.Wrappers.CSharpWrapper.Model;

namespace Example
{
    public class RemovingMultipleSubscribersExample
    {
        public void main()
        {
            var apiInstance = new SubscribersApi();
            var format = format_example;  // string | 
            var apikey = apikey_example;  // string | You may find your API Key or generate a new one in your account settings.
            var mailingListID = mailingListID_example;  // string | The ID of the mailing list to remove the subscribers from.
            var body = new RemovingMultipleSubscribersRequest(); // RemovingMultipleSubscribersRequest | 

            try
            {
                // Removing multiple subscribers
                RemovingMultipleSubscribersResponse result = apiInstance.RemovingMultipleSubscribers(format, apikey, mailingListID, body);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling SubscribersApi.RemovingMultipleSubscribers: " + e.Message );
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
 **mailingListID** | **string**| The ID of the mailing list to remove the subscribers from. | 
 **body** | [**RemovingMultipleSubscribersRequest**](RemovingMultipleSubscribersRequest.md)|  | 

### Return type

[**RemovingMultipleSubscribersResponse**](RemovingMultipleSubscribersResponse.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="unsubscribingsubscribersfromaccount"></a>
# **UnsubscribingSubscribersFromAccount**
> UnsubscribingSubscribersFromAccountResponse UnsubscribingSubscribersFromAccount (string format, string apikey, UnsubscribingSubscribersFromAccountRequest body)

Unsubscribing subscribers from account

Unsubscribes a subscriber from the account.

### Example
```csharp
using System;
using System.Diagnostics;
using Moosend.Wrappers.CSharpWrapper.Api;
using Moosend.Wrappers.CSharpWrapper.Client;
using Moosend.Wrappers.CSharpWrapper.Model;

namespace Example
{
    public class UnsubscribingSubscribersFromAccountExample
    {
        public void main()
        {
            var apiInstance = new SubscribersApi();
            var format = format_example;  // string | 
            var apikey = apikey_example;  // string | You may find your API Key or generate a new one in your account settings.
            var body = new UnsubscribingSubscribersFromAccountRequest(); // UnsubscribingSubscribersFromAccountRequest | 

            try
            {
                // Unsubscribing subscribers from account
                UnsubscribingSubscribersFromAccountResponse result = apiInstance.UnsubscribingSubscribersFromAccount(format, apikey, body);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling SubscribersApi.UnsubscribingSubscribersFromAccount: " + e.Message );
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
 **body** | [**UnsubscribingSubscribersFromAccountRequest**](UnsubscribingSubscribersFromAccountRequest.md)|  | 

### Return type

[**UnsubscribingSubscribersFromAccountResponse**](UnsubscribingSubscribersFromAccountResponse.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="unsubscribingsubscribersfrommailinglist"></a>
# **UnsubscribingSubscribersFromMailingList**
> UnsubscribingSubscribersFromMailingListResponse UnsubscribingSubscribersFromMailingList (string format, string apikey, string mailingListID, UnsubscribingSubscribersFromMailingListRequest body)

Unsubscribing subscribers from mailing list

Unsubscribes a subscriber from the specified mailing list. The subscriber is not deleted, but moved to the suppression list.

### Example
```csharp
using System;
using System.Diagnostics;
using Moosend.Wrappers.CSharpWrapper.Api;
using Moosend.Wrappers.CSharpWrapper.Client;
using Moosend.Wrappers.CSharpWrapper.Model;

namespace Example
{
    public class UnsubscribingSubscribersFromMailingListExample
    {
        public void main()
        {
            var apiInstance = new SubscribersApi();
            var format = format_example;  // string | 
            var apikey = apikey_example;  // string | You may find your API Key or generate a new one in your account settings.
            var mailingListID = mailingListID_example;  // string | The ID of the mailing list to add subscribers to.
            var body = new UnsubscribingSubscribersFromMailingListRequest(); // UnsubscribingSubscribersFromMailingListRequest | 

            try
            {
                // Unsubscribing subscribers from mailing list
                UnsubscribingSubscribersFromMailingListResponse result = apiInstance.UnsubscribingSubscribersFromMailingList(format, apikey, mailingListID, body);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling SubscribersApi.UnsubscribingSubscribersFromMailingList: " + e.Message );
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
 **mailingListID** | **string**| The ID of the mailing list to add subscribers to. | 
 **body** | [**UnsubscribingSubscribersFromMailingListRequest**](UnsubscribingSubscribersFromMailingListRequest.md)|  | 

### Return type

[**UnsubscribingSubscribersFromMailingListResponse**](UnsubscribingSubscribersFromMailingListResponse.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="unsubscribingsubscribersfrommailinglistandaspecifiedcampaign"></a>
# **UnsubscribingSubscribersFromMailingListAndASpecifiedCampaign**
> UnsubscribingSubscribersFromMailingListAndASpecifiedCampaignResponse UnsubscribingSubscribersFromMailingListAndASpecifiedCampaign (string format, string campaignID, string apikey, string mailingListID, UnsubscribingSubscribersFromMailingListAndASpecifiedCampaignRequest body)

Unsubscribing subscribers from mailing list and a specified campaign

Unsubscribes a subscriber from the specified mailing list and the specified campaign. The subscriber is not deleted, but moved to the suppression list.  This call will take into account the setting you have in \"suppression list and unsubscribe settings\" and will remove the subscriber from all other mailing lists or not accordingly.

### Example
```csharp
using System;
using System.Diagnostics;
using Moosend.Wrappers.CSharpWrapper.Api;
using Moosend.Wrappers.CSharpWrapper.Client;
using Moosend.Wrappers.CSharpWrapper.Model;

namespace Example
{
    public class UnsubscribingSubscribersFromMailingListAndASpecifiedCampaignExample
    {
        public void main()
        {
            var apiInstance = new SubscribersApi();
            var format = format_example;  // string | 
            var campaignID = campaignID_example;  // string | The ID of the campaign that was sent to the specific mailing list.
            var apikey = apikey_example;  // string | You may find your API Key or generate a new one in your account settings.
            var mailingListID = mailingListID_example;  // string | The ID of the mailing list to remove the subscriber from.
            var body = new UnsubscribingSubscribersFromMailingListAndASpecifiedCampaignRequest(); // UnsubscribingSubscribersFromMailingListAndASpecifiedCampaignRequest | 

            try
            {
                // Unsubscribing subscribers from mailing list and a specified campaign
                UnsubscribingSubscribersFromMailingListAndASpecifiedCampaignResponse result = apiInstance.UnsubscribingSubscribersFromMailingListAndASpecifiedCampaign(format, campaignID, apikey, mailingListID, body);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling SubscribersApi.UnsubscribingSubscribersFromMailingListAndASpecifiedCampaign: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **format** | **string**|  | 
 **campaignID** | **string**| The ID of the campaign that was sent to the specific mailing list. | 
 **apikey** | **string**| You may find your API Key or generate a new one in your account settings. | 
 **mailingListID** | **string**| The ID of the mailing list to remove the subscriber from. | 
 **body** | [**UnsubscribingSubscribersFromMailingListAndASpecifiedCampaignRequest**](UnsubscribingSubscribersFromMailingListAndASpecifiedCampaignRequest.md)|  | 

### Return type

[**UnsubscribingSubscribersFromMailingListAndASpecifiedCampaignResponse**](UnsubscribingSubscribersFromMailingListAndASpecifiedCampaignResponse.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="updatingasubscriber"></a>
# **UpdatingASubscriber**
> UpdatingASubscriberResponse UpdatingASubscriber (string format, string apikey, string mailingListID, string subscriberID, UpdatingASubscriberRequest body)

Updating a subscriber

Updates a subscriber in the specified mailing list. You can even update the subscribers email, if he has not unsubscribed.

### Example
```csharp
using System;
using System.Diagnostics;
using Moosend.Wrappers.CSharpWrapper.Api;
using Moosend.Wrappers.CSharpWrapper.Client;
using Moosend.Wrappers.CSharpWrapper.Model;

namespace Example
{
    public class UpdatingASubscriberExample
    {
        public void main()
        {
            var apiInstance = new SubscribersApi();
            var format = format_example;  // string | 
            var apikey = apikey_example;  // string | You may find your API Key or generate a new one in your account settings.
            var mailingListID = mailingListID_example;  // string | The ID of the mailing list that contains the subscriber
            var subscriberID = subscriberID_example;  // string | The id of the subscriber to be updated
            var body = new UpdatingASubscriberRequest(); // UpdatingASubscriberRequest | 

            try
            {
                // Updating a subscriber
                UpdatingASubscriberResponse result = apiInstance.UpdatingASubscriber(format, apikey, mailingListID, subscriberID, body);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling SubscribersApi.UpdatingASubscriber: " + e.Message );
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
 **mailingListID** | **string**| The ID of the mailing list that contains the subscriber | 
 **subscriberID** | **string**| The id of the subscriber to be updated | 
 **body** | [**UpdatingASubscriberRequest**](UpdatingASubscriberRequest.md)|  | 

### Return type

[**UpdatingASubscriberResponse**](UpdatingASubscriberResponse.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

