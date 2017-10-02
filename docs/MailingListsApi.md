# Moosend.Wrappers.CSharpWrapper.Api.MailingListsApi

All URIs are relative to *https://api.moosend.com/v3*

Method | HTTP request | Description
------------- | ------------- | -------------
[**CreatingACustomField**](MailingListsApi.md#creatingacustomfield) | **POST** /lists/{MailingListID}/customfields/create.{Format} | Creating a custom field
[**CreatingAMailingList**](MailingListsApi.md#creatingamailinglist) | **POST** /lists/create.{Format} | Creating a mailing list
[**DeletingAMailingList**](MailingListsApi.md#deletingamailinglist) | **DELETE** /lists/{MailingListID}/delete.{Format} | Deleting a mailing list
[**GettingAllActiveMailingLists**](MailingListsApi.md#gettingallactivemailinglists) | **GET** /lists.{Format} | Getting all active mailing lists
[**GettingAllActiveMailingListsWithPaging**](MailingListsApi.md#gettingallactivemailinglistswithpaging) | **GET** /lists/{Page}/{PageSize}.{Format} | Getting all active mailing lists with paging
[**GettingMailingListDetails**](MailingListsApi.md#gettingmailinglistdetails) | **GET** /lists/{MailingListID}/details.{Format} | Getting mailing list details
[**RemovingACustomField**](MailingListsApi.md#removingacustomfield) | **DELETE** /lists/{MailingListID}/customfields/{CustomFieldID}/delete.{Format} | Removing a custom field
[**UpdatingACustomField**](MailingListsApi.md#updatingacustomfield) | **POST** /lists/{MailingListID}/customfields/{CustomFieldID}/update.{Format} | Updating a custom field
[**UpdatingAMailingList**](MailingListsApi.md#updatingamailinglist) | **POST** /lists/{MailingListID}/update.{Format} | Updating a mailing list


<a name="creatingacustomfield"></a>
# **CreatingACustomField**
> CreatingACustomFieldResponse CreatingACustomField (string format, string apikey, string mailingListID, CreatingACustomFieldRequest body)

Creating a custom field

Creates a new custom field in the specified mailing list.

### Example
```csharp
using System;
using System.Diagnostics;
using Moosend.Wrappers.CSharpWrapper.Api;
using Moosend.Wrappers.CSharpWrapper.Client;
using Moosend.Wrappers.CSharpWrapper.Model;

namespace Example
{
    public class CreatingACustomFieldExample
    {
        public void main()
        {
            var apiInstance = new MailingListsApi();
            var format = format_example;  // string | 
            var apikey = apikey_example;  // string | You may find your API Key or generate a new one in your account settings.
            var mailingListID = mailingListID_example;  // string | The ID of the mailing list where the custom field will belong.
            var body = new CreatingACustomFieldRequest(); // CreatingACustomFieldRequest | 

            try
            {
                // Creating a custom field
                CreatingACustomFieldResponse result = apiInstance.CreatingACustomField(format, apikey, mailingListID, body);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling MailingListsApi.CreatingACustomField: " + e.Message );
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
 **mailingListID** | **string**| The ID of the mailing list where the custom field will belong. | 
 **body** | [**CreatingACustomFieldRequest**](CreatingACustomFieldRequest.md)|  | 

### Return type

[**CreatingACustomFieldResponse**](CreatingACustomFieldResponse.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="creatingamailinglist"></a>
# **CreatingAMailingList**
> CreatingAMailingListResponse CreatingAMailingList (string format, string apikey, CreatingAMailingListRequest body)

Creating a mailing list

Creates a new empty mailing list in your account.

### Example
```csharp
using System;
using System.Diagnostics;
using Moosend.Wrappers.CSharpWrapper.Api;
using Moosend.Wrappers.CSharpWrapper.Client;
using Moosend.Wrappers.CSharpWrapper.Model;

namespace Example
{
    public class CreatingAMailingListExample
    {
        public void main()
        {
            var apiInstance = new MailingListsApi();
            var format = format_example;  // string | 
            var apikey = apikey_example;  // string | You may find your API Key or generate a new one in your account settings.
            var body = new CreatingAMailingListRequest(); // CreatingAMailingListRequest | 

            try
            {
                // Creating a mailing list
                CreatingAMailingListResponse result = apiInstance.CreatingAMailingList(format, apikey, body);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling MailingListsApi.CreatingAMailingList: " + e.Message );
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
 **body** | [**CreatingAMailingListRequest**](CreatingAMailingListRequest.md)|  | 

### Return type

[**CreatingAMailingListResponse**](CreatingAMailingListResponse.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="deletingamailinglist"></a>
# **DeletingAMailingList**
> DeletingAMailingListResponse DeletingAMailingList (string format, string apikey, string mailingListID)

Deleting a mailing list

Deletes a mailing list from your account.

### Example
```csharp
using System;
using System.Diagnostics;
using Moosend.Wrappers.CSharpWrapper.Api;
using Moosend.Wrappers.CSharpWrapper.Client;
using Moosend.Wrappers.CSharpWrapper.Model;

namespace Example
{
    public class DeletingAMailingListExample
    {
        public void main()
        {
            var apiInstance = new MailingListsApi();
            var format = format_example;  // string | 
            var apikey = apikey_example;  // string | You may find your API Key or generate a new one in your account settings.
            var mailingListID = mailingListID_example;  // string | The ID of the mailing list to be deleted.

            try
            {
                // Deleting a mailing list
                DeletingAMailingListResponse result = apiInstance.DeletingAMailingList(format, apikey, mailingListID);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling MailingListsApi.DeletingAMailingList: " + e.Message );
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
 **mailingListID** | **string**| The ID of the mailing list to be deleted. | 

### Return type

[**DeletingAMailingListResponse**](DeletingAMailingListResponse.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="gettingallactivemailinglists"></a>
# **GettingAllActiveMailingLists**
> GettingAllActiveMailingListsResponse GettingAllActiveMailingLists (string format, string apikey, string withStatistics = null, string shortBy = null, string sortMethod = null)

Getting all active mailing lists

Gets a list of your active mailing lists in your account.

### Example
```csharp
using System;
using System.Diagnostics;
using Moosend.Wrappers.CSharpWrapper.Api;
using Moosend.Wrappers.CSharpWrapper.Client;
using Moosend.Wrappers.CSharpWrapper.Model;

namespace Example
{
    public class GettingAllActiveMailingListsExample
    {
        public void main()
        {
            var apiInstance = new MailingListsApi();
            var format = format_example;  // string | 
            var apikey = apikey_example;  // string | You may find your API Key or generate a new one in your account settings.
            var withStatistics = withStatistics_example;  // string | Specifies whether to fetch statistics for the subscribers or not. If omitted, results will be returned with statistics by default. (optional) 
            var shortBy = shortBy_example;  // string | The name of the campaign property to sort results by. If not specified, results will be sorted by the CreatedOn property (optional) 
            var sortMethod = sortMethod_example;  // string | The method to sort results: ASC for ascending, DESC for descending. If not specified, `ASC` will be assumed (optional) 

            try
            {
                // Getting all active mailing lists
                GettingAllActiveMailingListsResponse result = apiInstance.GettingAllActiveMailingLists(format, apikey, withStatistics, shortBy, sortMethod);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling MailingListsApi.GettingAllActiveMailingLists: " + e.Message );
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
 **withStatistics** | **string**| Specifies whether to fetch statistics for the subscribers or not. If omitted, results will be returned with statistics by default. | [optional] 
 **shortBy** | **string**| The name of the campaign property to sort results by. If not specified, results will be sorted by the CreatedOn property | [optional] 
 **sortMethod** | **string**| The method to sort results: ASC for ascending, DESC for descending. If not specified, &#x60;ASC&#x60; will be assumed | [optional] 

### Return type

[**GettingAllActiveMailingListsResponse**](GettingAllActiveMailingListsResponse.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="gettingallactivemailinglistswithpaging"></a>
# **GettingAllActiveMailingListsWithPaging**
> GettingAllActiveMailingListsWithPagingResponse GettingAllActiveMailingListsWithPaging (string format, string apikey, double? page, double? pageSize, string shortBy = null, string sortMethod = null)

Getting all active mailing lists with paging

Gets a list of your active mailing lists in your account. Because the results for this call could be quite big, paging information is required as input.

### Example
```csharp
using System;
using System.Diagnostics;
using Moosend.Wrappers.CSharpWrapper.Api;
using Moosend.Wrappers.CSharpWrapper.Client;
using Moosend.Wrappers.CSharpWrapper.Model;

namespace Example
{
    public class GettingAllActiveMailingListsWithPagingExample
    {
        public void main()
        {
            var apiInstance = new MailingListsApi();
            var format = format_example;  // string | 
            var apikey = apikey_example;  // string | You may find your API Key or generate a new one in your account settings.
            var page = 1.2;  // double? | The page that you want to get.
            var pageSize = 1.2;  // double? | Lists Per Page.
            var shortBy = shortBy_example;  // string | The name of the campaign property to sort results by. If not specified, results will be sorted by the CreatedOn property (optional) 
            var sortMethod = sortMethod_example;  // string | The method to sort results: ASC for ascending, DESC for descending. If not specified, `ASC` will be assumed (optional) 

            try
            {
                // Getting all active mailing lists with paging
                GettingAllActiveMailingListsWithPagingResponse result = apiInstance.GettingAllActiveMailingListsWithPaging(format, apikey, page, pageSize, shortBy, sortMethod);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling MailingListsApi.GettingAllActiveMailingListsWithPaging: " + e.Message );
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
 **page** | **double?**| The page that you want to get. | 
 **pageSize** | **double?**| Lists Per Page. | 
 **shortBy** | **string**| The name of the campaign property to sort results by. If not specified, results will be sorted by the CreatedOn property | [optional] 
 **sortMethod** | **string**| The method to sort results: ASC for ascending, DESC for descending. If not specified, &#x60;ASC&#x60; will be assumed | [optional] 

### Return type

[**GettingAllActiveMailingListsWithPagingResponse**](GettingAllActiveMailingListsWithPagingResponse.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="gettingmailinglistdetails"></a>
# **GettingMailingListDetails**
> GettingMailingListDetailsResponse GettingMailingListDetails (string format, string mailingListID, string apikey, string withStatistics = null)

Getting mailing list details

Gets details for a given mailing list. You may include subscriber statistics in your results or not. Any segments existing for the requested mailing list will not be included in the results.

### Example
```csharp
using System;
using System.Diagnostics;
using Moosend.Wrappers.CSharpWrapper.Api;
using Moosend.Wrappers.CSharpWrapper.Client;
using Moosend.Wrappers.CSharpWrapper.Model;

namespace Example
{
    public class GettingMailingListDetailsExample
    {
        public void main()
        {
            var apiInstance = new MailingListsApi();
            var format = format_example;  // string | 
            var mailingListID = mailingListID_example;  // string | The ID of the mailing list to be returned.
            var apikey = apikey_example;  // string | You may find your API Key or generate a new one in your account settings.
            var withStatistics = withStatistics_example;  // string | Specifies whether to fetch statistics for the subscribers or not. If omitted, results will be returned with statistics by default. (optional) 

            try
            {
                // Getting mailing list details
                GettingMailingListDetailsResponse result = apiInstance.GettingMailingListDetails(format, mailingListID, apikey, withStatistics);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling MailingListsApi.GettingMailingListDetails: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **format** | **string**|  | 
 **mailingListID** | **string**| The ID of the mailing list to be returned. | 
 **apikey** | **string**| You may find your API Key or generate a new one in your account settings. | 
 **withStatistics** | **string**| Specifies whether to fetch statistics for the subscribers or not. If omitted, results will be returned with statistics by default. | [optional] 

### Return type

[**GettingMailingListDetailsResponse**](GettingMailingListDetailsResponse.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="removingacustomfield"></a>
# **RemovingACustomField**
> RemovingACustomFieldResponse RemovingACustomField (string format, string customFieldID, string apikey, string mailingListID)

Removing a custom field

Removes a custom field definition from the specified mailing list.

### Example
```csharp
using System;
using System.Diagnostics;
using Moosend.Wrappers.CSharpWrapper.Api;
using Moosend.Wrappers.CSharpWrapper.Client;
using Moosend.Wrappers.CSharpWrapper.Model;

namespace Example
{
    public class RemovingACustomFieldExample
    {
        public void main()
        {
            var apiInstance = new MailingListsApi();
            var format = format_example;  // string | 
            var customFieldID = customFieldID_example;  // string | The ID of the custom field to be deleted.
            var apikey = apikey_example;  // string | You may find your API Key or generate a new one in your account settings.
            var mailingListID = mailingListID_example;  // string | The ID of the mailing list where the custom field belongs.

            try
            {
                // Removing a custom field
                RemovingACustomFieldResponse result = apiInstance.RemovingACustomField(format, customFieldID, apikey, mailingListID);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling MailingListsApi.RemovingACustomField: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **format** | **string**|  | 
 **customFieldID** | **string**| The ID of the custom field to be deleted. | 
 **apikey** | **string**| You may find your API Key or generate a new one in your account settings. | 
 **mailingListID** | **string**| The ID of the mailing list where the custom field belongs. | 

### Return type

[**RemovingACustomFieldResponse**](RemovingACustomFieldResponse.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="updatingacustomfield"></a>
# **UpdatingACustomField**
> UpdatingACustomFieldResponse UpdatingACustomField (string format, string customFieldID, string apikey, string mailingListID, UpdatingACustomFieldRequest body)

Updating a custom field

Updates the properties of an existing custom field in the specified mailing list.

### Example
```csharp
using System;
using System.Diagnostics;
using Moosend.Wrappers.CSharpWrapper.Api;
using Moosend.Wrappers.CSharpWrapper.Client;
using Moosend.Wrappers.CSharpWrapper.Model;

namespace Example
{
    public class UpdatingACustomFieldExample
    {
        public void main()
        {
            var apiInstance = new MailingListsApi();
            var format = format_example;  // string | 
            var customFieldID = customFieldID_example;  // string | The ID of the custom field to be updated.
            var apikey = apikey_example;  // string | You may find your API Key or generate a new one in your account settings.
            var mailingListID = mailingListID_example;  // string | The ID of the mailing list where the custom field belongs.
            var body = new UpdatingACustomFieldRequest(); // UpdatingACustomFieldRequest | 

            try
            {
                // Updating a custom field
                UpdatingACustomFieldResponse result = apiInstance.UpdatingACustomField(format, customFieldID, apikey, mailingListID, body);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling MailingListsApi.UpdatingACustomField: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **format** | **string**|  | 
 **customFieldID** | **string**| The ID of the custom field to be updated. | 
 **apikey** | **string**| You may find your API Key or generate a new one in your account settings. | 
 **mailingListID** | **string**| The ID of the mailing list where the custom field belongs. | 
 **body** | [**UpdatingACustomFieldRequest**](UpdatingACustomFieldRequest.md)|  | 

### Return type

[**UpdatingACustomFieldResponse**](UpdatingACustomFieldResponse.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="updatingamailinglist"></a>
# **UpdatingAMailingList**
> UpdatingAMailingListResponse UpdatingAMailingList (string format, string apikey, string mailingListID, UpdatingAMailingListRequest body)

Updating a mailing list

Updates the properties of an existing mailing list.

### Example
```csharp
using System;
using System.Diagnostics;
using Moosend.Wrappers.CSharpWrapper.Api;
using Moosend.Wrappers.CSharpWrapper.Client;
using Moosend.Wrappers.CSharpWrapper.Model;

namespace Example
{
    public class UpdatingAMailingListExample
    {
        public void main()
        {
            var apiInstance = new MailingListsApi();
            var format = format_example;  // string | 
            var apikey = apikey_example;  // string | You may find your API Key or generate a new one in your account settings.
            var mailingListID = mailingListID_example;  // string | The ID of the mailing list to be updated.
            var body = new UpdatingAMailingListRequest(); // UpdatingAMailingListRequest | 

            try
            {
                // Updating a mailing list
                UpdatingAMailingListResponse result = apiInstance.UpdatingAMailingList(format, apikey, mailingListID, body);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling MailingListsApi.UpdatingAMailingList: " + e.Message );
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
 **mailingListID** | **string**| The ID of the mailing list to be updated. | 
 **body** | [**UpdatingAMailingListRequest**](UpdatingAMailingListRequest.md)|  | 

### Return type

[**UpdatingAMailingListResponse**](UpdatingAMailingListResponse.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

