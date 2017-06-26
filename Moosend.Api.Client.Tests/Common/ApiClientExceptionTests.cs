using System;
using Moosend.Api.Common;
using NUnit.Framework;

namespace Moosend.Api.Tests.Common
{
    [TestFixture]
    public class ApiClientExceptionTests
    {
        [Test]
        public void Can_Create_ApiClientException()
        {
            var ex = new ApiClientException();

            Assert.IsAssignableFrom<ApiClientException>(ex);
        }

        [Test]
        public void Given_Message_Then_It_Can_Create_ApiClientException()
        {
            var msg = "a message";

            var ex = new ApiClientException(msg);

            Assert.IsAssignableFrom<ApiClientException>(ex);
            Assert.AreEqual(msg, ex.Message);
        }

        [Test]
        public void Given_Message_And_Code_Then_It_Can_Create_ApiClientException()
        {
            var msg = "a message";
            var code = 1;

            var ex = new ApiClientException(msg, code);

            Assert.IsAssignableFrom<ApiClientException>(ex);
            Assert.AreEqual(code, ex.Code);
            Assert.AreEqual(msg, ex.Message);
        }

        [Test]
        public void Given_Message_Code_And_Inner_Exception_Then_It_Can_Create_ApiClientException()
        {
            var msg = "a message";
            var code = 1;
            var innerEx = new Exception();

            var ex = new ApiClientException(msg, code, innerEx);

            Assert.IsAssignableFrom<ApiClientException>(ex);
            Assert.AreEqual(code, ex.Code);
            Assert.AreEqual(msg, ex.Message);
            Assert.AreEqual(innerEx, ex.InnerException);
        }
    }
}