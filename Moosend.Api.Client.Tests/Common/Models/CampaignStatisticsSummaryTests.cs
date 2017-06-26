using Moosend.Api.Common.Models;
using NUnit.Framework;

namespace Moosend.Api.Tests.Common.Models
{
    [TestFixture]
    public class CampaignStatisticsSummaryTests
    {
        [Test]
        public void Given_Sent_Is_Not_0_Then_It_Returns_LinkClicksPercentage_Right_Value()
        {
            var sent = 10;
            var uniqueLinkClicks = 3;

            var campaignStatisticsSummary = new CampaignStatisticsSummary()
            {
                Sent = sent,
                UniqueLinkClicks = uniqueLinkClicks
            };

            Assert.AreEqual(uniqueLinkClicks/(double)sent, campaignStatisticsSummary.LinkClicksPercentage);
        }

        [Test]
        public void Given_Sent_Is_Not_0_Then_It_Returns_OpenedPercentage_Right_Value()
        {
            var sent = 10;
            var uniqueOpens = 3;

            var campaignStatisticsSummary = new CampaignStatisticsSummary()
            {
                Sent = sent,
                UniqueOpens = uniqueOpens
            };

            Assert.AreEqual(uniqueOpens / (double)sent, campaignStatisticsSummary.OpenedPercentage);
        }

        [Test]
        public void Given_Sent_Is_Not_0_Then_It_Returns_BouncedPercentage_Right_Value()
        {
            var sent = 10;
            var totalBounces = 3;

            var campaignStatisticsSummary = new CampaignStatisticsSummary()
            {
                Sent = sent,
                TotalBounces = totalBounces
            };

            Assert.AreEqual(totalBounces / (double)sent, campaignStatisticsSummary.BouncedPercentage);
        }

        [Test]
        public void Given_Sent_Is_Not_0_Then_It_Returns_UnsubscribedPercentage_Right_Value()
        {
            var sent = 10;
            var totalUnsubscribes = 3;

            var campaignStatisticsSummary = new CampaignStatisticsSummary()
            {
                Sent = sent,
                TotalUnsubscribes = totalUnsubscribes

            };

            Assert.AreEqual(totalUnsubscribes / (double)sent, campaignStatisticsSummary.UnsubscribedPercentage);
        }

        [Test]
        public void Given_Sent_Is_Not_0_Then_It_Returns_UndeliveredPercentage_Right_Value()
        {
            var sent = 10;
            var totalBounces = 3;

            var campaignStatisticsSummary = new CampaignStatisticsSummary()
            {
                Sent = sent,
                TotalBounces = totalBounces

            };

            Assert.AreEqual(totalBounces / (double)sent, campaignStatisticsSummary.UndeliveredPercentage);
        }
    }
}