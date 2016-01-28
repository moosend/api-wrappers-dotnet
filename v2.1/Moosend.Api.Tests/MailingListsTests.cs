using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Moosend.Api.Client;
using Moosend.Api.Common;
using Moosend.Api.Common.Models;
using Moosend.Api.Common.Responses;
using Newtonsoft.Json;
using NUnit.Framework;

namespace Moosend.Api.Tests
{
    [TestFixture]
    public class MailingListsTests
    {
        private Uri _uri;
        private string _apiKey;
        private IMoosendApiClient _client;
        private TestHttpMessageHandler _handler;

        [SetUp]
        public void Setup()
        {            
            _uri = new Uri("https://api.moosend.com/v3");
            _apiKey = "moosend_api_key";
            _handler = new TestHttpMessageHandler();

            var ctx = new ServiceClientContext(_uri) { Handler = _handler };
            _client = new MoosendApiClient(_apiKey, ctx);
        }

        [Test]
        public async Task Given_MoosendApiClient_When_Getting_Mailing_Lists_Then_The_Right_Url_Is_Accessed()
        {
            var page = 2;
            var pageSize = 11;
            var url = string.Format("/lists/{0}/{1}.json?apikey={2}", page, pageSize, _apiKey);

            var expectedUrl = new Uri(_uri + url);

            try
            {
                await _client.GetMailingListsAsync(page, pageSize);
            }
            catch (Exception ex)
            {
                // known serialization exception caused by returning {} from test handler 
            }

            Assert.That(_handler.Requests[0].Method, Is.EqualTo(HttpMethod.Get));
            Assert.That(_handler.Requests[0].RequestUri.AbsoluteUri, Is.EqualTo(expectedUrl.AbsoluteUri));
        }

        [Test]
        public async Task Given_MoosendApiClient_When_Getting_Mailing_Lists_Then_The_Headers_Are_As_Expected()
        {
            try
            {
                await _client.GetMailingListsAsync();
            }
            catch (Exception ex)
            {
                // known serialization exception caused by returning {} from test handler 
            }

            IEnumerable<string> headers;
            _handler.Requests[0].Headers.TryGetValues("Keep-Alive", out headers);

            Assert.That(_handler.Requests[0].Headers.Accept.FirstOrDefault().MediaType, Is.EqualTo("application/json"));
            Assert.That(_handler.Requests[0].Headers.UserAgent.ToString(), Is.EqualTo(string.Format("moosend-api-client-{0}-{1}", Environment.Version, Environment.OSVersion)));
            Assert.That(headers.Single(), Is.EqualTo("false"));

        }

        [Test]
        public async Task Given_MoosendApiClient_When_Getting_Mailing_Lists_Then_Can_Get_Mailing_Lists()
        {
            // arrange
            var listId = new Guid();
            var ctx = new ServiceClientContext(_uri);

            var expectedLists = new MailingListsResult()
            {
                Paging = new Paging() { TotalResults = 1 },
                MailingLists = new List<MailingList>() { new MailingList() { Id = listId } }
            };

            var content = new ApiResponse<MailingListsResult>() { Context = expectedLists };
            ctx.Handler = new TestHttpMessageHandler(HttpStatusCode.OK, content);

            var client = new MoosendApiClient(_apiKey, ctx);

            // act
            var result = await client.GetMailingListsAsync();

            // assert
            Assert.That(result.Paging.TotalResults, Is.EqualTo(expectedLists.Paging.TotalResults));
            Assert.That(result.MailingLists.Count, Is.EqualTo(expectedLists.MailingLists.Count));
            Assert.That(result.MailingLists.Single().Id, Is.EqualTo(expectedLists.MailingLists.Single().Id));
        }

        [Test]
        public async Task Given_MoosendApiClient_When_Creating_List_Then_The_Right_Url_Is_Accessed()
        {
            var url = string.Format("/lists/create.json?apikey={0}", _apiKey);

            var expectedUrl = new Uri(_uri + url);

            try
            {
                await _client.CreateMailingListAsync("list_name");
            }
            catch (Exception ex)
            {
                // known serialization exception caused by returning {} from test handler 
            }

            Assert.That(_handler.Requests[0].Method, Is.EqualTo(HttpMethod.Post));
            Assert.That(_handler.Requests[0].RequestUri.AbsoluteUri, Is.EqualTo(expectedUrl.AbsoluteUri));
        }

        [Test]
        public async Task Given_MoosendApiClient_When_Creating_List_Then_The_Headers_Are_As_Expected()
        {
            try
            {
                await _client.CreateMailingListAsync("list_name");
            }
            catch (Exception ex)
            {
                // known serialization exception caused by returning {} from test handler 
            }

            IEnumerable<string> headers;
            _handler.Requests[0].Headers.TryGetValues("Keep-Alive", out headers);

            Assert.That(_handler.Requests[0].Headers.Accept.FirstOrDefault().MediaType, Is.EqualTo("application/json"));
            Assert.That(_handler.Requests[0].Headers.UserAgent.ToString(), Is.EqualTo(string.Format("moosend-api-client-{0}-{1}", Environment.Version, Environment.OSVersion)));
            Assert.That(headers.Single(), Is.EqualTo("false"));
        }

        [Test]
        public async Task Given_MoosendApiClient_When_Creating_List_Then_Can_Create()
        {
            // arrange
            var ctx = new ServiceClientContext(_uri);

            var expectedId = new Guid();

            var content = new ApiResponse<Guid>() { Context = expectedId };
            ctx.Handler = new TestHttpMessageHandler(HttpStatusCode.OK, content);

            var client = new MoosendApiClient(_apiKey, ctx);

            // act
            var listId = await client.CreateMailingListAsync("list_name");

            // assert
            Assert.That(listId, Is.EqualTo(expectedId));
        }

        [Test]
        public async Task Given_MoosendApiClient_When_Creating_List_Then_The_Payload_Is_As_Expected()
        {
            const string listName = "list_name";
            const string confirmationPage = "http://a-confirmationPage.com";
            const string redirectAfterUnsubscribePage = "http://a-redirectAfterUnsubscribePage.com";
            var parameters = new
            {
                Name = listName,
                ConfirmationPage = confirmationPage,
                RedirectAfterUnsubscribePage = redirectAfterUnsubscribePage
            };

            try
            {
                await _client.CreateMailingListAsync(listName, confirmationPage, redirectAfterUnsubscribePage);

            }
            catch (Exception ex)
            {
                // known serialization exception caused by returning {} from test handler 
            }

            var expectedJson = JsonConvert.SerializeObject(parameters);

            Assert.That(_handler.Payloads[0].Contains(expectedJson));
        }

        [Test]
        public async Task Given_MoosendApiClient_When_Updating_List_Then_The_Right_Url_Is_Accessed()
        {
            var url = string.Format("/lists/{0}/update.json?apikey={1}", new Guid(), _apiKey);

            var expectedUrl = new Uri(_uri + url);

            try
            {
                await _client.UpdateMailingListAsync(new Guid(), "list_name");
            }
            catch (Exception ex)
            {
                // known serialization exception caused by returning {} from test handler 
            }

            Assert.That(_handler.Requests[0].Method, Is.EqualTo(HttpMethod.Post));
            Assert.That(_handler.Requests[0].RequestUri.AbsoluteUri, Is.EqualTo(expectedUrl.AbsoluteUri));
        }

        [Test]
        public async Task Given_MoosendApiClient_When_Updating_List_Then_The_Headers_Are_As_Expected()
        {
            try
            {
                await _client.UpdateMailingListAsync(new Guid(), "list_name");
            }
            catch (Exception ex)
            {
                // known serialization exception caused by returning {} from test handler 
            }

            IEnumerable<string> headers;
            _handler.Requests[0].Headers.TryGetValues("Keep-Alive", out headers);

            Assert.That(_handler.Requests[0].Headers.Accept.FirstOrDefault().MediaType, Is.EqualTo("application/json"));
            Assert.That(_handler.Requests[0].Headers.UserAgent.ToString(), Is.EqualTo(string.Format("moosend-api-client-{0}-{1}", Environment.Version, Environment.OSVersion)));
            Assert.That(headers.Single(), Is.EqualTo("false"));
        }

        [Test]
        public async Task Given_MoosendApiClient_When_Updating_List_Then_Can_Update()
        {
            // arrange
            var ctx = new ServiceClientContext(_uri);

            var content = new ApiResponse<Guid> { Context = new Guid() };
            ctx.Handler = new TestHttpMessageHandler(HttpStatusCode.OK, content);

            var client = new MoosendApiClient(_apiKey, ctx);

            // act
            var listId = await client.UpdateMailingListAsync(new Guid(), "list_name");

            // assert
            Assert.That(listId, Is.EqualTo(content.Context));
        }

        [Test]
        public async Task Given_MoosendApiClient_When_Updating_List_Then_The_Payload_Is_As_Expected()
        {
            const string listName = "list_name";
            const string confirmationPage = "http://a-confirmationPage.com";
            const string redirectAfterUnsubscribePage = "http://a-redirectAfterUnsubscribePage.com";
            var parameters = new
            {
                Name = listName,
                ConfirmationPage = confirmationPage,
                RedirectAfterUnsubscribePage = redirectAfterUnsubscribePage
            };

            try
            {
                await _client.UpdateMailingListAsync(new Guid(), listName, confirmationPage, redirectAfterUnsubscribePage);

            }
            catch (Exception ex)
            {
                // known serialization exception caused by returning {} from test handler 
            }

            var expectedJson = JsonConvert.SerializeObject(parameters);

            Assert.That(_handler.Payloads[0].Contains(expectedJson));
        }

        [Test]
        public async Task Given_MoosendApiClient_When_Getting_A_Mailing_List_Then_The_Right_Url_Is_Accessed()
        {
            var listId = new Guid();

            var url = string.Format("/lists/{0}/details.json?apikey={1}&WithStatistics={2}", listId, _apiKey, false);

            var expectedUrl = new Uri(_uri + url);

            try
            {
                await _client.GetMailingListByIdAsync(listId, false);
            }
            catch (Exception ex)
            {
                // known serialization exception caused by returning {} from test handler 
            }

            Assert.That(_handler.Requests[0].Method, Is.EqualTo(HttpMethod.Get));
            Assert.That(_handler.Requests[0].RequestUri.AbsoluteUri, Is.EqualTo(expectedUrl.AbsoluteUri));
        }

        [Test]
        public async Task Given_MoosendApiClient_When_Getting_A_Mailing_List_Then_The_Headers_Are_As_Expected()
        {
            try
            {
                await _client.GetMailingListByIdAsync(new Guid());
            }
            catch (Exception ex)
            {
                // known serialization exception caused by returning {} from test handler 
            }

            IEnumerable<string> headers;
            _handler.Requests[0].Headers.TryGetValues("Keep-Alive", out headers);

            Assert.That(_handler.Requests[0].Headers.Accept.FirstOrDefault().MediaType, Is.EqualTo("application/json"));
            Assert.That(_handler.Requests[0].Headers.UserAgent.ToString(), Is.EqualTo(string.Format("moosend-api-client-{0}-{1}", Environment.Version, Environment.OSVersion)));
            Assert.That(headers.Single(), Is.EqualTo("false"));
        }

        [Test]
        public async Task Given_MoosendApiClient_When_Getting_A_Mailing_List_Then_Can_Get_Mailing_List()
        {
            // arrange
            var ctx = new ServiceClientContext(_uri);

            var expectedList = new MailingList()
            {
                Id = new Guid()
            };

            var content = new ApiResponse<MailingList>() { Context = expectedList };
            ctx.Handler = new TestHttpMessageHandler(HttpStatusCode.OK, content);

            var client = new MoosendApiClient(_apiKey, ctx);

            // act
            var listResult = await client.GetMailingListByIdAsync(new Guid());

            // assert
            Assert.That(listResult.Id, Is.EqualTo(expectedList.Id));
        }

        [Test]
        public async Task Given_MoosendApiClient_When_Getting_Subscribers_Then_The_Right_Url_Is_Accessed()
        {
            var listId = new Guid();
            var status = SubscribeType.Bounced;
            var page = 2;
            var pageSize = 300;

            var url = string.Format("/lists/{0}/subscribers/{1}.json?apikey={2}&Page={3}&PageSize={4}", listId, status, _apiKey, page, pageSize);

            var expectedUrl = new Uri(_uri + url);

            try
            {
                await _client.GetSubscribersAsync(listId, status, null, page, pageSize);
            }
            catch (Exception ex)
            {
                // known serialization exception caused by returning {} from test handler 
            }

            Assert.That(_handler.Requests[0].Method, Is.EqualTo(HttpMethod.Get));
            Assert.That(_handler.Requests[0].RequestUri.AbsoluteUri, Is.EqualTo(expectedUrl.AbsoluteUri));
        }

        [Test]
        public async Task Given_MoosendApiClient_When_Getting_Subscribers_List_Then_The_Headers_Are_As_Expected()
        {
            try
            {
                await _client.GetSubscribersAsync(new Guid(), null);
            }
            catch (Exception ex)
            {
                // known serialization exception caused by returning {} from test handler 
            }

            IEnumerable<string> headers;
            _handler.Requests[0].Headers.TryGetValues("Keep-Alive", out headers);

            Assert.That(_handler.Requests[0].Headers.Accept.FirstOrDefault().MediaType, Is.EqualTo("application/json"));
            Assert.That(_handler.Requests[0].Headers.UserAgent.ToString(), Is.EqualTo(string.Format("moosend-api-client-{0}-{1}", Environment.Version, Environment.OSVersion)));
            Assert.That(headers.Single(), Is.EqualTo("false"));
        }

        [Test]
        public async Task Given_MoosendApiClient_When_Getting_Subscribers_List_Then_Can_Get_Subscribers()
        {
            // arrange
            var ctx = new ServiceClientContext(_uri);

            var expectedResult = new SubscribersResult
            {
                Paging = new Paging() { TotalResults = 200},
                Subscribers = new List<Subscriber>() {new Subscriber() {Id = new Guid()}}
            };

            var expectedContent = new ApiResponse<SubscribersResult>() { Context = expectedResult };

            ctx.Handler = new TestHttpMessageHandler(HttpStatusCode.OK, expectedContent);
            var client = new MoosendApiClient(_apiKey, ctx);

            // act
            var result = await client.GetSubscribersAsync(new Guid(), null);

            // assert
            Assert.That(result.Subscribers.Count, Is.EqualTo(expectedResult.Subscribers.Count));
            Assert.That(result.Subscribers.Single().Id, Is.EqualTo(expectedResult.Subscribers.Single().Id));
            Assert.That(result.Paging.TotalResults, Is.EqualTo(expectedResult.Paging.TotalResults));
        }

        [Test] public async Task Given_MoosendApiClient_When_Deleting_A_Mailing_List_Then_The_Right_Url_Is_Accessed()
        {
            var listId = new Guid();

            var url = string.Format("/lists/{0}/delete.json?apikey={1}", listId, _apiKey);

            var expectedUrl = new Uri(_uri + url);

            try
            {
                await _client.DeleteMailingListAsync(listId);
            }
            catch (Exception ex)
            {
                // known serialization exception caused by returning {} from test handler 
            }

            Assert.That(_handler.Requests[0].Method, Is.EqualTo(HttpMethod.Delete));
            Assert.That(_handler.Requests[0].RequestUri.AbsoluteUri, Is.EqualTo(expectedUrl.AbsoluteUri));
        }

        [Test]
        public async Task Given_MoosendApiClient_When_Deleting_A_Mailing_List_Then_The_Headers_Are_As_Expected()
        {
            try
            {
                await _client.DeleteMailingListAsync(new Guid());
            }
            catch (Exception ex)
            {
                // known serialization exception caused by returning {} from test handler 
            }

            IEnumerable<string> headers;
            _handler.Requests[0].Headers.TryGetValues("Keep-Alive", out headers);

            Assert.That(_handler.Requests[0].Headers.Accept.FirstOrDefault().MediaType, Is.EqualTo("application/json"));
            Assert.That(_handler.Requests[0].Headers.UserAgent.ToString(), Is.EqualTo(string.Format("moosend-api-client-{0}-{1}", Environment.Version, Environment.OSVersion)));
            Assert.That(headers.Single(), Is.EqualTo("false"));
        }

        [Test]
        public async Task Given_MoosendApiClient_When_Deleting_A_Mailing_List_Then_Can_Delete_Mailing_List()
        {
            // arrange
            var ctx = new ServiceClientContext(_uri);

            var content = new ApiResponse<bool>() { Context = true };
            ctx.Handler = new TestHttpMessageHandler(HttpStatusCode.OK, content);

            var client = new MoosendApiClient(_apiKey, ctx);

            // act
            var success = await client.DeleteMailingListAsync(new Guid());

            // assert
            Assert.That(success, Is.True);
        }

        [Test]
        public async Task Given_MoosendApiClient_When_Creating_Custom_Field_Then_The_Right_Url_Is_Accessed()
        {
            var listId = new Guid();
            var url = string.Format("/lists/{0}/customfields/create.json?apikey={1}", listId, _apiKey);

            var expectedUrl = new Uri(_uri + url);

            try
            {
                await _client.CreateCustomFieldAsync(listId, "custom_field_name");
            }
            catch (Exception ex)
            {
                // known serialization exception caused by returning {} from test handler 
            }

            Assert.That(_handler.Requests[0].Method, Is.EqualTo(HttpMethod.Post));
            Assert.That(_handler.Requests[0].RequestUri.AbsoluteUri, Is.EqualTo(expectedUrl.AbsoluteUri));
        }

        [Test]
        public async Task Given_MoosendApiClient_When_Creating_Custom_Field_Then_The_Headers_Are_As_Expected()
        {
            try
            {
                await _client.CreateCustomFieldAsync(new Guid(), "cf_name");
            }
            catch (Exception ex)
            {
                // known serialization exception caused by returning {} from test handler 
            }

            IEnumerable<string> headers;
            _handler.Requests[0].Headers.TryGetValues("Keep-Alive", out headers);

            Assert.That(_handler.Requests[0].Headers.Accept.FirstOrDefault().MediaType, Is.EqualTo("application/json"));
            Assert.That(_handler.Requests[0].Headers.UserAgent.ToString(), Is.EqualTo(string.Format("moosend-api-client-{0}-{1}", Environment.Version, Environment.OSVersion)));
            Assert.That(headers.Single(), Is.EqualTo("false"));
        }

        [Test]
        public async Task Given_MoosendApiClient_When_Creating_Custom_Field_Then_Can_Create()
        {
            // arrange
            var ctx = new ServiceClientContext(_uri);

            var expectedId = new Guid();

            var content = new ApiResponse<Guid>() { Context = expectedId };
            ctx.Handler = new TestHttpMessageHandler(HttpStatusCode.OK, content);

            var client = new MoosendApiClient(_apiKey, ctx);

            // act
            var cfId = await client.CreateCustomFieldAsync(new Guid(), "cf_name");

            // assert
            Assert.That(cfId, Is.EqualTo(expectedId));
        }

        [Test]
        public async Task Given_MoosendApiClient_When_Creating_Custom_Field_Then_The_Payload_Is_As_Expected()
        {
            var name = "cf_name";
            var type = CustomFieldType.SingleSelectDropdown;
            var isRequired = false;
            var options = "yes, no";

            var parameters = new
            {
                Name = name,
                CustomFieldType = type,
                IsRequired = isRequired,
                Options = options
            };

            try
            {
                await _client.CreateCustomFieldAsync(new Guid(), name, type, isRequired, options);
            }
            catch (Exception ex)
            {
                // known serialization exception caused by returning {} from test handler 
            }

            var expectedJson = JsonConvert.SerializeObject(parameters);

            Assert.That(_handler.Payloads[0].Contains(expectedJson));
        }

        [Test]
        public async Task Given_MoosendApiClient_When_Updating_Custom_Field_Then_The_Right_Url_Is_Accessed()
        {
            var listId = new Guid();
            var cfId = new Guid();
            var url = string.Format("/lists/{0}/customfields/{1}/update.json?apikey={2}", listId, cfId, _apiKey);

            var expectedUrl = new Uri(_uri + url);

            try
            {
                await _client.UpdateCustomFieldAsync(listId, cfId, "cf_name");
            }
            catch (Exception ex)
            {
                // known serialization exception caused by returning {} from test handler 
            }

            Assert.That(_handler.Requests[0].Method, Is.EqualTo(HttpMethod.Post));
            Assert.That(_handler.Requests[0].RequestUri.AbsoluteUri, Is.EqualTo(expectedUrl.AbsoluteUri));
        }

        [Test]
        public async Task Given_MoosendApiClient_When_Updating_Custom_Field_Then_The_Headers_Are_As_Expected()
        {
            try
            {
                await _client.UpdateCustomFieldAsync(new Guid(), new Guid(), "cf_name");
            }
            catch (Exception ex)
            {
                // known serialization exception caused by returning {} from test handler 
            }

            IEnumerable<string> headers;
            _handler.Requests[0].Headers.TryGetValues("Keep-Alive", out headers);

            Assert.That(_handler.Requests[0].Headers.Accept.FirstOrDefault().MediaType, Is.EqualTo("application/json"));
            Assert.That(_handler.Requests[0].Headers.UserAgent.ToString(), Is.EqualTo(string.Format("moosend-api-client-{0}-{1}", Environment.Version, Environment.OSVersion)));
            Assert.That(headers.Single(), Is.EqualTo("false"));
        }

        [Test]
        public async Task Given_MoosendApiClient_When_Updating_Custom_Field_Then_Can_Create()
        {
            // arrange
            var ctx = new ServiceClientContext(_uri);

            var expectedId = new Guid();

            var content = new ApiResponse<Guid>() { Context = expectedId };
            ctx.Handler = new TestHttpMessageHandler(HttpStatusCode.OK, content);

            var client = new MoosendApiClient(_apiKey, ctx);

            // act
            var cfId = await client.UpdateCustomFieldAsync(new Guid(), new Guid(), "cf_name");

            // assert
            Assert.That(cfId, Is.EqualTo(expectedId));
        }

        [Test]
        public async Task Given_MoosendApiClient_When_Updating_Custom_Field_Then_The_Payload_Is_As_Expected()
        {
            var name = "cf_name";
            var type = CustomFieldType.SingleSelectDropdown;
            var isRequired = false;
            var options = "yes, no";

            var parameters = new
            {
                Name = name,
                CustomFieldType = type,
                IsRequired = isRequired,
                Options = options
            };

            try
            {
                await _client.CreateCustomFieldAsync(new Guid(), name, type, isRequired, options);
            }
            catch (Exception ex)
            {
                // known serialization exception caused by returning {} from test handler 
            }

            var expectedJson = JsonConvert.SerializeObject(parameters);

            Assert.That(_handler.Payloads[0].Contains(expectedJson));
        }
    }
}