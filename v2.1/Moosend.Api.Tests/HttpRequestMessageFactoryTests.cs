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

            HttpRequestMessageFactory.ApiKey = _apiKey;
            HttpRequestMessageFactory.Endpoint = _endpoint;
        }

        [Test]
        public void Given_Null_HttpMethod_When_Creating_HttpRequestMessage_Through_HttpRequestMessageFactory_Then_It_Throws()
        {
            var e = Assert.Throws<ArgumentNullException>(
                () =>
                    HttpRequestMessageFactory.Create(null, _path));
        }

        [Test]
        public void Given_Null_Path_When_Creating_HttpRequestMessage_Through_HttpRequestMessageFactory_Then_It_Throws()
        {
            var e = Assert.Throws<ArgumentNullException>(
                () =>
                    HttpRequestMessageFactory.Create(_httpMethod, null));
        }

        [Test]
        public void Given_Null_ApiKey_When_Creating_HttpRequestMessage_Through_HttpRequestMessageFactory_Then_It_Throws()
        {
            HttpRequestMessageFactory.ApiKey = null;

            var e = Assert.Throws<ArgumentNullException>(
                () =>
                    HttpRequestMessageFactory.Create(_httpMethod, _path));
        }

        [Test]
        public void Given_Null_Endpoint_When_Creating_HttpRequestMessage_Through_HttpRequestMessageFactory_Then_It_Throws()
        {
            HttpRequestMessageFactory.Endpoint = null;

            var e = Assert.Throws<ArgumentNullException>(
                () =>
                    HttpRequestMessageFactory.Create(_httpMethod, _path));
        }

        [Test]
        public void Given_Valid_Arguments_And_Null_QueryParams_When_Creating_HttpRequestMessage_Through_HttpRequestMessageFatctory_Then_It_Successfully_Creates_Request()
        {
            var request = HttpRequestMessageFactory.Create(_httpMethod, _path, null);
            var expectedUri = new Uri("http://endpoint.com/path/to/method?apiKey=api_key");

            Assert.That(_httpMethod, Is.EqualTo(request.Method));
            Assert.That(expectedUri, Is.EqualTo(request.RequestUri));
        }

        [Test]
        public void Given_Valid_Arguments_With_QueryParams_When_Creating_HttpRequestMessage_Through_HttpRequestMessageFatctory_Then_It_Successfully_Creates_Request()
        {
            var request = HttpRequestMessageFactory.Create(_httpMethod, _path, _queryParams);
            var expectedUri = new Uri("http://endpoint.com/path/to/method?apiKey=api_key&key_1=value_1&key_2=value_2");

            Assert.That(_httpMethod, Is.EqualTo(request.Method));
            Assert.That(expectedUri, Is.EqualTo(request.RequestUri));
        }
    }
}