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
    public class SegmentsTests
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
        public async Task Given_MoosendApiClient_When_Getting_Segments_Then_The_Right_Url_Is_Accessed()
        {
            var listId = new Guid();
            var page = 2;
            var pageSize = 110;
            var url = string.Format("/lists/{0}/segments.json?apikey={1}&Page={2}&PageSize={3}", listId, _apiKey, page, pageSize);

            var expectedUrl = new Uri(_uri + url);

            try
            {
                await _client.GetSegmentsForListAsync(listId, page, pageSize);
            }
            catch (Exception ex)
            {
                // known serialization exception caused by returning {} from test handler 
            }

            Assert.That(_handler.Requests[0].Method, Is.EqualTo(HttpMethod.Get));
            Assert.That(_handler.Requests[0].RequestUri.AbsoluteUri, Is.EqualTo(expectedUrl.AbsoluteUri));
        }

        [Test]
        public async Task Given_MoosendApiClient_When_Getting_Segments_Then_The_Headers_Are_As_Expected()
        {
            try
            {
                await _client.GetSegmentsForListAsync(new Guid());
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
        public async Task Given_MoosendApiClient_When_Getting_Segments_Then_Can_Get_Segment()
        {
            // arrange
            var segId = 1;
            var ctx = new ServiceClientContext(_uri);

            var expectedSegments = new SegmentsResult()
            {
                Paging = new Paging() { TotalResults = 1 },
                Segments = new List<Segment>() { new Segment() { Id = segId } }
            };

            var content = new ApiResponse<SegmentsResult>() { Context = expectedSegments };
            ctx.Handler = new TestHttpMessageHandler(HttpStatusCode.OK, content);

            var client = new MoosendApiClient(_apiKey, ctx);

            // act
            var result = await client.GetSegmentsForListAsync(new Guid());

            // assert
            Assert.That(result.Paging.TotalResults, Is.EqualTo(expectedSegments.Paging.TotalResults));
            Assert.That(result.Segments.Count, Is.EqualTo(expectedSegments.Segments.Count()));
            Assert.That(result.Segments.Single().Id, Is.EqualTo(expectedSegments.Segments.Single().Id));
        }

        [Test]
        public async Task Given_MoosendApiClient_When_Creating_Segments_Then_The_Right_Url_Is_Accessed()
        {
            var listId = new Guid();
            var url = string.Format("/lists/{0}/segments/create.json?apikey={1}", listId, _apiKey);

            var expectedUrl = new Uri(_uri + url);

            try
            {
                await _client.CreateSegmentAsync(listId, "segment_name");
            }
            catch (Exception ex)
            {
                // known serialization exception caused by returning {} from test handler 
            }

            Assert.That(_handler.Requests[0].Method, Is.EqualTo(HttpMethod.Post));
            Assert.That(_handler.Requests[0].RequestUri.AbsoluteUri, Is.EqualTo(expectedUrl.AbsoluteUri));
        }

        [Test]
        public async Task Given_MoosendApiClient_When_Creating_Segment_Then_The_Headers_Are_As_Expected()
        {
            try
            {
                await _client.CreateSegmentAsync(new Guid(), "segment_name");
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
        public async Task Given_MoosendApiClient_When_Creating_Segment_Then_Can_Create()
        {
            // arrange
            var ctx = new ServiceClientContext(_uri);

            var expectedId = 1;

            var content = new ApiResponse<int>() { Context = expectedId };
            ctx.Handler = new TestHttpMessageHandler(HttpStatusCode.OK, content);

            var client = new MoosendApiClient(_apiKey, ctx);

            // act
            var listId = await client.CreateSegmentAsync(new Guid(), "segment_name");

            // assert
            Assert.That(listId, Is.EqualTo(expectedId));
        }

        [Test]
        public async Task Given_MoosendApiClient_When_Creating_Segment_Then_The_Payload_Is_As_Expected()
        {
            var segmentName = "segment_name";
            var segmentMatchType = SegmentMatchType.All;
            var parameters = new
            {
                Name = segmentName,
                MatchType = segmentMatchType
            };

            try
            {
                await _client.CreateSegmentAsync(new Guid(), segmentName, segmentMatchType);

            }
            catch (Exception ex)
            {
                // known serialization exception caused by returning {} from test handler 
            }

            var expectedJson = JsonConvert.SerializeObject(parameters);

            Assert.That(_handler.Payloads[0].Contains(expectedJson));
        }

        [Test]
        public async Task Given_MoosendApiClient_When_Updating_Segment_Then_The_Right_Url_Is_Accessed()
        {
            var listId = new Guid();
            var segId = 1;
            var url = string.Format("/lists/{0}/segments/{1}/update.json?apikey={2}", listId, segId, _apiKey);

            var expectedUrl = new Uri(_uri + url);

            try
            {
                await _client.UpdateSegmentAsync(listId, segId, "segment_name");
            }
            catch (Exception ex)
            {
                // known serialization exception caused by returning {} from test handler 
            }

            Assert.That(_handler.Requests[0].Method, Is.EqualTo(HttpMethod.Post));
            Assert.That(_handler.Requests[0].RequestUri.AbsoluteUri, Is.EqualTo(expectedUrl.AbsoluteUri));
        }

        [Test]
        public async Task Given_MoosendApiClient_When_Updating_Segment_Then_The_Headers_Are_As_Expected()
        {
            try
            {
                await _client.UpdateSegmentAsync(new Guid(), 1, "segment_name");
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
        public async Task Given_MoosendApiClient_When_Updating_Segment_Then_Can_Update()
        {
            // arrange
            var ctx = new ServiceClientContext(_uri);

            var content = new ApiResponse<bool>() { Context = true };
            ctx.Handler = new TestHttpMessageHandler(HttpStatusCode.OK, content);

            var client = new MoosendApiClient(_apiKey, ctx);

            // act
            var listId = await client.UpdateSegmentAsync(new Guid(), 1, "segment_name");

            // assert
            Assert.That(listId, Is.True);
        }

        [Test]
        public async Task Given_MoosendApiClient_When_Updating_Segment_Then_The_Payload_Is_As_Expected()
        {
            var segmentName = "segment_name";
            var segmentMatchType = SegmentMatchType.All;
            var parameters = new
            {
                Name = segmentName,
                MatchType = segmentMatchType
            };

            try
            {
                await _client.UpdateSegmentAsync(new Guid(), 1, segmentName, segmentMatchType);

            }
            catch (Exception ex)
            {
                // known serialization exception caused by returning {} from test handler 
            }

            var expectedJson = JsonConvert.SerializeObject(parameters);

            Assert.That(_handler.Payloads[0].Contains(expectedJson));
        }

        [Test]
        public async Task Given_MoosendApiClient_When_Deleting_Segment_Then_The_Right_Url_Is_Accessed()
        {
            var listId = new Guid();
            var segId = 1;
            var url = string.Format("/lists/{0}/segments/{1}/delete.json?apikey={2}", listId, segId, _apiKey);

            var expectedUrl = new Uri(_uri + url);

            try
            {
                await _client.DeleteSegmentAsync(listId, segId);
            }
            catch (Exception ex)
            {
                // known serialization exception caused by returning {} from test handler 
            }

            Assert.That(_handler.Requests[0].Method, Is.EqualTo(HttpMethod.Delete));
            Assert.That(_handler.Requests[0].RequestUri.AbsoluteUri, Is.EqualTo(expectedUrl.AbsoluteUri));
        }

        [Test]
        public async Task Given_MoosendApiClient_When_Deleting_Segment_Then_The_Headers_Are_As_Expected()
        {
            try
            {
                await _client.DeleteSegmentAsync(new Guid(), 1);
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
        public async Task Given_MoosendApiClient_When_Deleting_Segment_Then_Can_Delete()
        {
            // arrange
            var ctx = new ServiceClientContext(_uri);

            var content = new ApiResponse<bool>() { Context = true };
            ctx.Handler = new TestHttpMessageHandler(HttpStatusCode.OK, content);

            var client = new MoosendApiClient(_apiKey, ctx);

            // act
            var listId = await client.DeleteSegmentAsync(new Guid(), 1);

            // assert
            Assert.That(listId, Is.True);
        }

        [Test]
        public async Task Given_MoosendApiClient_When_Adding_Segment_Criteria_Then_The_Right_Url_Is_Accessed()
        {
            var listId = new Guid();
            var segId = 1;
            var url = string.Format("/lists/{0}/segments/{1}/criteria/add.json?apikey={2}", listId, segId, _apiKey);

            var expectedUrl = new Uri(_uri + url);

            try
            {
                await _client.AddSegmentCriteriaAsync(listId, segId, "CampaignsOpened", SegmentCriteriaComparer.IsGreaterThanOrEqualTo, "100");
            }
            catch (Exception ex)
            {
                // known serialization exception caused by returning {} from test handler 
            }

            Assert.That(_handler.Requests[0].Method, Is.EqualTo(HttpMethod.Post));
            Assert.That(_handler.Requests[0].RequestUri.AbsoluteUri, Is.EqualTo(expectedUrl.AbsoluteUri));
        }

        [Test]
        public async Task Given_MoosendApiClient_When_Adding_Segment_Criteria_Then_The_Headers_Are_As_Expected()
        {
            try
            {
                await _client.AddSegmentCriteriaAsync(new Guid(), 1, "CampaignsOpened", SegmentCriteriaComparer.IsGreaterThanOrEqualTo, "100");
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
        public async Task Given_MoosendApiClient_When_Adding_Segment_Criteria_Then_Can_Add()
        {
            // arrange
            var ctx = new ServiceClientContext(_uri);

            var expectedId = 1;

            var content = new ApiResponse<int>() { Context = expectedId };
            ctx.Handler = new TestHttpMessageHandler(HttpStatusCode.OK, content);

            var client = new MoosendApiClient(_apiKey, ctx);

            // act
            var criteriaId = await client.AddSegmentCriteriaAsync(new Guid(), 1, "CampaignsOpened", SegmentCriteriaComparer.IsGreaterThanOrEqualTo, "100");

            // assert
            Assert.That(criteriaId, Is.EqualTo(expectedId));
        }

        [Test]
        public async Task Given_MoosendApiClient_When_Adding_Segment_Criteria_Then_The_Payload_Is_As_Expected()
        {
            var field = "CampaignsOpened";
            var comparer = SegmentCriteriaComparer.IsGreaterThan;
            var value = "100";
            var dateFrom = new DateTime();
            var dateTo = new DateTime();
            var parameters = new
            {
                Field = field,
                Comparer = comparer,
                Value = value,
                DateFrom = dateFrom,
                DateTo = dateTo
            };

            try
            {
                await _client.AddSegmentCriteriaAsync(new Guid(), 1, field, comparer, value, dateFrom, dateTo);

            }
            catch (Exception ex)
            {
                // known serialization exception caused by returning {} from test handler 
            }

            var expectedJson = JsonConvert.SerializeObject(parameters);

            Assert.That(_handler.Payloads[0].Contains(expectedJson));
        }

        [Test]
        public async Task Given_MoosendApiClient_When_Updating_Segment_Criteria_Then_The_Right_Url_Is_Accessed()
        {
            var listId = new Guid();
            var segId = 1;
            var criteriaId = 1;
            var url = string.Format("/lists/{0}/segments/{1}/criteria/{2}/update.json?apikey={3}", listId, segId, criteriaId, _apiKey);

            var expectedUrl = new Uri(_uri + url);

            try
            {
                await _client.UpdateSegmentCriteriaAsync(listId, segId, criteriaId, "CampaignsOpened", SegmentCriteriaComparer.IsGreaterThanOrEqualTo, "100");
            }
            catch (Exception ex)
            {
                // known serialization exception caused by returning {} from test handler 
            }

            Assert.That(_handler.Requests[0].Method, Is.EqualTo(HttpMethod.Post));
            Assert.That(_handler.Requests[0].RequestUri.AbsoluteUri, Is.EqualTo(expectedUrl.AbsoluteUri));
        }

        [Test]
        public async Task Given_MoosendApiClient_When_Updating_Segment_Criteria_Then_The_Headers_Are_As_Expected()
        {
            try
            {
                await _client.UpdateSegmentCriteriaAsync(new Guid(), 1, 1, "CampaignsOpened", SegmentCriteriaComparer.IsGreaterThanOrEqualTo, "100");
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
        public async Task Given_MoosendApiClient_When_Updating_Segment_Criteria_Then_Can_Update()
        {
            // arrange
            var ctx = new ServiceClientContext(_uri);

            var content = new ApiResponse<bool>() { Context = true };
            ctx.Handler = new TestHttpMessageHandler(HttpStatusCode.OK, content);

            var client = new MoosendApiClient(_apiKey, ctx);

            // act
            var criteriaId = await client.UpdateSegmentCriteriaAsync(new Guid(), 1, 1, "CampaignsOpened", SegmentCriteriaComparer.IsGreaterThanOrEqualTo, "100");

            // assert
            Assert.That(criteriaId, Is.True);
        }

        [Test]
        public async Task Given_MoosendApiClient_When_Updating_Segment_Criteria_Then_The_Payload_Is_As_Expected()
        {
            var field = "CampaignsOpened";
            var comparer = SegmentCriteriaComparer.IsGreaterThan;
            var value = "100";
            var dateFrom = new DateTime();
            var dateTo = new DateTime();
            var parameters = new
            {
                Field = field,
                Comparer = comparer,
                Value = value,
                DateFrom = dateFrom,
                DateTo = dateTo
            };

            try
            {
                await _client.UpdateSegmentCriteriaAsync(new Guid(), 1, 1, field, comparer, value, dateFrom, dateTo);

            }
            catch (Exception ex)
            {
                // known serialization exception caused by returning {} from test handler 
            }

            var expectedJson = JsonConvert.SerializeObject(parameters);

            Assert.That(_handler.Payloads[0].Contains(expectedJson));
        }

        [Test]
        public async Task Given_MoosendApiClient_When_Getting_Segment_Then_The_Right_Url_Is_Accessed()
        {
            var listId = new Guid();
            var segId = 2;
            var url = string.Format("/lists/{0}/segments/{1}/details.json?apikey={2}", listId, segId, _apiKey);

            var expectedUrl = new Uri(_uri + url);

            try
            {
                await _client.GetSegmentById(listId, segId);
            }
            catch (Exception ex)
            {
                // known serialization exception caused by returning {} from test handler 
            }

            Assert.That(_handler.Requests[0].Method, Is.EqualTo(HttpMethod.Get));
            Assert.That(_handler.Requests[0].RequestUri.AbsoluteUri, Is.EqualTo(expectedUrl.AbsoluteUri));
        }

        [Test]
        public async Task Given_MoosendApiClient_When_Getting_Segment_Then_The_Headers_Are_As_Expected()
        {
            try
            {
                await _client.GetSegmentById(new Guid(), 1);
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
        public async Task Given_MoosendApiClient_When_Getting_Segment_Then_Can_Get_Segments()
        {
            // arrange
            var segId = 1;
            var ctx = new ServiceClientContext(_uri);

            var expectedSegment = new Segment() { Id = 1 };

            var content = new ApiResponse<Segment>() { Context = expectedSegment };
            ctx.Handler = new TestHttpMessageHandler(HttpStatusCode.OK, content);

            var client = new MoosendApiClient(_apiKey, ctx);

            // act
            var result = await client.GetSegmentById(new Guid(), 1);

            // assert
            Assert.That(result.Id, Is.EqualTo(expectedSegment.Id));
        }
    }
}