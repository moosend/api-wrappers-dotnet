using System;
using System.Net.Http;
using Moosend.Api.Client;
using NUnit.Framework;

namespace Moosend.Api.Tests
{
    [TestFixture]
    public class ServiceClientContextTests
    {
        [Test]
        public void Given_Null_Endpoint_When_Creating_ServiceClientContext_Then_It_Throws()
        {
            var e = Assert.Throws<ArgumentNullException>(() => new ServiceClientContext(null, "api_key"));
        }

        [Test]
        public void Given_Null_ApiKey_When_Creating_ServiceClientContext_Then_It_Throws()
        {
            var e = Assert.Throws<ArgumentNullException>(() => new ServiceClientContext(new Uri("http://endpoint"), null));
        }

        [Test]
        public void Given_Valid_Arguments_When_Creating_ServiceClientContext_Then_It_Can_Create_Instance()
        {
            var apiKey = "api_key";
            var endpoint = new Uri("http://endpoint");
            var context = new ServiceClientContext(endpoint, apiKey);
            HttpMessageHandler handler = new HttpClientHandler();
            context.Handler = handler;

            Assert.That(context, Is.InstanceOf<ServiceClientContext>());
            Assert.That(endpoint, Is.EqualTo(context.Endpoint));
            Assert.That(apiKey, Is.EqualTo(context.ApiKey));
            Assert.That(TimeSpan.FromSeconds(10), Is.EqualTo(context.Timeout));
            Assert.That(handler, Is.EqualTo(context.Handler));
        }
    }
}
