using Moosend.Api.Common.Models;
using NUnit.Framework;

namespace Moosend.Api.Tests.Common.Models
{
    [TestFixture]
    public class CampaignMailingListTests
    {
        [Test]
        public void Can_Create_CampaignMailingList()
        {
            var campaign = new Campaign();
            var list = new MailingList();
            var segment = new Segment();

            var campaignMailingList = new CampaignMailingList()
            {
                Campaign = campaign,
                MailingList = list,
                Segment = segment
            };

            Assert.AreEqual(campaign, campaignMailingList.Campaign);
            Assert.AreEqual(list, campaignMailingList.MailingList);
            Assert.AreEqual(segment, campaignMailingList.Segment);
        }
    }
}