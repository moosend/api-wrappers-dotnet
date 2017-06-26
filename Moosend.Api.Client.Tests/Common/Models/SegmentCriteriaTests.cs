using System;
using Moosend.Api.Common;
using Moosend.Api.Common.Models;
using NUnit.Framework;

namespace Moosend.Api.Tests.Common.Models
{
    [TestFixture]
    public class SegmentCriteriaTests
    {
        [Test]
        public void Can_Create_SegmentCriteria()
        {
            var id = 1;
            var segmentId = 2;
            var segmentCriteriaField = SegmentCriteriaField.CustomField;
            var cfId = Guid.NewGuid();
            var comparer = SegmentCriteriaComparer.Is;
            var value = "value";
            var dateFrom = DateTime.UtcNow;
            var dateTo = DateTime.UtcNow;

            var criteria = new SegmentCriteria()
            {
                Id = id,
                SegmentId = segmentId,
                Field = segmentCriteriaField,
                CustomFieldId = cfId,
                Comparer = comparer,
                Value = value,
                DateFrom = dateFrom,
                DateTo = dateTo
            };

            Assert.AreEqual(id, criteria.Id);
            Assert.AreEqual(segmentId, criteria.SegmentId);
            Assert.AreEqual(segmentCriteriaField, criteria.Field);
            Assert.AreEqual(cfId, criteria.CustomFieldId);
            Assert.AreEqual(comparer, criteria.Comparer);
            Assert.AreEqual(value, criteria.Value);
            Assert.AreEqual(dateFrom, criteria.DateFrom);
            Assert.AreEqual(dateTo, criteria.DateTo);
        }
    }
}