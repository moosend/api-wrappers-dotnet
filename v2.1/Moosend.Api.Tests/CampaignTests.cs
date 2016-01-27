using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using Moosend.Api.Client;
using Moosend.Api.Common;
using Moosend.Api.Common.Models;
using Moosend.Api.Common.Responses;
using Newtonsoft.Json;
using NUnit.Framework;

namespace Moosend.Api.Tests
{
    [TestFixture]
    public class CampaignTests
    {
        private Uri _uri;
        private string _apiKey;
        private IMoosendApiClient _client;
        private TestHttpMessageHandler _handler;

        [SetUp]
        public void Setup()
        {
            var url = "https://api.moosend.com/v3";
            _uri = new Uri(url);

            _apiKey = "moosend_api_key";

            _handler = new TestHttpMessageHandler();
            var ctx = new ServiceClientContext(_uri) { Handler = _handler };
            _client = new MoosendApiClient(_apiKey, ctx);
        }

        #region Campaign API tests

        [Test]
        public async Task Given_MoosendApiClient_When_Getting_All_Campaigns_Then_The_Right_Url_Is_Accessed()
        {
            const int page = 2;
            const int pageSize = 11;
            const string sortBy = "Name";
            const string sortMethod = "DESC";
            var url = string.Format("/campaigns/{0}/{1}.json?apikey={2}&SortBy={3}&SortMethod={4}", page, pageSize, _apiKey, sortBy, sortMethod);

            var expectedUrl = new Uri(_uri + url);

            try
            {
                await _client.GetCampaignsAsync(page, pageSize, sortBy, sortMethod);
            }
            catch (Exception ex)
            {
                // known serialization exception caused by returning {} from test handler 
            }

            Assert.AreEqual(HttpMethod.Get, _handler.Requests[0].Method);
            Assert.AreEqual(expectedUrl.AbsoluteUri, _handler.Requests[0].RequestUri.AbsoluteUri);
        }

        [Test]
        public async Task Given_MoosendApiClient_When_Getting_All_Campaigns_Then_The_Headers_Are_As_Expected()
        {
            try
            {
                await _client.GetCampaignsAsync();
            }
            catch (Exception ex)
            {
                // known serialization exception caused by returning {} from test handler 
            }

            IEnumerable<string> headers;
            _handler.Requests[0].Headers.TryGetValues("Keep-Alive", out headers);

            Assert.AreEqual("application/json", _handler.Requests[0].Headers.Accept.FirstOrDefault().MediaType);
            Assert.AreEqual(string.Format("moosend-api-client-{0}-{1}", Environment.Version, Environment.OSVersion), _handler.Requests[0].Headers.UserAgent.ToString());
            Assert.AreEqual("false", headers.Single());

        }

        [Test]
        public async Task Given_MoosendApiClient_When_Getting_All_Campaigns_Then_Can_Get_Campaigns()
        {
            // arrange
            const int page = 2;
            const int pageSize = 11;
            const string sortBy = "Name";
            const string sortMethod = "DESC";
            var id = new Guid();

            var ctx = new ServiceClientContext(_uri);
            var campaigns = new CampaignsResult();

            var expectedContent = new ApiResponse<CampaignsResult>() { Context = campaigns };
            expectedContent.Context.Paging = new Paging() { TotalResults = 1 };
            expectedContent.Context.Campaigns = new List<CampaignSummary>() { new CampaignSummary() { Id = id } };

            ctx.Handler = new TestHttpMessageHandler(HttpStatusCode.OK, expectedContent);
            var client = new MoosendApiClient(_apiKey, ctx);

            // act
            var campaignsResponse = await client.GetCampaignsAsync(page, pageSize, sortBy, sortMethod);

            // assert
            Assert.AreEqual(expectedContent.Context.Paging.TotalResults, campaignsResponse.Paging.TotalResults);
            Assert.AreEqual(expectedContent.Context.Campaigns.Count, campaignsResponse.Campaigns.Count);
            Assert.AreEqual(expectedContent.Context.Campaigns.Single().Id, campaignsResponse.Campaigns.Single().Id);
        }

        [Test]
        public async Task Given_MoosendApiClient_When_Getting_A_Sender_Then_The_Right_Url_Is_Accessed()
        {
            const string email = "email@test.com";
            var url = string.Format("/senders/find_one.json?apikey={0}&Email={1}", _apiKey, email);

            var expectedUrl = new Uri(_uri + url);

            try
            {
                await _client.GetSenderByEmailAsync(email);
            }
            catch (Exception ex)
            {
                // known serialization exception caused by returning {} from test handler 
            }

            Assert.AreEqual(HttpMethod.Get, _handler.Requests[0].Method);
            Assert.AreEqual(expectedUrl.AbsoluteUri, HttpUtility.UrlDecode(_handler.Requests[0].RequestUri.AbsoluteUri));
        }

        [Test]
        public async Task Given_MoosendApiClient_When_Getting_A_Sender_Then_The_Headers_Are_As_Expected()
        {
            try
            {
                await _client.GetSenderByEmailAsync("email@test.com");
            }
            catch (Exception ex)
            {
                // known serialization exception caused by returning {} from test handler 
            }

            IEnumerable<string> headers;
            _handler.Requests[0].Headers.TryGetValues("Keep-Alive", out headers);

            Assert.AreEqual("application/json", _handler.Requests[0].Headers.Accept.FirstOrDefault().MediaType);
            Assert.AreEqual(string.Format("moosend-api-client-{0}-{1}", Environment.Version, Environment.OSVersion), _handler.Requests[0].Headers.UserAgent.ToString());
            Assert.AreEqual("false", headers.Single());

        }

        [Test]
        public async Task Given_MoosendApiClient_When_Getting_A_Sender_Then_Can_Get_Sender()
        {
            // arrange
            const string email = "email@gmail.com";

            var ctx = new ServiceClientContext(_uri);
            var id = new Guid();
            var sender = new Sender();

            var expectedContent = new ApiResponse<Sender>() { Context = sender };
            expectedContent.Context.Id = id;

            ctx.Handler = new TestHttpMessageHandler(HttpStatusCode.OK, expectedContent);
            var client = new MoosendApiClient(_apiKey, ctx);

            // act
            var senderResult = await client.GetSenderByEmailAsync(email);

            // assert
            Assert.AreEqual(expectedContent.Context.Id, senderResult.Id);
        }

        [Test]
        public async Task Given_MoosendApiClient_When_Getting_Senders_Then_The_Right_Url_Is_Accessed()
        {
            var url = string.Format("/senders/find_all.json?apikey={0}", _apiKey);

            var expectedUrl = new Uri(_uri + url);

            try
            {
                await _client.GetSendersAsync();
            }
            catch (Exception ex)
            {
                // known serialization exception caused by returning {} from test handler 
            }

            Assert.AreEqual(HttpMethod.Get, _handler.Requests[0].Method);
            Assert.AreEqual(expectedUrl.AbsoluteUri, _handler.Requests[0].RequestUri.AbsoluteUri);
        }

        [Test]
        public async Task Given_MoosendApiClient_When_Getting_Senders_Then_The_Headers_Are_As_Expected()
        {
            try
            {
                await _client.GetSenderByEmailAsync("email@test.com");
            }
            catch (Exception ex)
            {
                // known serialization exception caused by returning {} from test handler 
            }

            IEnumerable<string> headers;
            _handler.Requests[0].Headers.TryGetValues("Keep-Alive", out headers);

            Assert.AreEqual("application/json", _handler.Requests[0].Headers.Accept.FirstOrDefault().MediaType);
            Assert.AreEqual(string.Format("moosend-api-client-{0}-{1}", Environment.Version, Environment.OSVersion), _handler.Requests[0].Headers.UserAgent.ToString());
            Assert.AreEqual("false", headers.Single());
        }

        [Test]
        public async Task Given_MoosendApiClient_When_Getting_Senders_Then_Can_Get_Senders()
        {
            // arrange
            var ctx = new ServiceClientContext(_uri);
            var id = new Guid();
            var sender = new Sender();

            var expectedContent = new ApiResponse<IList<Sender>>() { Context = new List<Sender>() { sender } };
            expectedContent.Context.Single().Id = id;

            ctx.Handler = new TestHttpMessageHandler(HttpStatusCode.OK, expectedContent);
            var client = new MoosendApiClient(_apiKey, ctx);

            // act
            var sendersResult = await client.GetSendersAsync();

            // assert
            Assert.AreEqual(expectedContent.Context.Count, sendersResult.Count);
            Assert.AreEqual(expectedContent.Context.Single().Id, sendersResult.Single().Id);
        }

        [Test]
        public async Task Given_MoosendApiClient_When_Creating_Campaign_Then_The_Right_Url_Is_Accessed()
        {
            var url = string.Format("/campaigns/create.json?apikey={0}", _apiKey);

            var expectedUrl = new Uri(_uri + url);

            try
            {
                await _client.CreateCampaignAsync(new CampaignParams());
            }
            catch (Exception ex)
            {
                // known serialization exception caused by returning {} from test handler 
            }

            Assert.AreEqual(HttpMethod.Post, _handler.Requests[0].Method);
            Assert.AreEqual(expectedUrl.AbsoluteUri, _handler.Requests[0].RequestUri.AbsoluteUri);
        }

        [Test]
        public async Task Given_MoosendApiClient_When_Creating_Campaign_Then_The_Headers_Are_As_Expected()
        {
            try
            {
                await _client.CreateCampaignAsync(new CampaignParams());
            }
            catch (Exception ex)
            {
                // known serialization exception caused by returning {} from test handler 
            }

            IEnumerable<string> headers;
            _handler.Requests[0].Headers.TryGetValues("Keep-Alive", out headers);

            Assert.AreEqual("application/json", _handler.Requests[0].Headers.Accept.FirstOrDefault().MediaType);
            Assert.AreEqual(string.Format("moosend-api-client-{0}-{1}", Environment.Version, Environment.OSVersion), _handler.Requests[0].Headers.UserAgent.ToString());
            Assert.AreEqual("false", headers.Single());
        }

        [Test]
        public async Task Given_MoosendApiClient_When_Creating_Campaign_Then_Can_Create()
        {
            // arrange
            var id = new Guid();
            var campaignParams = new CampaignParams();

            var expectedContent = new ApiResponse<Guid>() { Context = id };
            var ctx = new ServiceClientContext(_uri);
            ctx.Handler = new TestHttpMessageHandler(HttpStatusCode.OK, expectedContent);
            var client = new MoosendApiClient(_apiKey, ctx);

            // act
            var cmId = await client.CreateCampaignAsync(campaignParams);

            // assert
            Assert.AreEqual(expectedContent.Context, cmId);
        }

        [Test]
        public async Task Given_MoosendApiClient_When_Creating_Campaign_Then_The_Payload_Is_As_Expected()
        {
            var campaignParams = new CampaignParams() { Name = "campaign_name", SenderEmail = "email@test.com" };

            try
            {
                await _client.CreateCampaignAsync(campaignParams);

            }
            catch (Exception ex)
            {
                // known serialization exception caused by returning {} from test handler 
            }

            var expectedJson = JsonConvert.SerializeObject(campaignParams);

            Assert.That(_handler.Payloads[0].Contains(expectedJson));
        }

        [Test]
        public async Task Given_MoosendApiClient_When_Sending_Test_Then_The_Right_Url_Is_Accessed()
        {
            var cmId = new Guid();
            var url = string.Format("/campaigns/{0}/send_test.json?apikey={1}", cmId, _apiKey);

            var expectedUrl = new Uri(_uri + url);

            try
            {
                await _client.SendTestAsync(cmId, new List<string>());
            }
            catch (Exception ex)
            {
                // known serialization exception caused by returning {} from test handler 
            }

            Assert.AreEqual(HttpMethod.Post, _handler.Requests[0].Method);
            Assert.AreEqual(expectedUrl.AbsoluteUri, _handler.Requests[0].RequestUri.AbsoluteUri);
        }

        [Test]
        public async Task Given_MoosendApiClient_When_Sending_Test_Then_The_Headers_Are_As_Expected()
        {
            try
            {
                await _client.SendTestAsync(new Guid(), new List<string>());
            }
            catch (Exception ex)
            {
                // known serialization exception caused by returning {} from test handler 
            }

            IEnumerable<string> headers;
            _handler.Requests[0].Headers.TryGetValues("Keep-Alive", out headers);

            Assert.AreEqual("application/json", _handler.Requests[0].Headers.Accept.FirstOrDefault().MediaType);
            Assert.AreEqual(string.Format("moosend-api-client-{0}-{1}", Environment.Version, Environment.OSVersion), _handler.Requests[0].Headers.UserAgent.ToString());
            Assert.AreEqual("false", headers.Single());

        }

        [Test]
        public async Task Given_MoosendApiClient_When_Sending_Test_Then_Can_Send()
        {
            // arrange
            var campaignParams = new CampaignParams();

            var expectedContent = new ApiResponse<bool>() { Context = true };
            var ctx = new ServiceClientContext(_uri);
            ctx.Handler = new TestHttpMessageHandler(HttpStatusCode.OK, expectedContent);
            var client = new MoosendApiClient(_apiKey, ctx);

            // act
            var success = await client.SendTestAsync(new Guid(), new List<string>());

            // assert
            Assert.That(success, Is.True);
        }

        [Test]
        public async Task Given_MoosendApiClient_When_Sending_Test_Then_The_Payload_Is_As_Expected()
        {
            var emails = new List<string>();

            try
            {
                await _client.SendTestAsync(new Guid(), emails);

            }
            catch (Exception ex)
            {
                // known serialization exception caused by returning {} from test handler 
            }

            var expectedJson = JsonConvert.SerializeObject(new { TestEmails = emails });

            Assert.That(_handler.Payloads[0].Contains(expectedJson));
        }

        [Test]
        public async Task Given_MoosendApiClient_When_Sending_Campaign_Then_The_Right_Url_Is_Accessed()
        {
            var url = string.Format("/campaigns/{0}/send.json?apikey={1}", new Guid(), _apiKey);

            var expectedUrl = new Uri(_uri + url);

            try
            {
                await _client.SendCampaignAsync(new Guid());
            }
            catch (Exception ex)
            {
                // known serialization exception caused by returning {} from test handler 
            }

            Assert.That(HttpMethod.Post, Is.EqualTo(_handler.Requests[0].Method));
            Assert.That(expectedUrl.AbsoluteUri, Is.EqualTo(_handler.Requests[0].RequestUri.AbsoluteUri));
        }

        [Test]
        public async Task Given_MoosendApiClient_When_Sending_Campaign_Then_The_Headers_Are_As_Expected()
        {
            try
            {
                await _client.SendCampaignAsync(new Guid());
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
        public async Task Given_MoosendApiClient_When_Sending_Campaign_Then_Can_Send()
        {
            // arrange
            var expectedContent = new ApiResponse<bool>() { Context = true };
            var ctx = new ServiceClientContext(_uri) { Handler = new TestHttpMessageHandler(HttpStatusCode.OK, expectedContent) };
            var client = new MoosendApiClient(_apiKey, ctx);

            // act
            var success = await client.SendTestAsync(new Guid(), new List<string>());

            // assert
            Assert.That(success, Is.True);
        }

        [Test]
        public async Task Given_MoosendApiClient_When_Deleting_Campaign_Then_The_Right_Url_Is_Accessed()
        {
            var url = string.Format("/campaigns/{0}/delete.json?apikey={1}", new Guid(), _apiKey);

            var expectedUrl = new Uri(_uri + url);

            try
            {
                await _client.DeleteCampaignAsync(new Guid());
            }
            catch (Exception ex)
            {
                // known serialization exception caused by returning {} from test handler 
            }

            Assert.That(HttpMethod.Delete, Is.EqualTo(_handler.Requests[0].Method));
            Assert.That(expectedUrl.AbsoluteUri, Is.EqualTo(_handler.Requests[0].RequestUri.AbsoluteUri));
        }

        [Test]
        public async Task Given_MoosendApiClient_When_Deleting_Campaign_Then_The_Headers_Are_As_Expected()
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
        public async Task Given_MoosendApiClient_When_Deleting_Campaign_Then_Can_Delete()
        {
            // arrange
            var expectedContent = new ApiResponse<bool>() { Context = true };
            var ctx = new ServiceClientContext(_uri) { Handler = new TestHttpMessageHandler(HttpStatusCode.OK, expectedContent) };
            var client = new MoosendApiClient(_apiKey, ctx);

            // act
            var success = await client.DeleteCampaignAsync(new Guid());

            // assert
            Assert.That(success, Is.True);
        }

        [Test]
        public async Task Given_MoosendApiClient_When_Getting_Campaign_Stats_Then_The_Right_Url_Is_Accessed()
        {
            var url = string.Format("/campaigns/{0}/stats/{1}.json?apikey={2}", new Guid(), MailStatus.Sent, _apiKey);

            var expectedUrl = new Uri(_uri + url);

            try
            {
                await _client.GetCampaignStatisticsAsync(new Guid());
            }
            catch (Exception ex)
            {
                // known serialization exception caused by returning {} from test handler 
            }

            Assert.That(HttpMethod.Get, Is.EqualTo(_handler.Requests[0].Method));
            Assert.That(expectedUrl.AbsoluteUri, Is.EqualTo(_handler.Requests[0].RequestUri.AbsoluteUri));
        }

        [Test]
        public async Task Given_MoosendApiClient_When_Getting_Campaign_Stats_Then_The_Headers_Are_As_Expected()
        {
            try
            {
                await _client.GetCampaignStatisticsAsync(new Guid());
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
        public async Task Given_MoosendApiClient_When_Getting_Campaign_Stats_Then_Can_Get_Stats()
        {
            // arrange
            var id = new Guid();

            var ctx = new ServiceClientContext(_uri);
            var stats = new CampaignsStatisticsResult();

            var expectedContent = new ApiResponse<CampaignsStatisticsResult>() { Context = stats };
            expectedContent.Context.Paging = new Paging() { TotalResults = 1 };
            expectedContent.Context.Analytics = new List<AnalyticsDetails>();
            var item = new AnalyticsDetails() {TotalCount = 5};
            expectedContent.Context.Analytics.Add(item);

            ctx.Handler = new TestHttpMessageHandler(HttpStatusCode.OK, expectedContent);
            var client = new MoosendApiClient(_apiKey, ctx);

            // act
            var response = await client.GetCampaignStatisticsAsync(id);

            // assert
            Assert.AreEqual(expectedContent.Context.Paging.TotalResults, response.Paging.TotalResults);
            Assert.AreEqual(expectedContent.Context.Analytics.Count, response.Analytics.Count);
            Assert.AreEqual(expectedContent.Context.Analytics.Single().TotalCount, response.Analytics.Single().TotalCount);
        }

        [Test]
        public async Task Given_MoosendApiClient_When_Getting_Link_Activity_Then_The_Right_Url_Is_Accessed()
        {
            var url = string.Format("/campaigns/{0}/stats/links.json?apikey={1}", new Guid(), _apiKey);

            var expectedUrl = new Uri(_uri + url);

            try
            {
                await _client.GetCampaignLinkActivityAsync(new Guid());
            }
            catch (Exception ex)
            {
                // known serialization exception caused by returning {} from test handler 
            }

            Assert.That(HttpMethod.Get, Is.EqualTo(_handler.Requests[0].Method));
            Assert.That(expectedUrl.AbsoluteUri, Is.EqualTo(_handler.Requests[0].RequestUri.AbsoluteUri));
        }

        [Test]
        public async Task Given_MoosendApiClient_When_Getting_Link_Activity_Then_The_Headers_Are_As_Expected()
        {
            try
            {
                await _client.GetCampaignLinkActivityAsync(new Guid());
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
        public async Task Given_MoosendApiClient_When_Getting_Link_Activity_Then_Can_Get_Link_Activity()
        {
            // arrange
            var id = new Guid();

            var ctx = new ServiceClientContext(_uri);
            var linkActivity = new CampaignsStatisticsResult();

            var expectedContent = new ApiResponse<CampaignsStatisticsResult>() { Context = linkActivity };
            expectedContent.Context.Paging = new Paging() { TotalResults = 1 };
            expectedContent.Context.Analytics = new List<AnalyticsDetails>();
            var item = new AnalyticsDetails() { TotalCount = 5 };
            expectedContent.Context.Analytics.Add(item);

            ctx.Handler = new TestHttpMessageHandler(HttpStatusCode.OK, expectedContent);
            var client = new MoosendApiClient(_apiKey, ctx);

            // act
            var response = await client.GetCampaignLinkActivityAsync(id);

            // assert
            Assert.AreEqual(expectedContent.Context.Paging.TotalResults, response.Paging.TotalResults);
            Assert.AreEqual(expectedContent.Context.Analytics.Count, response.Analytics.Count);
            Assert.AreEqual(expectedContent.Context.Analytics.Single().TotalCount, response.Analytics.Single().TotalCount);
        }

        [Test]
        public async Task Given_MoosendApiClient_When_Getting_Activity_By_Location__Then_The_Right_Url_Is_Accessed()
        {
            var url = string.Format("/campaigns/{0}/stats/countries.json?apikey={1}", new Guid(), _apiKey);

            var expectedUrl = new Uri(_uri + url);

            try
            {
                await _client.GetCampaignActivityByLocationAsync(new Guid());
            }
            catch (Exception ex)
            {
                // known serialization exception caused by returning {} from test handler 
            }

            Assert.That(HttpMethod.Get, Is.EqualTo(_handler.Requests[0].Method));
            Assert.That(expectedUrl.AbsoluteUri, Is.EqualTo(_handler.Requests[0].RequestUri.AbsoluteUri));
        }

        [Test]
        public async Task Given_MoosendApiClient_When_Getting_Activity_By_Location_Then_The_Headers_Are_As_Expected()
        {
            try
            {
                await _client.GetCampaignActivityByLocationAsync(new Guid());
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
        public async Task Given_MoosendApiClient_When_Getting_Activity_By_Location_Then_Can_Get_Activity_By_Location_()
        {
            // arrange
            var id = new Guid();

            var ctx = new ServiceClientContext(_uri);
            var linkActivity = new CampaignsStatisticsResult();

            var expectedContent = new ApiResponse<CampaignsStatisticsResult>() { Context = linkActivity };
            expectedContent.Context.Paging = new Paging() { TotalResults = 1 };
            expectedContent.Context.Analytics = new List<AnalyticsDetails>();
            var item = new AnalyticsDetails() { TotalCount = 5 };
            expectedContent.Context.Analytics.Add(item);

            ctx.Handler = new TestHttpMessageHandler(HttpStatusCode.OK, expectedContent);
            var client = new MoosendApiClient(_apiKey, ctx);

            // act
            var response = await client.GetCampaignActivityByLocationAsync(id);

            // assert
            Assert.AreEqual(expectedContent.Context.Paging.TotalResults, response.Paging.TotalResults);
            Assert.AreEqual(expectedContent.Context.Analytics.Count, response.Analytics.Count);
            Assert.AreEqual(expectedContent.Context.Analytics.Single().TotalCount, response.Analytics.Single().TotalCount);
        }

        [Test]
        public async Task Given_MoosendApiClient_When_Updating_Campaign_Then_The_Right_Url_Is_Accessed()
        {
            var url = string.Format("/campaigns/{0}/update.json?apikey={1}", new Guid(), _apiKey);

            var expectedUrl = new Uri(_uri + url);

            try
            {
                await _client.UpdateCampaignAsync(new Guid(),new CampaignParams());
            }
            catch (Exception ex)
            {
                // known serialization exception caused by returning {} from test handler 
            }

            Assert.That(HttpMethod.Post, Is.EqualTo(_handler.Requests[0].Method));
            Assert.That(expectedUrl.AbsoluteUri, Is.EqualTo(_handler.Requests[0].RequestUri.AbsoluteUri));
        }

        [Test]
        public async Task Given_MoosendApiClient_When_Updating_Campaign_Then_The_Headers_Are_As_Expected()
        {
            try
            {
                await _client.UpdateCampaignAsync(new Guid(), new CampaignParams());
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
        public async Task Given_MoosendApiClient_When_Updating_Campaign_Then_Can_Update()
        {
            // arrange
            var id = new Guid();

            var ctx = new ServiceClientContext(_uri);
            var campaignParams = new CampaignParams() {Name = "new_name"};
            var expectedContent = new ApiResponse<bool>() { Context = true };

            ctx.Handler = new TestHttpMessageHandler(HttpStatusCode.OK, expectedContent);
            var client = new MoosendApiClient(_apiKey, ctx);

            // act
            var success = await client.UpdateCampaignAsync(id, campaignParams);

            // assert
            Assert.That(success, Is.True);
        }

        [Test]
        public async Task Given_MoosendApiClient_When_Updating_Campaign_Then_The_Payload_Is_As_Expected()
        {
            var campaignParams = new CampaignParams() { Name = "campaign_name", SenderEmail = "email@test.com" };

            try
            {
                await _client.UpdateCampaignAsync(new Guid(),  campaignParams);

            }
            catch (Exception ex)
            {
                // known serialization exception caused by returning {} from test handler 
            }

            var expectedJson = JsonConvert.SerializeObject(campaignParams);

            Assert.That(_handler.Payloads[0].Contains(expectedJson));
        }

        #endregion 
    }
}