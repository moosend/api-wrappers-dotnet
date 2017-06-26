using System;
using System.Runtime.ConstrainedExecution;
using Moosend.Api.Common.Models;
using NUnit.Framework;

namespace Moosend.Api.Tests.Common.Models
{
    [TestFixture]
    public class ImportOperationTests
    {
        [Test]
        public void Can_Create_ImportOperation()
        {
            var id = 1;
            var dataHash = Guid.NewGuid();
            var mappings = "mappings";
            var emailNotify = "email";
            var createdOn = DateTime.UtcNow;
            var startedOn = DateTime.UtcNow;
            var completedOn = DateTime.UtcNow;
            var totalInserted = 11;
            var totalUpdated = 12;
            var totalUnsubscribed = 112;
            var totalInvalid = 1121;
            var totalIgnored = 11211;
            var totalDublicate = 122;
            var totalMembers = 1221;
            var message = "message";
            var success = true;
            var skipNewMembers = "skip";

            var operation = new ImportOperation()
            {
                Id = id,
                DataHash = dataHash,
                Mappings = mappings,
                EmailNotify = emailNotify,
                CreatedOn = createdOn,
                StartedOn = startedOn,
                CompletedOn = completedOn,
                TotalInserted = totalInserted,
                TotalUpdated = totalUpdated,
                TotalUnsubscribed = totalUnsubscribed,
                TotalInvalid = totalInvalid,
                TotalIgnored = totalIgnored,
                TotalDuplicate = totalDublicate,
                TotalMembers = totalMembers,
                Message = message,
                SkipNewMembers = skipNewMembers, 
                Success = success
            };

            Assert.AreEqual(id, operation.Id);
            Assert.AreEqual(dataHash, operation.DataHash);
            Assert.AreEqual(mappings, operation.Mappings);
            Assert.AreEqual(emailNotify, operation.EmailNotify);
            Assert.AreEqual(createdOn, operation.CreatedOn);
            Assert.AreEqual(startedOn, operation.StartedOn);
            Assert.AreEqual(completedOn, operation.CompletedOn);
            Assert.AreEqual(totalInvalid, operation.TotalInvalid);
            Assert.AreEqual(totalInserted, operation.TotalInserted);
            Assert.AreEqual(totalUpdated, operation.TotalUpdated);
            Assert.AreEqual(totalUnsubscribed, operation.TotalUnsubscribed);
            Assert.AreEqual(totalIgnored, operation.TotalIgnored);
            Assert.AreEqual(totalDublicate, operation.TotalDuplicate);
            Assert.AreEqual(totalMembers, operation.TotalMembers);
            Assert.AreEqual(message, operation.Message);
            Assert.AreEqual(skipNewMembers, operation.SkipNewMembers);
            Assert.AreEqual(success, operation.Success);
        }
    }
}