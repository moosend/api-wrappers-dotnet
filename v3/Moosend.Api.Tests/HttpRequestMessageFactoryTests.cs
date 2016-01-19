using System;
using System.Collections.Generic;
using System.Net.Http;
using Moosend.Api.Client;
using NUnit.Framework;

namespace Moosend.Api.Tests
{
    [TestFixture]
    public class HttpRequestMessageFactoryTests
    {
        private HttpMethod _httpMethod;
        private Uri _endpoint;
        private string _path;
        private string _apiKey;
        private IDictionary<string, string> _queryParams;
        private HttpRequestMessageFactory _factory;

        [SetUp]
        public void SetUp()
        {
            _httpMethod = HttpMethod.Get;
            _endpoint = new Uri("http://endpoint.com/");
            _path = "path/to/method";
            _apiKey = "api_key";
            _queryParams = new Dictionary<string, string>
            {
                {"key_1", "value_1"},
                {"key_2", "value_2"}
            };

            _factory = new HttpRequestMessageFactory();
        }

        [Test]
        public void Given_Null_HttpMethod_When_Creating_HttpRequestMessage_Through_HttpRequestMessageFactory_Then_It_Throws()
        {
            var e = Assert.Throws<ArgumentNullException>(
                () =>
                    new HttpRequestMessageFactory().Create(null, _endpoint, _path, _apiKey));
        }

        [Test]
        public void Given_Null_Endpoint_When_Creating_HttpRequestMessage_Through_HttpRequestMessageFactory_Then_It_Throws()
        {
            var e = Assert.Throws<ArgumentNullException>(
                () =>
                    new HttpRequestMessageFactory().Create(_httpMethod, null, _path, _apiKey));
        }

        [Test]
        public void Given_Null_Path_When_Creating_HttpRequestMessage_Through_HttpRequestMessageFactory_Then_It_Throws()
        {
            var e = Assert.Throws<ArgumentNullException>(
                () =>
                    new HttpRequestMessageFactory().Create(_httpMethod, _endpoint, null, _apiKey));
        }

        [Test]
        public void Given_Null_ApiKey_When_Creating_HttpRequestMessage_Through_HttpRequestMessageFactory_Then_It_Throws()
        {
            var e = Assert.Throws<ArgumentNullException>(
                () =>
                    new HttpRequestMessageFactory().Create(_httpMethod, _endpoint, _path, null));
        }

        [Test]
        public void Given_Valid_Arguments_And_Null_QueryParams_When_Creating_HttpRequestMessage_Through_HttpRequestMessageFatctory_Then_It_Successfully_Creates_Request()
        {
            var request = _factory.Create(_httpMethod, new Uri("http://endpoint.com"), _path, _apiKey, null);
            var expectedUri = new Uri("http://endpoint.com/path/to/method?apiKey=api_key");

            Assert.That(_httpMethod, Is.EqualTo(request.Method));
            Assert.That(expectedUri, Is.EqualTo(request.RequestUri));
        }

        [Test]
        public void Given_Valid_Arguments_With_QueryParams_When_Creating_HttpRequestMessage_Through_HttpRequestMessageFatctory_Then_It_Successfully_Creates_Request()
        {
            var request = _factory.Create(_httpMethod, new Uri("http://endpoint.com"), _path, _apiKey, _queryParams);
            var expectedUri = new Uri("http://endpoint.com/path/to/method?apiKey=api_key&key_1=value_1&key_2=value_2");

            Assert.That(_httpMethod, Is.EqualTo(request.Method));
            Assert.That(expectedUri, Is.EqualTo(request.RequestUri));
        }
    }
}