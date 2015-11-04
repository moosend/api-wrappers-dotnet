using System;
using System.Collections.Generic;
using Moosend.API.Client.Models;

namespace Moosend.API.Client.Wrappers
{
    public interface ICampaignsWrapper
    {
        Campaign Clone(Guid campaignID);
        Guid Create(CampaignParams campaignParams);
        void Delete(Guid campaignID);
        PagedList<CampaignSummary> FindAll(int page = 1, int pageSize = 10);
        Campaign FindByID(Guid campaignID);
        Sender FindSender(string email);
        PagedList<ContextAnalyticsNode> GetActivityByLocation(Guid campaignID);
        PagedList<ContextAnalyticsNode> GetLinkActivity(Guid campaignID);
        IList<Sender> GetSenders();
        PagedList<ContextAnalyticsNode> GetStatistics(Guid campaignID, MailStatus type = MailStatus.Sent, int page = 1, int pageSize = 50, DateTime? from = default(DateTime?), DateTime? to = default(DateTime?));
        CampaignStatisticsSummary GetSummary(Guid campaignID);
        void Save(Campaign campaign);
        void Send(Guid campaignID);
        void SendTest(Guid campaignID, IList<string> emails);
        void Update(Campaign campaign);
    }
}