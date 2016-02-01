using System;
using System.Net;
using System.Net.Http;
using Moosend.Api.Client;
using Moosend.Api.Common;
using Moosend.Api.Common.Models;
using Moosend.Api.Common.Responses;
using NUnit.Framework;

namespace Moosend.Api.Tests
{
    [TestFixture]
    public class MoosendApiClientTests
    {
        private Uri _uri;
        private string _apiKey;
        private MoosendApiClient _client;
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
        public void Given_Null_Api_Key_When_Constructing_MoosendApiClient_Then_It_Throws()
        {
            var e = Assert.Throws<ArgumentNullException>(() => new MoosendApiClient(null));
            Assert.That(e.ParamName, Is.EqualTo("apiKey"));
        }

        [Test]
        public void Given_Api_Returns_Code_Minus_2_Then_It_Throws_And_The_Error_Contains_The_Expected_Messages()
        {
            var content = new StringContent("{\"Code\":-2,\"Error\":null,\"Context\":{\"ID\":\"00000000-0000-0000-0000-000000000000\",\"Messages\":[{\"Code\":501,\"Message\":\"Sender is missing\"}]}}");
            var response = new HttpResponseMessage(HttpStatusCode.OK) {Content = content};

            var ae = Assert.Throws<AggregateException>(() =>
            {
                var campaign = _client.GetResponse<Campaign>(response).Result;
            }); 

            Assert.That(ae.InnerExceptions.Count, Is.EqualTo(1));
            Assert.That(ae.InnerException, Is.TypeOf<ApiClientException>());
            Assert.That(((ApiClientException) ae.InnerException).Code, Is.EqualTo(-2));
            Assert.That(((ApiClientException)ae.InnerException).Message.Contains("Sender is missing"));
        }

        [Test]
        public void Given_Api_Returns_Http_Status_Code_400_Then_It_Throws_With_The_Expected_Code_And_Message()
        {
            var content = new StringContent("content");
            var response = new HttpResponseMessage(HttpStatusCode.BadRequest) { Content = content };

            var ae = Assert.Throws<AggregateException>(() =>
            {
                var campaign = _client.GetResponse<Campaign>(response).Result;
            });

            Assert.That(ae.InnerExceptions.Count, Is.EqualTo(1));
            Assert.That(ae.InnerException, Is.TypeOf<ApiClientException>());
            Assert.That(((ApiClientException)ae.InnerException).Code, Is.EqualTo(400));
            Assert.That(((ApiClientException)ae.InnerException).Message, Is.EqualTo("Bad Request"));
        }

        [Test]
        public void Given_Api_Returns_Http_Status_Code_200_But_Unknown_Api_Code_Then_It_Throws_With_The_Expected_Code_And_Message()
        {
            var content = new StringContent("{\"Code\":-30,\"Error\":\"Unknown Error\",\"Context\":null}");
            var response = new HttpResponseMessage(HttpStatusCode.OK) { Content = content };

            var ae = Assert.Throws<AggregateException>(() =>
            {
                var campaign = _client.GetResponse<Campaign>(response).Result;
            });

            Assert.That(ae.InnerExceptions.Count, Is.EqualTo(1));
            Assert.That(ae.InnerException, Is.TypeOf<ApiClientException>());
            Assert.That(((ApiClientException)ae.InnerException).Code, Is.EqualTo(-30));
            Assert.That(((ApiClientException)ae.InnerException).Message, Is.EqualTo("Unknown Error"));
        }

        [Test]
        public void Given_Api_Returns_Http_Status_Code_404_Then_It_Throws_With_The_Expected_Code_And_Message()
        {
            var content = new StringContent("content");
            var response = new HttpResponseMessage(HttpStatusCode.NotFound) { Content = content };

            var ae = Assert.Throws<AggregateException>(() =>
            {
                var campaign = _client.GetResponse<Campaign>(response).Result;
            });

            Assert.That(ae.InnerExceptions.Count, Is.EqualTo(1));
            Assert.That(ae.InnerException, Is.TypeOf<ApiClientException>());
            Assert.That(((ApiClientException)ae.InnerException).Code, Is.EqualTo(404));
            Assert.That(((ApiClientException)ae.InnerException).Message, Is.EqualTo("Not Found"));
        }

        [Test]
        public void Given_Api_Unexpected_Http_Status_Code_Then_It_Throws_With_The_Expected_Code_And_Message()
        {
            var content = new StringContent("content");
            var response = new HttpResponseMessage(HttpStatusCode.Forbidden) { Content = content };

            var ae = Assert.Throws<AggregateException>(() =>
            {
                var campaign = _client.GetResponse<Campaign>(response).Result;
            });

            Assert.That(ae.InnerExceptions.Count, Is.EqualTo(1));
            Assert.That(ae.InnerException, Is.TypeOf<ApiClientException>());
            Assert.That(((ApiClientException)ae.InnerException).Code, Is.EqualTo((int)response.StatusCode));
            Assert.That(((ApiClientException)ae.InnerException).Message, Is.EqualTo("An error occurred."));
        }

        [Test]
        public void Given_Empty_Http_Content_When_Getting_Response_Then_It_Throws()
        {
            var response = new HttpResponseMessage(HttpStatusCode.OK);

            var ae = Assert.Throws<AggregateException>(() =>
            {
                var campaign = _client.GetResponse<Campaign>(response).Result;
            });

            Assert.That(ae.InnerExceptions.Count, Is.EqualTo(1));
            Assert.That(ae.InnerException, Is.TypeOf<ApiClientException>());
            Assert.That(((ApiClientException)ae.InnerException).Message, Is.EqualTo("Response content was empty."));
        }
    }
}