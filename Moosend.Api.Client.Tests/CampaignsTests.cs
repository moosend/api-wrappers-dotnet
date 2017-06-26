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
    public class CampaignsTests
    {
        private Uri _uri;
        private Guid _apiKey;
        private IMoosendApiClient _client;
        private TestHttpMessageHandler _handler;

        [SetUp]
        public void Setup()
        {
            _uri = new Uri("https://api.moosend.com/v3");
            _apiKey = Guid.NewGuid();
            _handler = new TestHttpMessageHandler();

            var ctx = new ServiceClientContext(_uri) {Handler = _handler};
            _client = new MoosendApiClient(_apiKey, ctx);
        }

        [Test]
        public async Task Given_MoosendApiClient_When_Getting_All_Campaigns_Then_The_Right_Url_Is_Accessed()
        {
            var page = 2;
            var pageSize = 11;
            var sortBy = "Name";
            var sortMethod = "DESC";
            var url = string.Format("/campaigns/{0}/{1}.json?apikey={2}&SortBy={3}&SortMethod={4}", page, pageSize,
                _apiKey, sortBy, sortMethod);

            var expectedUrl = new Uri(_uri + url);

            try
            {
                await _client.GetCampaignsAsync(page, pageSize, sortBy, sortMethod);
            }
            catch (Exception ex)
            {
                // known serialization exception caused by returning {} from test handler 
            }

            Assert.That(_handler.Requests[0].Method, Is.EqualTo(HttpMethod.Get));
            Assert.That(_handler.Requests[0].RequestUri.AbsoluteUri, Is.EqualTo(expectedUrl.AbsoluteUri));
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

            Assert.That(_handler.Requests[0].Headers.Accept.FirstOrDefault().MediaType, Is.EqualTo("application/json"));
            Assert.That(_handler.Requests[0].Headers.UserAgent.ToString(),
                Is.EqualTo(string.Format("moosend-api-client-{0}-{1}", Environment.Version, Environment.OSVersion)));
        }

        [Test]
        public async Task Given_MoosendApiClient_When_Getting_All_Campaigns_Then_Can_Get_Campaigns()
        {
            // arrange
            var cmId = new Guid();
            var ctx = new ServiceClientContext(_uri);

            var expectedCampaigns = new CampaignsResponse
            {
                Paging = new Paging {TotalPageCount = 1},
                Campaigns = new List<CampaignSummary> {new CampaignSummary {Id = cmId}}
            };

            var content = new ApiResponse<CampaignsResponse> {Context = expectedCampaigns};
            ctx.Handler = new TestHttpMessageHandler(HttpStatusCode.OK, content);

            var client = new MoosendApiClient(_apiKey, ctx);

            // act
            var campaignsResult = await client.GetCampaignsAsync();

            // assert
            Assert.That(campaignsResult.Paging.TotalPageCount, Is.EqualTo(expectedCampaigns.Paging.TotalPageCount));
            Assert.That(campaignsResult.Campaigns.Count, Is.EqualTo(expectedCampaigns.Campaigns.Count()));
            Assert.That(campaignsResult.Campaigns.Single().Id, Is.EqualTo(campaignsResult.Campaigns.Single().Id));
        }

        [Test]
        public async Task Given_MoosendApiClient_When_Getting_A_Campaign_Then_The_Right_Url_Is_Accessed()
        {
            var cId = Guid.NewGuid();
            var url = string.Format("/campaigns/{0}/view.json?apikey={1}", cId, _apiKey);

            var expectedUrl = new Uri(_uri + url);

            try
            {
                await _client.GetCampaignAsync(cId);
            }
            catch (Exception ex)
            {
                // known serialization exception caused by returning {} from test handler 
            }

            Assert.That(_handler.Requests[0].Method, Is.EqualTo(HttpMethod.Get));
            Assert.That(_handler.Requests[0].RequestUri.AbsoluteUri, Is.EqualTo(expectedUrl.AbsoluteUri));
        }

        [Test]
        public async Task Given_MoosendApiClient_When_Getting_A_Campaign_Then_The_Headers_Are_As_Expected()
        {
            try
            {
                await _client.GetCampaignAsync(Guid.NewGuid());
            }
            catch (Exception ex)
            {
                // known serialization exception caused by returning {} from test handler 
            }

            Assert.That(_handler.Requests[0].Headers.Accept.FirstOrDefault().MediaType, Is.EqualTo("application/json"));
            Assert.That(_handler.Requests[0].Headers.UserAgent.ToString(),
                Is.EqualTo(string.Format("moosend-api-client-{0}-{1}", Environment.Version, Environment.OSVersion)));
        }

        [Test]
        public async Task Given_MoosendApiClient_When_Getting_A_Campaign_Then_Can_Get_Campaigns()
        {
            // arrange
            var ctx = new ServiceClientContext(_uri);

            var expectedCampaign = new Campaign();

            var content = new ApiResponse<Campaign> { Context = expectedCampaign };
            ctx.Handler = new TestHttpMessageHandler(HttpStatusCode.OK, content);

            var client = new MoosendApiClient(_apiKey, ctx);

            // act
            var campaign = await client.GetCampaignAsync(Guid.NewGuid());

            // assert
            Assert.AreEqual(expectedCampaign.Id, campaign.Id);
        }

        [Test]
        public async Task Given_MoosendApiClient_When_Cloning_A_Campaign_Then_The_Right_Url_Is_Accessed()
        {
            var cId = Guid.NewGuid();
            var url = string.Format("/campaigns/{0}/clone.json?apikey={1}", cId, _apiKey);

            var expectedUrl = new Uri(_uri + url);

            try
            {
                await _client.CloneAsync(cId);
            }
            catch (Exception ex)
            {
                // known serialization exception caused by returning {} from test handler 
            }

            Assert.That(_handler.Requests[0].Method, Is.EqualTo(HttpMethod.Post));
            Assert.That(_handler.Requests[0].RequestUri.AbsoluteUri, Is.EqualTo(expectedUrl.AbsoluteUri));
        }

        [Test]
        public async Task Given_MoosendApiClient_When_Cloning_A_Campaign_Then_The_Headers_Are_As_Expected()
        {
            try
            {
                await _client.CloneAsync(Guid.NewGuid());
            }
            catch (Exception ex)
            {
                // known serialization exception caused by returning {} from test handler 
            }

            Assert.That(_handler.Requests[0].Headers.Accept.FirstOrDefault().MediaType, Is.EqualTo("application/json"));
            Assert.That(_handler.Requests[0].Headers.UserAgent.ToString(),
                Is.EqualTo(string.Format("moosend-api-client-{0}-{1}", Environment.Version, Environment.OSVersion)));
        }

        [Test]
        public async Task Given_MoosendApiClient_When_Cloning_A_Campaign_Then_Can_Get_Campaigns()
        {
            // arrange
            var ctx = new ServiceClientContext(_uri);

            var expectedCampaign = new Campaign();

            var content = new ApiResponse<Campaign> { Context = expectedCampaign };
            ctx.Handler = new TestHttpMessageHandler(HttpStatusCode.OK, content);

            var client = new MoosendApiClient(_apiKey, ctx);

            // act
            var campaign = await client.CloneAsync(Guid.NewGuid());

            // assert
            Assert.AreEqual(expectedCampaign.Id, campaign.Id);
        }

        [Test]
        public async Task Given_MoosendApiClient_When_Getting_Campaign_Then_The_Right_Url_Is_Accessed()
        {
            var cId = Guid.NewGuid();
            var url = string.Format("/campaigns/{0}/view_summary.json?apikey={1}", cId, _apiKey);

            var expectedUrl = new Uri(_uri + url);

            try
            {
                await _client.GetCampaignSummaryAsync(cId);
            }
            catch (Exception ex)
            {
                // known serialization exception caused by returning {} from test handler 
            }

            Assert.That(_handler.Requests[0].Method, Is.EqualTo(HttpMethod.Get));
            Assert.That(_handler.Requests[0].RequestUri.AbsoluteUri, Is.EqualTo(expectedUrl.AbsoluteUri));
        }

        [Test]
        public async Task Given_MoosendApiClient_When_Getting_Campaign_Then_The_Headers_Are_As_Expected()
        {
            try
            {
                await _client.GetCampaignSummaryAsync(Guid.NewGuid());
            }
            catch (Exception ex)
            {
                // known serialization exception caused by returning {} from test handler 
            }

            Assert.That(_handler.Requests[0].Headers.Accept.FirstOrDefault().MediaType, Is.EqualTo("application/json"));
            Assert.That(_handler.Requests[0].Headers.UserAgent.ToString(),
                Is.EqualTo(string.Format("moosend-api-client-{0}-{1}", Environment.Version, Environment.OSVersion)));
        }

        [Test]
        public async Task Given_MoosendApiClient_When_Getting_Campaign_Summary_Then_Can_Get_Campaigns()
        {
            // arrange
            var ctx = new ServiceClientContext(_uri);

            var expectedCampaign = new CampaignStatisticsSummary() {CampaignId = Guid.NewGuid()};

            var content = new ApiResponse<CampaignStatisticsSummary> { Context = expectedCampaign };
            ctx.Handler = new TestHttpMessageHandler(HttpStatusCode.OK, content);

            var client = new MoosendApiClient(_apiKey, ctx);

            // act
            var campaign = await client.GetCampaignSummaryAsync(Guid.NewGuid());

            // assert
            Assert.AreEqual(expectedCampaign.CampaignId, campaign.CampaignId);
        }

        [Test]
        public async Task Given_MoosendApiClient_When_Getting_A_Sender_Then_The_Right_Url_Is_Accessed()
        {
            var email = "email@test.com";
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

            Assert.That(_handler.Requests[0].Method, Is.EqualTo(HttpMethod.Get));
            Assert.That(HttpUtility.UrlDecode(_handler.Requests[0].RequestUri.AbsoluteUri),
                Is.EqualTo(expectedUrl.AbsoluteUri));
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

            Assert.That(_handler.Requests[0].Headers.Accept.FirstOrDefault().MediaType, Is.EqualTo("application/json"));
            Assert.That(_handler.Requests[0].Headers.UserAgent.ToString(),
                Is.EqualTo(string.Format("moosend-api-client-{0}-{1}", Environment.Version, Environment.OSVersion)));
        }

        [Test]
        public async Task Given_MoosendApiClient_When_Getting_A_Sender_Then_Can_Get_Sender()
        {
            // arrange
            var email = "email@gmail.com";
            var cmId = new Guid();
            var ctx = new ServiceClientContext(_uri);

            var expectedSender = new Sender {Id = cmId};

            var content = new ApiResponse<Sender> {Context = expectedSender};
            ctx.Handler = new TestHttpMessageHandler(HttpStatusCode.OK, content);

            var client = new MoosendApiClient(_apiKey, ctx);

            // act
            var senderResult = await client.GetSenderByEmailAsync(email);

            // assert
            Assert.That(senderResult.Id, Is.EqualTo(expectedSender.Id));
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

            Assert.That(_handler.Requests[0].Method, Is.EqualTo(HttpMethod.Get));
            Assert.That(HttpUtility.UrlDecode(_handler.Requests[0].RequestUri.AbsoluteUri),
                Is.EqualTo(expectedUrl.AbsoluteUri));
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

            Assert.That(_handler.Requests[0].Headers.Accept.FirstOrDefault().MediaType, Is.EqualTo("application/json"));
            Assert.That(_handler.Requests[0].Headers.UserAgent.ToString(),
                Is.EqualTo(string.Format("moosend-api-client-{0}-{1}", Environment.Version, Environment.OSVersion)));
        }

        [Test]
        public async Task Given_MoosendApiClient_When_Getting_Senders_Then_Can_Get_Senders()
        {
            // arrange
            var senderId = new Guid();
            var ctx = new ServiceClientContext(_uri);

            var expectedSenders = new List<Sender> {new Sender {Id = senderId}};

            var content = new ApiResponse<IList<Sender>> {Context = expectedSenders};
            ctx.Handler = new TestHttpMessageHandler(HttpStatusCode.OK, content);

            var client = new MoosendApiClient(_apiKey, ctx);

            // act
            var sendersResult = await client.GetSendersAsync();

            // assert
            Assert.That(sendersResult.Count, Is.EqualTo(expectedSenders.Count));
            Assert.That(sendersResult.Single().Id, Is.EqualTo(expectedSenders.Single().Id));
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

            Assert.That(_handler.Requests[0].Method, Is.EqualTo(HttpMethod.Post));
            Assert.That(HttpUtility.UrlDecode(_handler.Requests[0].RequestUri.AbsoluteUri),
                Is.EqualTo(expectedUrl.AbsoluteUri));
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

            Assert.That(_handler.Requests[0].Headers.Accept.FirstOrDefault().MediaType, Is.EqualTo("application/json"));
            Assert.That(_handler.Requests[0].Headers.UserAgent.ToString(),
                Is.EqualTo(string.Format("moosend-api-client-{0}-{1}", Environment.Version, Environment.OSVersion)));
        }

        [Test]
        public async Task Given_MoosendApiClient_When_Creating_Campaign_Then_Can_Create()
        {
            // arrange
            var ctx = new ServiceClientContext(_uri);

            var expectedId = new Guid();

            var content = new ApiResponse<Guid> {Context = expectedId};
            ctx.Handler = new TestHttpMessageHandler(HttpStatusCode.OK, content);

            var client = new MoosendApiClient(_apiKey, ctx);

            // act
            var cmId = await client.CreateCampaignAsync(new CampaignParams());

            // assert
            Assert.That(cmId, Is.EqualTo(expectedId));
        }

        [Test]
        public async Task Given_MoosendApiClient_When_Creating_Campaign_Then_The_Payload_Is_As_Expected()
        {
            var campaignParams = new CampaignParams {Name = "campaign_name", SenderEmail = "email@test.com"};

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

            Assert.That(_handler.Requests[0].Method, Is.EqualTo(HttpMethod.Post));
            Assert.That(HttpUtility.UrlDecode(_handler.Requests[0].RequestUri.AbsoluteUri),
                Is.EqualTo(expectedUrl.AbsoluteUri));
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

            Assert.That(_handler.Requests[0].Headers.Accept.FirstOrDefault().MediaType, Is.EqualTo("application/json"));
            Assert.That(_handler.Requests[0].Headers.UserAgent.ToString(),
                Is.EqualTo(string.Format("moosend-api-client-{0}-{1}", Environment.Version, Environment.OSVersion)));
        }

        [Test]
        public async Task Given_MoosendApiClient_When_Sending_Test_Then_Can_Send()
        {
            // arrange
            var ctx = new ServiceClientContext(_uri);

            var content = new ApiResponse<bool> {Context = true};

            ctx.Handler = new TestHttpMessageHandler(HttpStatusCode.OK, content);
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

            var expectedJson = JsonConvert.SerializeObject(new {TestEmails = emails});

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

            Assert.That(_handler.Requests[0].Headers.Accept.FirstOrDefault().MediaType, Is.EqualTo("application/json"));
            Assert.That(_handler.Requests[0].Headers.UserAgent.ToString(),
                Is.EqualTo(string.Format("moosend-api-client-{0}-{1}", Environment.Version, Environment.OSVersion)));
        }

        [Test]
        public async Task Given_MoosendApiClient_When_Sending_Campaign_Then_Can_Send()
        {
            // arrange
            var ctx = new ServiceClientContext(_uri);

            var content = new ApiResponse<bool> {Context = true};

            ctx.Handler = new TestHttpMessageHandler(HttpStatusCode.OK, content);
            var client = new MoosendApiClient(_apiKey, ctx);

            // act
            var success = await client.SendCampaignAsync(new Guid());

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

            Assert.That(_handler.Requests[0].Headers.Accept.FirstOrDefault().MediaType, Is.EqualTo("application/json"));
            Assert.That(_handler.Requests[0].Headers.UserAgent.ToString(),
                Is.EqualTo(string.Format("moosend-api-client-{0}-{1}", Environment.Version, Environment.OSVersion)));
        }

        [Test]
        public async Task Given_MoosendApiClient_When_Deleting_Campaign_Then_Can_Delete()
        {
            // arrange
            var ctx = new ServiceClientContext(_uri);

            var content = new ApiResponse<bool> {Context = true};

            ctx.Handler = new TestHttpMessageHandler(HttpStatusCode.OK, content);
            var client = new MoosendApiClient(_apiKey, ctx);

            // act
            var success = await client.DeleteCampaignAsync(new Guid());

            // assert
            Assert.That(success, Is.True);
        }

        [Test]
        public async Task Given_MoosendApiClient_When_Getting_Campaign_Stats_Then_The_Right_Url_Is_Accessed()
        {
            var cmId = new Guid();
            var status = MailStatus.Error;
            var page = 1;
            var pageSize = 11;

            var url = string.Format("/campaigns/{0}/stats/{1}.json?apikey={2}&Page={3}&PageSize={4}", cmId, status,
                _apiKey, page, pageSize);

            var expectedUrl = new Uri(_uri + url);

            try
            {
                await _client.GetCampaignStatisticsAsync(cmId, status, page, pageSize);
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

            Assert.That(_handler.Requests[0].Headers.Accept.FirstOrDefault().MediaType, Is.EqualTo("application/json"));
            Assert.That(_handler.Requests[0].Headers.UserAgent.ToString(),
                Is.EqualTo(string.Format("moosend-api-client-{0}-{1}", Environment.Version, Environment.OSVersion)));
        }

        [Test]
        public async Task Given_MoosendApiClient_When_Getting_Campaign_Stats_Then_Can_Get_Stats()
        {
            // arrange
            var ctx = new ServiceClientContext(_uri);

            var expectedStats = new CampaignsStatisticsResult
            {
                Analytics = new List<AnalyticsDetails> {new AnalyticsDetails {TotalCount = 5}},
                Paging = new Paging {TotalResults = 1}
            };

            var content = new ApiResponse<CampaignsStatisticsResult> {Context = expectedStats};
            ctx.Handler = new TestHttpMessageHandler(HttpStatusCode.OK, content);

            var client = new MoosendApiClient(_apiKey, ctx);

            // act
            var statsResult = await client.GetCampaignStatisticsAsync(new Guid());

            // assert
            Assert.That(statsResult.Paging.TotalPageCount, Is.EqualTo(expectedStats.Paging.TotalPageCount));
            Assert.That(statsResult.Analytics.Count, Is.EqualTo(expectedStats.Analytics.Count()));
            Assert.That(statsResult.Analytics.Single().TotalCount,
                Is.EqualTo(expectedStats.Analytics.Single().TotalCount));
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

            Assert.That(_handler.Requests[0].Headers.Accept.FirstOrDefault().MediaType, Is.EqualTo("application/json"));
            Assert.That(_handler.Requests[0].Headers.UserAgent.ToString(),
                Is.EqualTo(string.Format("moosend-api-client-{0}-{1}", Environment.Version, Environment.OSVersion)));
        }

        [Test]
        public async Task Given_MoosendApiClient_When_Getting_Link_Activity_Then_Can_Get_Link_Activity()
        {
            // arrange
            var ctx = new ServiceClientContext(_uri);

            var expectedLinkActivity = new CampaignsStatisticsResult
            {
                Analytics = new List<AnalyticsDetails> {new AnalyticsDetails {TotalCount = 5}},
                Paging = new Paging {TotalResults = 1}
            };

            var content = new ApiResponse<CampaignsStatisticsResult> {Context = expectedLinkActivity};
            ctx.Handler = new TestHttpMessageHandler(HttpStatusCode.OK, content);

            var client = new MoosendApiClient(_apiKey, ctx);

            // act
            var linkActivityResult = await client.GetCampaignLinkActivityAsync(new Guid());

            // assert
            Assert.That(linkActivityResult.Paging.TotalPageCount, Is.EqualTo(expectedLinkActivity.Paging.TotalPageCount));
            Assert.That(linkActivityResult.Analytics.Count, Is.EqualTo(expectedLinkActivity.Analytics.Count()));
            Assert.That(linkActivityResult.Analytics.Single().TotalCount,
                Is.EqualTo(expectedLinkActivity.Analytics.Single().TotalCount));
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

            Assert.That(_handler.Requests[0].Headers.Accept.FirstOrDefault().MediaType, Is.EqualTo("application/json"));
            Assert.That(_handler.Requests[0].Headers.UserAgent.ToString(),
                Is.EqualTo(string.Format("moosend-api-client-{0}-{1}", Environment.Version, Environment.OSVersion)));
        }

        [Test]
        public async Task Given_MoosendApiClient_When_Getting_Activity_By_Location_Then_Can_Get_Activity_By_Location_()
        {
            // arrange
            var ctx = new ServiceClientContext(_uri);

            var expectedActivityByLocation = new CampaignsStatisticsResult
            {
                Analytics = new List<AnalyticsDetails> {new AnalyticsDetails {TotalCount = 5}},
                Paging = new Paging {TotalResults = 1}
            };

            var content = new ApiResponse<CampaignsStatisticsResult> {Context = expectedActivityByLocation};

            ctx.Handler = new TestHttpMessageHandler(HttpStatusCode.OK, content);
            var client = new MoosendApiClient(_apiKey, ctx);

            // act
            var ctivityByLocationResult = await client.GetCampaignActivityByLocationAsync(new Guid());

            // assert
            Assert.AreEqual(ctivityByLocationResult.Paging.TotalResults, expectedActivityByLocation.Paging.TotalResults);
            Assert.AreEqual(ctivityByLocationResult.Analytics.Count(), expectedActivityByLocation.Analytics.Count());
            Assert.AreEqual(ctivityByLocationResult.Analytics.Single().TotalCount,
                expectedActivityByLocation.Analytics.Single().TotalCount);
        }

        [Test]
        public async Task Given_MoosendApiClient_When_Updating_Campaign_Then_The_Right_Url_Is_Accessed()
        {
            var url = string.Format("/campaigns/{0}/update.json?apikey={1}", new Guid(), _apiKey);

            var expectedUrl = new Uri(_uri + url);

            try
            {
                await _client.UpdateCampaignAsync(new Guid(), new CampaignParams());
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

            Assert.That(_handler.Requests[0].Headers.Accept.FirstOrDefault().MediaType, Is.EqualTo("application/json"));
            Assert.That(_handler.Requests[0].Headers.UserAgent.ToString(),
                Is.EqualTo(string.Format("moosend-api-client-{0}-{1}", Environment.Version, Environment.OSVersion)));
        }

        [Test]
        public async Task Given_MoosendApiClient_When_Updating_Campaign_Then_Can_Update()
        {
            // arrange
            var ctx = new ServiceClientContext(_uri);

            var content = new ApiResponse<bool> {Context = true};
            ctx.Handler = new TestHttpMessageHandler(HttpStatusCode.OK, content);

            var client = new MoosendApiClient(_apiKey, ctx);

            // act
            var success = await client.UpdateCampaignAsync(new Guid(), new CampaignParams());

            // assert
            Assert.That(success, Is.True);
        }

        [Test]
        public async Task Given_MoosendApiClient_When_Updating_Campaign_Then_The_Payload_Is_As_Expected()
        {
            var campaignParams = new CampaignParams {Name = "campaign_name", SenderEmail = "email@test.com"};

            try
            {
                await _client.UpdateCampaignAsync(new Guid(), campaignParams);
            }
            catch (Exception ex)
            {
                // known serialization exception caused by returning {} from test handler 
            }

            var expectedJson = JsonConvert.SerializeObject(campaignParams);

            Assert.That(_handler.Payloads[0].Contains(expectedJson));
        }

        [Test]
        public async Task Given_MoosendApiClient_When_Unscheduling_Campaign_Then_The_Right_Url_Is_Accessed()
        {
            var url = string.Format("/campaigns/{0}/unschedule.json?apikey={1}", new Guid(), _apiKey);

            var expectedUrl = new Uri(_uri + url);

            try
            {
                await _client.UnscheduleCampaignAsync(new Guid());
            }
            catch (Exception ex)
            {
                // known serialization exception caused by returning {} from test handler 
            }

            Assert.That(HttpMethod.Post, Is.EqualTo(_handler.Requests[0].Method));
            Assert.That(expectedUrl.AbsoluteUri, Is.EqualTo(_handler.Requests[0].RequestUri.AbsoluteUri));
        }

        [Test]
        public async Task Given_MoosendApiClient_When_Unscheduling_Campaign_Then_The_Headers_Are_As_Expected()
        {
            try
            {
                await _client.UnscheduleCampaignAsync(new Guid());
            }
            catch (Exception ex)
            {
                // known serialization exception caused by returning {} from test handler 
            }

            Assert.That(_handler.Requests[0].Headers.Accept.FirstOrDefault().MediaType, Is.EqualTo("application/json"));
            Assert.That(_handler.Requests[0].Headers.UserAgent.ToString(),
                Is.EqualTo(string.Format("moosend-api-client-{0}-{1}", Environment.Version, Environment.OSVersion)));
        }

        [Test]
        public async Task Given_MoosendApiClient_When_Unscheduling_Campaign_Then_Can_Unschedule()
        {
            // arrange
            var ctx = new ServiceClientContext(_uri);

            var content = new ApiResponse<bool> {Context = true};
            ctx.Handler = new TestHttpMessageHandler(HttpStatusCode.OK, content);

            var client = new MoosendApiClient(_apiKey, ctx);

            // act
            var success = await client.UnscheduleCampaignAsync(new Guid());

            // assert
            Assert.That(success, Is.True);
        }

        [Test]
        public async Task Given_MoosendApiClient_When_AbTestCampaignSummary_Then_The_Right_Url_Is_Accessed()
        {
           var cmId = new Guid();
            var url = string.Format("/campaigns/{0}/view_ab_summary.json?apikey={1}", cmId, _apiKey);

            var expectedUrl = new Uri(_uri + url);

            try
            {
                await _client.GetAbTestCampaignSummary(cmId);
            }
            catch (Exception ex)
            {
                // known serialization exception caused by returning {} from test handler 
            }

            Assert.That(_handler.Requests[0].Method, Is.EqualTo(HttpMethod.Get));
            Assert.That(_handler.Requests[0].RequestUri.AbsoluteUri, Is.EqualTo(expectedUrl.AbsoluteUri));
        }

        [Test]
        public async Task Given_MoosendApiClient_When_Getting_AbTestCampaignSummary_Then_The_Headers_Are_As_Expected()
        {
            try
            {
                await _client.GetAbTestCampaignSummary(new Guid());
            }
            catch (Exception ex)
            {
                // known serialization exception caused by returning {} from test handler 
            }

            Assert.That(_handler.Requests[0].Headers.Accept.FirstOrDefault().MediaType, Is.EqualTo("application/json"));
            Assert.That(_handler.Requests[0].Headers.UserAgent.ToString(),
                Is.EqualTo(string.Format("moosend-api-client-{0}-{1}", Environment.Version, Environment.OSVersion)));
        }

        [Test]
        public async Task Given_MoosendApiClient_When_Getting_AbTestCampaignSummary_Then_Can_Get_Summary()
        {
            // arrange
            var ctx = new ServiceClientContext(_uri);

            var expectedSummary = new AbTestCampaignSummaryResult()
            {
                A = new ABCampaign {CampaignID = new Guid()},
                B = new ABCampaign {CampaignID = new Guid()},
                CampaignId = new Guid()
            };

            var content = new ApiResponse<AbTestCampaignSummaryResult> { Context = expectedSummary };
            ctx.Handler = new TestHttpMessageHandler(HttpStatusCode.OK, content);

            var client = new MoosendApiClient(_apiKey, ctx);

            // act
            var result = await client.GetAbTestCampaignSummary(new Guid());

            // assert
            Assert.That(result.A.CampaignID, Is.EqualTo(expectedSummary.A.CampaignID));
            Assert.That(result.B.CampaignID, Is.EqualTo(expectedSummary.B.CampaignID));
            Assert.That(result.CampaignId, Is.EqualTo(expectedSummary.CampaignId));
        }

        [Test]
        public async Task Given_MoosendApiClient_When_Scheduling_Campaign_Then_The_Right_Url_Is_Accessed()
        {
            var url = string.Format("/campaigns/{0}/schedule.json?apikey={1}", new Guid(), _apiKey);

            var expectedUrl = new Uri(_uri + url);

            try
            {
                await _client.ScheduleCampaignAsync(new Guid(), new DateTime(), "timezone");
            }
            catch (Exception ex)
            {
                // known serialization exception caused by returning {} from test handler 
            }

            Assert.That(HttpMethod.Post, Is.EqualTo(_handler.Requests[0].Method));
            Assert.That(expectedUrl.AbsoluteUri, Is.EqualTo(_handler.Requests[0].RequestUri.AbsoluteUri));
        }

        [Test]
        public async Task Given_MoosendApiClient_When_Scheduling_Campaign_Then_The_Headers_Are_As_Expected()
        {
            try
            {
                await _client.ScheduleCampaignAsync(new Guid(), new DateTime(), "timezone");
            }
            catch (Exception ex)
            {
                // known serialization exception caused by returning {} from test handler 
            }

            Assert.That(_handler.Requests[0].Headers.Accept.FirstOrDefault().MediaType, Is.EqualTo("application/json"));
            Assert.That(_handler.Requests[0].Headers.UserAgent.ToString(),
                Is.EqualTo(string.Format("moosend-api-client-{0}-{1}", Environment.Version, Environment.OSVersion)));
        }

        [Test]
        public async Task Given_MoosendApiClient_When_Scheduling_Campaign_Then_Can_Schedule()
        {
            // arrange
            var ctx = new ServiceClientContext(_uri);

            var content = new ApiResponse<bool> { Context = true };
            ctx.Handler = new TestHttpMessageHandler(HttpStatusCode.OK, content);

            var client = new MoosendApiClient(_apiKey, ctx);

            // act
            var success = await client.ScheduleCampaignAsync(new Guid(), new DateTime(), "timezone");

            // assert
            Assert.That(success, Is.True);
        }

        [Test]
        public async Task Given_MoosendApiClient_When_Scheduling_Campaign_Then_The_Payload_Is_As_Expected()
        {
            var campaignParams = new CampaignParams { Name = "campaign_name", SenderEmail = "email@test.com" };

            try
            {
                await _client.ScheduleCampaignAsync(new Guid(), new DateTime(), "timezone");
            }
            catch (Exception ex)
            {
                // known serialization exception caused by returning {} from test handler 
            }

            var expectedJson = "{\"DateTime\":\"01/01/001 00:00:00\",\"Timezone\":\"timezone\"}";

            Assert.That(_handler.Payloads[0].Contains(expectedJson));
        }
    }
}