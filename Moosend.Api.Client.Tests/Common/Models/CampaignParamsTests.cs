using System.Collections.Generic;
using Moosend.Api.Common;
using Moosend.Api.Common.Models;
using NUnit.Framework;

namespace Moosend.Api.Tests.Common.Models
{
    [TestFixture]
    public class CampaignParamsTests
    {
        [Test]
        public void Can_Create_CampaignParams()
        {
            var name = "name";
            var subject = "subject";
            var senderEmail = "sender@test.com";
            var replyToEmail = "reply@test.com";
            var confirmationToEmail = "confirmation@email.com";
            var webLocation = "web location";
            var mailingLists = new List<CampaignSummaryMailingList>();
            var abCampaignType = AbCampaignType.SubjectLine;
            var subjectB = "subjectB";
            var webLocationB = "web location B";
            var senderEmailB = "senderB@test.com";
            var hoursToTest = 2;
            var listPercentage = 20;
            var abWinnerSelectionType = AbWinnerSelectionType.TotalUniqueClicks;

            var campaignParams = new CampaignParams()
            {
                Name = name,
                Subject = subject,
                SenderEmail = senderEmail,
                ReplyToEmail = replyToEmail,
                ConfirmationToEmail = confirmationToEmail,
                WebLocation = webLocation,
                MailingLists = mailingLists,
                AbCampaignType = abCampaignType,
                SubjectB = subjectB,
                WebLocationB = webLocationB,
                SenderEmailB = senderEmailB,
                HoursToTest = hoursToTest,
                ListPercentage = listPercentage,
                AbWinnerSelectionType = abWinnerSelectionType
            };

            Assert.AreEqual(name, campaignParams.Name);
            Assert.AreEqual(subject, campaignParams.Subject);
            Assert.AreEqual(senderEmail, campaignParams.SenderEmail);
            Assert.AreEqual(replyToEmail, campaignParams.ReplyToEmail);
            Assert.AreEqual(confirmationToEmail, campaignParams.ConfirmationToEmail);
            Assert.AreEqual(webLocation, campaignParams.WebLocation);
            Assert.AreEqual(mailingLists, campaignParams.MailingLists);
            Assert.AreEqual(abCampaignType, campaignParams.AbCampaignType);
            Assert.AreEqual(subjectB, campaignParams.SubjectB);
            Assert.AreEqual(webLocationB, campaignParams.WebLocationB);
            Assert.AreEqual(senderEmailB, campaignParams.SenderEmailB);
            Assert.AreEqual(hoursToTest, campaignParams.HoursToTest);
            Assert.AreEqual(listPercentage, campaignParams.ListPercentage);
            Assert.AreEqual(abWinnerSelectionType, campaignParams.AbWinnerSelectionType);

        }
    }
}