using System;

namespace Moosend.Api.Common.Models
{
    public class AbCampaignData
    {
        public int Id
        {
            get;
            set;
        }

        public string SubjectB
        {
            get;
            set;
        }

        public string PlainContentB
        {
            get;
            set;
        }

        public string HtmlContentB
        {
            get;
            set;
        }

        public string WebLocationB
        {
            get;
            set;
        }

        public Sender SenderB
        {
            get;
            set;
        }

        public int HoursToTest
        {
            get;
            set;
        }

        public int ListPercentage
        {
            get;
            set;
        }

        public AbCampaignType AbCampaignType
        {
            get;
            set;
        }

        public AbWinnerSelectionType AbWinnerSelectionType
        {
            get;
            set;
        }

        public DateTime? DeliveredOnA
        {
            get;
            set;
        }

        public DateTime? DeliveredOnB
        {
            get;
            set;
        }
    }
}