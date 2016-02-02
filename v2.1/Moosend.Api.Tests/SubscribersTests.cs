using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using Moosend.Api.Client;
using Moosend.Api.Common.Models;
using Moosend.Api.Common.Responses;
using Newtonsoft.Json;
using NUnit.Framework;

namespace Moosend.Api.Tests
{
    [TestFixture]
    public class SubscribersTests
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
        public async Task Given_MoosendApiClient_When_Getting_Subscriber_By_Email_Lists_Then_The_Right_Url_Is_Accessed()
        {
            var listId = new Guid();
            var email = "email@test.com";
            var url = string.Format("/subscribers/{0}/view.json?apikey={1}&Email={2}", listId, _apiKey, email);

            var expectedUrl = new Uri(_uri + url);

            try
            {
                await _client.GetSubscriberByEmailAsync(listId, email);
            }
            catch (Exception ex)
            {
                // known serialization exception caused by returning {} from test handler 
            }

            Assert.That(_handler.Requests[0].Method, Is.EqualTo(HttpMethod.Get));
            Assert.That(HttpUtility.UrlDecode(_handler.Requests[0].RequestUri.AbsoluteUri), Is.EqualTo(expectedUrl.AbsoluteUri));
        }

        [Test]
        public async Task Given_MoosendApiClient_When_Getting_Subscriber_By_Email_Lists_Then_The_Headers_Are_As_Expected()
        {
            try
            {
                await _client.GetSubscriberByEmailAsync(new Guid(), "email@test.com");
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
        public async Task Given_MoosendApiClient_When_Getting_Subscriber_By_Email_Lists_Then_Can_Get_Subscriber()
        {
            // arrange
            var ctx = new ServiceClientContext(_uri);

            var expectedSubscriber = new Subscriber()
            {
                Id = new Guid()
            };

            var content = new ApiResponse<Subscriber>() { Context = expectedSubscriber };
            ctx.Handler = new TestHttpMessageHandler(HttpStatusCode.OK, content);

            var client = new MoosendApiClient(_apiKey, ctx);

            // act
            var result = await client.GetSubscriberByEmailAsync(new Guid(), "email@test.com");

            // assert
            Assert.That(result.Id, Is.EqualTo(expectedSubscriber.Id));
        }

        [Test]
        public async Task Given_MoosendApiClient_When_Subscribing_Member_Then_The_Right_Url_Is_Accessed()
        {
            var listId = new Guid();
            var url = string.Format("/subscribers/{0}/subscribe.json?apikey={1}", listId, _apiKey);

            var expectedUrl = new Uri(_uri + url);

            try
            {
                await _client.SubscribeMemberAsync(listId, new SubscriberParams());
            }
            catch (Exception ex)
            {
                // known serialization exception caused by returning {} from test handler 
            }

            Assert.That(_handler.Requests[0].Method, Is.EqualTo(HttpMethod.Post));
            Assert.That(_handler.Requests[0].RequestUri.AbsoluteUri, Is.EqualTo(expectedUrl.AbsoluteUri));
        }

        [Test]
        public async Task Given_MoosendApiClient_When_Subscribing_Member_Then_The_Headers_Are_As_Expected()
        {
            try
            {
                await _client.SubscribeMemberAsync(new Guid(), new SubscriberParams());
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
        public async Task Given_MoosendApiClient_When_Subscribing_Member_Then_Can_Subscribe()
        {
            // arrange
            var ctx = new ServiceClientContext(_uri);

            var expectedSubscriber = new Subscriber()
            {
                Id = new Guid()
            };

            var content = new ApiResponse<Subscriber>() { Context = expectedSubscriber };
            ctx.Handler = new TestHttpMessageHandler(HttpStatusCode.OK, content);

            var client = new MoosendApiClient(_apiKey, ctx);

            // act
            var subscriber = await client.SubscribeMemberAsync(new Guid(), new SubscriberParams());

            // assert
            Assert.That(subscriber.Id, Is.EqualTo(expectedSubscriber.Id));
        }

        [Test]
        public async Task Given_MoosendApiClient_When_Subscribing_Member_Then_The_Payload_Is_As_Expected()
        {
            var member = new SubscriberParams()
            {
                Name = "name",
                Email = "email@test.com",
                CustomFields = new Dictionary<string, string>()
                {
                    {"Age", "30"}
                }
            };

            try
            {
                await _client.SubscribeMemberAsync(new Guid(), member);

            }
            catch (Exception ex)
            {
                // known serialization exception caused by returning {} from test handler 
            }

            var expectedJson = "{\"Name\":\"name\",\"Email\":\"email@test.com\",\"CustomFields\":[\"Age=30\"]}";

            Assert.That(_handler.Payloads[0].Contains(expectedJson));
        }

        [Test]
        public async Task Given_MoosendApiClient_When_Updating_Member_Then_The_Right_Url_Is_Accessed()
        {
            var listId = new Guid();
            var subId = new Guid();
            var url = string.Format("/subscribers/{0}/update/{1}.json?apikey={2}", listId, subId, _apiKey);

            var expectedUrl = new Uri(_uri + url);

            try
            {
                await _client.UpdateMemberAsync(listId, subId, new SubscriberParams());
            }
            catch (Exception ex)
            {
                // known serialization exception caused by returning {} from test handler 
            }

            Assert.That(_handler.Requests[0].Method, Is.EqualTo(HttpMethod.Post));
            Assert.That(_handler.Requests[0].RequestUri.AbsoluteUri, Is.EqualTo(expectedUrl.AbsoluteUri));
        }

        [Test]
        public async Task Given_MoosendApiClient_When_Updating_Member_Then_The_Headers_Are_As_Expected()
        {
            try
            {
                await _client.UpdateMemberAsync(new Guid(), new Guid(), new SubscriberParams());
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
        public async Task Given_MoosendApiClient_When_Updating_Member_Then_Can_Update()
        {
            // arrange
            var ctx = new ServiceClientContext(_uri);

            var expectedSubscriber = new Subscriber()
            {
                Id = new Guid()
            };

            var content = new ApiResponse<Subscriber>() { Context = expectedSubscriber };
            ctx.Handler = new TestHttpMessageHandler(HttpStatusCode.OK, content);

            var client = new MoosendApiClient(_apiKey, ctx);

            // act
            var subscriber = await client.UpdateMemberAsync(new Guid(), new Guid(), new SubscriberParams());

            // assert
            Assert.That(subscriber.Id, Is.EqualTo(expectedSubscriber.Id));
        }

        [Test]
        public async Task Given_MoosendApiClient_When_Updating_Member_Then_The_Payload_Is_As_Expected()
        {
            var member = new SubscriberParams()
            {
                Name = "name",
                Email = "email@test.com",
                CustomFields = new Dictionary<string, string>()
                {
                    {"Age", "30"}
                }
            };

            try
            {
                await _client.UpdateMemberAsync(new Guid(), new Guid(), member);

            }
            catch (Exception ex)
            {
                // known serialization exception caused by returning {} from test handler 
            }

            var expectedJson = "{\"Name\":\"name\",\"Email\":\"email@test.com\",\"CustomFields\":[\"Age=30\"]}";

            Assert.That(_handler.Payloads[0].Contains(expectedJson));
        }

        [Test]
        public async Task Given_MoosendApiClient_When_Subscribing_Many_Then_The_Right_Url_Is_Accessed()
        {
            var listId = new Guid();
            var url = string.Format("/subscribers/{0}/subscribe_many.json?apikey={1}", listId, _apiKey);

            var expectedUrl = new Uri(_uri + url);

            try
            {
                await _client.SubscribeManyAsync(listId, new List<SubscriberParams>());
            }
            catch (Exception ex)
            {
                // known serialization exception caused by returning {} from test handler 
            }

            Assert.That(_handler.Requests[0].Method, Is.EqualTo(HttpMethod.Post));
            Assert.That(_handler.Requests[0].RequestUri.AbsoluteUri, Is.EqualTo(expectedUrl.AbsoluteUri));
        }

        [Test]
        public async Task Given_MoosendApiClient_When_Subscribing_Many_Then_The_Headers_Are_As_Expected()
        {
            try
            {
                await _client.SubscribeManyAsync(new Guid(), new List<SubscriberParams>());
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
        public async Task Given_MoosendApiClient_When_Subscribing_Many_Then_Can_Subscribe_Many()
        {
            // arrange
            var ctx = new ServiceClientContext(_uri);

            var expectedSubscribers = new List<Subscriber>()
            {
                new Subscriber() { Id = new Guid() }
            };

            var content = new ApiResponse<List<Subscriber>>() { Context = expectedSubscribers };
            ctx.Handler = new TestHttpMessageHandler(HttpStatusCode.OK, content);

            var client = new MoosendApiClient(_apiKey, ctx);

            // act
            var subscribers = await client.SubscribeManyAsync(new Guid(), new List<SubscriberParams>());

            // assert
            Assert.That(subscribers.Count, Is.EqualTo(expectedSubscribers.Count));
            Assert.That(subscribers.Single().Id, Is.EqualTo(expectedSubscribers.Single().Id));
        }

        [Test]
        public async Task Given_MoosendApiClient_When_Subscribing_Many_Then_The_Payload_Is_As_Expected()
        {
            var members = new List<SubscriberParams>()
            {
                new SubscriberParams()
                {
                    Name = "name",
                    Email = "email@test.com",
                    CustomFields = new Dictionary<string, string>()
                    {
                        {"Age", "30"}
                    }
                }
            };

            try
            {
                await _client.SubscribeManyAsync(new Guid(), members);

            }
            catch (Exception ex)
            {
                // known serialization exception caused by returning {} from test handler 
            }

            var expectedJson = "{\"Subscribers\":[{\"Name\":\"name\",\"Email\":\"email@test.com\",\"CustomFields\":[\"Age=30\"]}]}";

            Assert.That(_handler.Payloads[0].Contains(expectedJson));
        }

        [Test]
        public async Task Given_MoosendApiClient_When_Unsubscribing_Member_Then_The_Right_Url_Is_Accessed()
        {
            var listId = new Guid();
            var cmId = new Guid();
            var url = string.Format("/subscribers/{0}/{1}/unsubscribe.json?apikey={2}", listId, cmId, _apiKey);

            var expectedUrl = new Uri(_uri + url);

            try
            {
                await _client.UnsubscribeMemberAsync(listId, cmId, "email@test.com");
            }
            catch (Exception ex)
            {
                // known serialization exception caused by returning {} from test handler 
            }

            Assert.That(_handler.Requests[0].Method, Is.EqualTo(HttpMethod.Post));
            Assert.That(_handler.Requests[0].RequestUri.AbsoluteUri, Is.EqualTo(expectedUrl.AbsoluteUri));
        }

        [Test]
        public async Task Given_MoosendApiClient_When_Unsubscribing_Member_Then_The_Headers_Are_As_Expected()
        {
            try
            {
                await _client.UnsubscribeMemberAsync(new Guid(), new Guid(), "email@test.com");
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
        public async Task Given_MoosendApiClient_When_Unsubscribing_Member_Then_Can_Unsubscribe()
        {
            // arrange
            var ctx = new ServiceClientContext(_uri);
                       
            var content = new ApiResponse<bool>() { Context = true };
            ctx.Handler = new TestHttpMessageHandler(HttpStatusCode.OK, content);

            var client = new MoosendApiClient(_apiKey, ctx);

            // act
            var success = await client.UnsubscribeMemberAsync(new Guid(), new Guid(), "email@test.com");

            // assert
            Assert.That(success, Is.True);
        }

        [Test]
        public async Task Given_MoosendApiClient_When_Unsubscribing_Member_Then_The_Payload_Is_As_Expected()
        {
            var email = "email@test.com";

            try
            {
                await _client.UnsubscribeMemberAsync(new Guid(), new Guid(), email);

            }
            catch (Exception ex)
            {
                // known serialization exception caused by returning {} from test handler 
            }

            var expectedJson = JsonConvert.SerializeObject(email);

            Assert.That(_handler.Payloads[0].Contains(expectedJson));
        }

        [Test]
        public async Task Given_MoosendApiClient_When_Removing_Member_Then_The_Right_Url_Is_Accessed()
        {
            var listId = new Guid();
            var email = "email@test.com";
            
            var url = string.Format("/subscribers/{0}/remove.json?apikey={1}", listId, _apiKey);

            var expectedUrl = new Uri(_uri + url);

            try
            {
                await _client.RemoveMemberAsync(listId, email);
            }
            catch (Exception ex)
            {
                // known serialization exception caused by returning {} from test handler 
            }

            Assert.That(_handler.Requests[0].Method, Is.EqualTo(HttpMethod.Post));
            Assert.That(_handler.Requests[0].RequestUri.AbsoluteUri, Is.EqualTo(expectedUrl.AbsoluteUri));
        }

        [Test]
        public async Task Given_MoosendApiClient_When_Removing_Member_Then_The_Headers_Are_As_Expected()
        {
            try
            {
                await _client.RemoveMemberAsync(new Guid(), "email@test.com");
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
        public async Task Given_MoosendApiClient_When_Removing_Member_Then_Can_Remove()
        {
            // arrange
            var ctx = new ServiceClientContext(_uri);

            var content = new ApiResponse<bool>() { Context = true };
            ctx.Handler = new TestHttpMessageHandler(HttpStatusCode.OK, content);

            var client = new MoosendApiClient(_apiKey, ctx);

            // act
            var success = await client.RemoveMemberAsync(new Guid(), "email@test.com");

            // assert
            Assert.That(success, Is.True);
        }

        [Test]
        public async Task Given_MoosendApiClient_When_Removing_Member_Then_The_Payload_Is_As_Expected()
        {
            var email = "email@test.com";

            try
            {
                await _client.RemoveMemberAsync(new Guid(), email);

            }
            catch (Exception ex)
            {
                // known serialization exception caused by returning {} from test handler 
            }

            var expectedJson = JsonConvert.SerializeObject(email);

            Assert.That(_handler.Payloads[0].Contains(expectedJson));
        }

        [Test]
        public async Task Given_MoosendApiClient_When_Removing_Many_Then_The_Right_Url_Is_Accessed()
        {
            var listId = new Guid();

            var url = string.Format("/subscribers/{0}/remove_many.json?apikey={1}", listId, _apiKey);

            var expectedUrl = new Uri(_uri + url);

            try
            {
                await _client.RemoveManyAsync(listId, new List<string>());
            }
            catch (Exception ex)
            {
                // known serialization exception caused by returning {} from test handler 
            }

            Assert.That(_handler.Requests[0].Method, Is.EqualTo(HttpMethod.Post));
            Assert.That(_handler.Requests[0].RequestUri.AbsoluteUri, Is.EqualTo(expectedUrl.AbsoluteUri));
        }

        [Test]
        public async Task Given_MoosendApiClient_When_Removing_Many_Then_The_Headers_Are_As_Expected()
        {
            try
            {
                await _client.RemoveManyAsync(new Guid(), new List<string>());
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
        public async Task Given_MoosendApiClient_When_Removing_Many_Then_Can_Remove()
        {
            // arrange
            var ctx = new ServiceClientContext(_uri);

            var content = new ApiResponse<bool>() { Context = true };
            ctx.Handler = new TestHttpMessageHandler(HttpStatusCode.OK, content);

            var client = new MoosendApiClient(_apiKey, ctx);

            // act
            var success = await client.RemoveManyAsync(new Guid(), new List<string>());

            // assert
            Assert.That(success, Is.True);
        }

        [Test]
        public async Task Given_MoosendApiClient_When_Removing_Many_Then_The_Payload_Is_As_Expected()
        {
            var emails = new List<string>()
            {
                "email1@test.com",
                "email2@test.com"
            };

            try
            {
                await _client.RemoveManyAsync(new Guid(), emails);

            }
            catch (Exception ex)
            {
                // known serialization exception caused by returning {} from test handler 
            }

            var expectedJson = JsonConvert.SerializeObject(string.Join(",", emails.ToArray()));

            Assert.That(_handler.Payloads[0].Contains(expectedJson));
        }

        [Test]
        public async Task Given_MoosendApiClient_When_Getting_Subscriber_By_Id_Lists_Then_The_Right_Url_Is_Accessed()
        {
            var listId = new Guid();
            var subId = new Guid();
            var url = string.Format("/subscribers/{0}/find/{1}.json?apikey={2}", listId, subId, _apiKey);

            var expectedUrl = new Uri(_uri + url);

            try
            {
                await _client.GetSubscriberByIdAsync(listId, subId);
            }
            catch (Exception ex)
            {
                // known serialization exception caused by returning {} from test handler 
            }

            Assert.That(_handler.Requests[0].Method, Is.EqualTo(HttpMethod.Get));
            Assert.That(HttpUtility.UrlDecode(_handler.Requests[0].RequestUri.AbsoluteUri), Is.EqualTo(expectedUrl.AbsoluteUri));
        }

        [Test]
        public async Task Given_MoosendApiClient_When_Getting_Subscriber_By_Id_Lists_Then_The_Headers_Are_As_Expected()
        {
            try
            {
                await _client.GetSubscriberByIdAsync(new Guid(), new Guid());
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
        public async Task Given_MoosendApiClient_When_Getting_Subscriber_By_Id_Lists_Then_Can_Get_Subscriber()
        {
            // arrange
            var ctx = new ServiceClientContext(_uri);

            var expectedSubscriber = new Subscriber()
            {
                Id = new Guid()
            };

            var content = new ApiResponse<Subscriber>() { Context = expectedSubscriber };
            ctx.Handler = new TestHttpMessageHandler(HttpStatusCode.OK, content);

            var client = new MoosendApiClient(_apiKey, ctx);

            // act
            var result = await client.GetSubscriberByIdAsync(new Guid(), new Guid());

            // assert
            Assert.That(result.Id, Is.EqualTo(expectedSubscriber.Id));
        }
    }
}