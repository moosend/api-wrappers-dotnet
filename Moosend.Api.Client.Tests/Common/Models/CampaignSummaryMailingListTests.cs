using System;
using Moosend.Api.Common.Models;
using NUnit.Framework;

namespace Moosend.Api.Tests.Common.Models
{
    [TestFixture]
    public class CampaignSummaryMailingListTests
    {
        [Test]
        public void Can_Create_CampaignSummaryMailingList()
        {
            var listId = Guid.NewGuid();
            var segmentId = 1;

            var campaignSummaryMailingList = new CampaignSummaryMailingList()
            {
                MailingListId = listId,
                SegmentId = segmentId
            };

            Assert.AreEqual(listId, campaignSummaryMailingList.MailingListId);
            Assert.AreEqual(segmentId, campaignSummaryMailingList.SegmentId);
        }
    }
}