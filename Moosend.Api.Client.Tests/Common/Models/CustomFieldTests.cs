using System;
using System.Security.Cryptography.X509Certificates;
using Moosend.Api.Common.Models;
using NUnit.Framework;

namespace Moosend.Api.Tests.Common.Models
{
    [TestFixture]
    public class CustomFieldTests
    {
        [Test]
        public void Can_Create_CustomField()
        {
            var cfId = Guid.NewGuid();
            var value = "value";

            var cf = new CustomField()
            {
                CustomFieldId = cfId,
                Value = value
            };

            Assert.AreEqual(cfId, cf.CustomFieldId);
            Assert.AreEqual(value, cf.Value);
        }
    }
}