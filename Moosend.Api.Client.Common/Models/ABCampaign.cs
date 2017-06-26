using System;
using System.Collections.Generic;

namespace Moosend.Api.Common.Models
{
    public class ABCampaign
    {
        public Guid CampaignID { get; set; }
        public int ABVersion { get; set; }
        public string CampaignName { get; set; }
        public string CampaignSubject { get; set; }
        public List<CampaignMailingList> MailingLists { get; set; }
        public DateTime CampaignDeliveredOn { get; set; }
        public DateTime To { get; set; }
        public DateTime From { get; set; }
        public int TotalOpens { get; set; }
        public int UniqueOpens { get; set; }
        public int TotalBounces { get; set; }
        public int TotalComplaints { get; set; }
        public int TotalForwards { get; set; }
        public int UniqueForwards { get; set; }
        public int TotalUnsubscribes { get; set; }
        public int TotalLinkClicks { get; set; }
        public int UniqueLinkClicks { get; set; }
        public int Sent { get; set; }
        public bool CampaignIsArchived { get; set; }
    }
}