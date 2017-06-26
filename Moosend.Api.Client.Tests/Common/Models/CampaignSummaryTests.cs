using Moosend.Api.Common.Models;
using NUnit.Framework;

namespace Moosend.Api.Tests.Common.Models
{
    [TestFixture]
    public class CampaignSummaryTests
    {
        [Test]
        public void Given_RecipientsCount_Is_Not_0_When_Getting_LinkClicksPercentage_Then_It_Returns_The_Right_Value()
        {
            var recipientCount = 5;
            var uniqueLinkClicks = 3;

            var campaignSummary = new CampaignSummary
            {
                RecipientsCount = recipientCount,
                UniqueLinkClicks = uniqueLinkClicks
            };

            Assert.AreEqual(uniqueLinkClicks/(double) recipientCount, campaignSummary.LinkClicksPercentage);
        }

        [Test]
        public void Given_RecipientsCount_Is_Not_0_When_Getting_OpenedPercentage_Then_It_Returns_The_Right_Value()
        {
            var recipientCount = 5;
            var uniqueOpens = 3;

            var campaignSummary = new CampaignSummary
            {
                RecipientsCount = recipientCount,
                UniqueOpens = uniqueOpens
            };

            Assert.AreEqual(uniqueOpens/(double) recipientCount, campaignSummary.OpenedPercentage);
        }

        [Test]
        public void Given_RecipientsCount_Is_Not_0_When_Getting_BouncedPercentage_Then_It_Returns_The_Right_Value()
        {
            var recipientCount = 5;
            var bounces = 3;

            var campaignSummary = new CampaignSummary
            {
                RecipientsCount = recipientCount,
                TotalBounces = bounces
            };

            Assert.AreEqual(bounces/(double) recipientCount, campaignSummary.BouncedPercentage);
        }
    }
}