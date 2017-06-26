using System;
using Moosend.Api.Common;
using Moosend.Api.Common.Models;
using NUnit.Framework;

namespace Moosend.Api.Tests.Common.Models
{
    [TestFixture]
    public class AbCampaignDataTests
    {
        [Test]
        public void Can_Create_AbCampaignData()
        {
            var id = 1;
            var subjectB = "SubjectB";
            var plainContentB = "PlainContentB";
            var htmlContentB = "HtmlContentB";
            var webLocationB = "WebLocationB";
            var senderB = new Sender();
            var hoursToTest = 10;
            var listPercentage = 30;
            var abCampaignType = AbCampaignType.Sender;
            var abWinnerSelectionType = AbWinnerSelectionType.OpenRate;
            var deliveredOnA = DateTime.UtcNow;
            var deliveredOnB = DateTime.UtcNow;

            var data = new AbCampaignData()
            {
                Id = id,
                SubjectB = subjectB,
                PlainContentB = plainContentB,
                HtmlContentB = htmlContentB,
                WebLocationB = webLocationB,
                SenderB = senderB,
                HoursToTest = hoursToTest,
                ListPercentage = listPercentage,
                AbCampaignType = abCampaignType,
                AbWinnerSelectionType = abWinnerSelectionType,
                DeliveredOnA = deliveredOnA,
                DeliveredOnB = deliveredOnB
            };

            Assert.AreEqual(id, data.Id);
            Assert.AreEqual(subjectB, data.SubjectB);
            Assert.AreEqual(plainContentB, data.PlainContentB);
            Assert.AreEqual(htmlContentB, data.HtmlContentB);
            Assert.AreEqual(webLocationB, data.WebLocationB);
            Assert.AreEqual(senderB, data.SenderB);
            Assert.AreEqual(hoursToTest, data.HoursToTest);
            Assert.AreEqual(listPercentage, data.ListPercentage);
            Assert.AreEqual(abCampaignType, data.AbCampaignType);
            Assert.AreEqual(abWinnerSelectionType, data.AbWinnerSelectionType);
            Assert.AreEqual(deliveredOnA, data.DeliveredOnA);
            Assert.AreEqual(deliveredOnB, data.DeliveredOnB);
        }
    }
}