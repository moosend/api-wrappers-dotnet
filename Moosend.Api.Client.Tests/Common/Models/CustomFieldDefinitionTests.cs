using System;
using Moosend.Api.Common;
using Moosend.Api.Common.Models;
using NUnit.Framework;

namespace Moosend.Api.Tests.Common.Models
{
    [TestFixture]
    public class CustomFieldDefinitionTests
    {
        [Test]
        public void Can_Create_CustomFieldDefinition()
        {
            var id = Guid.NewGuid();
            var name = "name";
            var context = "context";
            var isRequired = true;
            var type = CustomFieldType.Text;

            var cfDef = new CustomFieldDefinition()
            {
                Id = id,
                Name = name,
                Context = context,
                IsRequired = isRequired,
                Type = type
            };

            Assert.AreEqual(id, cfDef.Id);
            Assert.AreEqual(name, cfDef.Name);
            Assert.AreEqual(context, cfDef.Context);
            Assert.AreEqual(isRequired, cfDef.IsRequired);
            Assert.AreEqual(type, cfDef.Type);
        }
    }
}