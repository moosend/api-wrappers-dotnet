using Moosend.Api.Common.Responses;
using NUnit.Framework;

namespace Moosend.Api.Tests.Common.Responses
{
    [TestFixture]
    public class ApiResponseTests
    {
        [Test]
        public void Given_Code_And_Error_Then_It_Can_Create_ApiResponse()
        {
            var code = 1;
            var error = "error";

            var response = new ApiResponse(code, error);

            Assert.AreEqual(code, response.Code);
            Assert.AreEqual(error, response.Error);
        }

        [Test]
        public void Given_Code_Error_And_Context_Then_It_Can_Create_ApiResponse()
        {
            var code = 1;
            var error = "error";
            var context = "{}";

            var response = new ApiResponse<string>(code, error, context);

            Assert.AreEqual(code, response.Code);
            Assert.AreEqual(error, response.Error);
            Assert.AreEqual(context, response.Context);
        }
    }
}