using System;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using Moosend.Api.Client;
using Moosend.Api.Common.Models;
using Moosend.Api.Common.Responses;
using Moq;
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
        private Mock<IMoosendApiClient> _clientMock;

        [SetUp]
        public void Setup()
        {
            var url = "https://api.moosend.com/v3";
            _uri = new Uri(url);

            _apiKey = "moosend_api_key";

            _handler = new TestHttpMessageHandler();
            var ctx = new ServiceClientContext(_uri) {Handler = _handler};
            _client = new MoosendApiClient(_apiKey, ctx);

            _clientMock = new Mock<IMoosendApiClient>();
        }

        #region Campaign API tests

        [Test]
        public void Given_MoosendApiClient_When_Getting_All_Campaigns_Then_The_Right_Url_Is_Accessed()
        {
            const int page = 2;
            const int pageSize = 11;
            const string sortBy = "Name";
            const string sortMethod = "DESC";
            var url = string.Format("/campaigns/{0}/{1}.json?apikey={2}&SortBy={3}&SortMethod={4}", page,
                pageSize, _apiKey, sortBy, sortMethod);

            var expectedUrl = new Uri(_uri + url);

            try
            {
                _client.GetAllCampaignsAsync(page, pageSize, sortBy, sortMethod).Wait();
            }
            catch (Exception ex)
            {
                // known serialization exception caused by returning {} from test handler 
            }

            Assert.That(HttpMethod.Get, Is.EqualTo(_handler.Requests[0].Method));
            Assert.AreEqual(expectedUrl.AbsoluteUri, _handler.Requests[0].RequestUri.AbsoluteUri);
        }

        [Test]
        public void Given_MoosendApiClient_When_Getting_Sender_Then_The_Right_Url_Is_Accessed()
        {
            const string email = "email@test.com";
            var url = string.Format("/senders/find_one.json?apikey={0}&Email={1}", _apiKey, email);

            var expectedUrl = new Uri(_uri + url);

            try
            {
                _client.GetSenderAsync(email).Wait();
            }
            catch (Exception ex)
            {
                // known serialization exception caused by returning {} from test handler 
            }

            Assert.That(HttpMethod.Get, Is.EqualTo(_handler.Requests[0].Method));
            Assert.AreEqual(expectedUrl.AbsoluteUri, HttpUtility.UrlDecode(_handler.Requests[0].RequestUri.AbsoluteUri));
        }

        [Test]
        public void Given_MoosendApiClient_When_Getting_All_Campaigns_Then_The_Accept_Is_As_Expected()
        {
            try
            {
                _client.GetAllCampaignsAsync().Wait();
            }
            catch (Exception ex)
            {
                // known serialization exception caused by returning {} from test handler 
            }

            Assert.AreEqual("application/json", _handler.Requests[0].Headers.Accept.FirstOrDefault().MediaType);
        }

        [Test]
        public void Given_MoosendApiClient_When_Getting_Sender_Then_The_Accept_Is_As_Expected()
        {
            try
            {
                _client.GetSenderAsync("email@test.com").Wait();
            }
            catch (Exception ex)
            {
                // known serialization exception caused by returning {} from test handler 
            }

            Assert.AreEqual("application/json", _handler.Requests[0].Headers.Accept.FirstOrDefault().MediaType);
        }

        [Test]
        public async Task Given_MoosendApiClient_When_Getting_All_Campaigns_Then_Returned_Result_Is_As_Excpected()
        {
            var expectedResult = new PagedCampaigns();
            const int page = 2;
            const int pageSize = 11;
            const string sortBy = "Name";
            const string sortMethod = "DESC";

            _clientMock.Setup(x => x.GetAllCampaignsAsync(page, pageSize, sortBy, sortMethod, It.IsAny<CancellationToken>())).ReturnsAsync(expectedResult);

            var actualResult = await _clientMock.Object.GetAllCampaignsAsync(page, pageSize, sortBy, sortMethod);

            Assert.That(expectedResult, Is.EqualTo(actualResult));
            _clientMock.Verify(x => x.GetAllCampaignsAsync(page, pageSize, sortBy, sortMethod, It.IsAny<CancellationToken>()), Times.Once);
        }

        [Test]
        public async Task Given_MoosendApiClient_When_Getting_Sender_Then_Returned_Result_Is_As_Excpected()
        {
            var expectedResult = new Sender();
            const string email = "email@test.com";

            _clientMock.Setup(x => x.GetSenderAsync(email, It.IsAny<CancellationToken>())).ReturnsAsync(expectedResult);

            var actualResult = await _clientMock.Object.GetSenderAsync(email);

            Assert.That(expectedResult, Is.EqualTo(actualResult));
            _clientMock.Verify(x => x.GetSenderAsync(email, It.IsAny<CancellationToken>()), Times.Once);
        }

        #endregion
    }
}