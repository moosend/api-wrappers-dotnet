using System;
using Newtonsoft.Json;
using Moosend.API.Client;

namespace Moosend.API.Client.Models
{
    public class ABCampaignData
    {
        [JsonProperty]
        public virtual int ID
        {
            get;
            internal set;
        }

        [JsonProperty]
        public virtual String SubjectB
        {
            get;
            set;
        }

        [JsonProperty]
        public virtual String PlainContentB
        {
            get;
            set;
        }

        [JsonProperty]
        public virtual String HTMLContentB
        {
            get;
            set;
        }

        [JsonProperty]
        public virtual String WebLocationB
        {
            get;
            set;
        }

        [JsonProperty]
        public virtual Sender SenderB
        {
            get;
            set;
        }

        [JsonProperty]
        public virtual int HoursToTest
        {
            get;
            set;
        }

        [JsonProperty]
        public virtual int ListPercentage
        {
            get;
            set;
        }

        [JsonProperty]
        public virtual ABCampaignType ABCampaignType
        {
            get;
            set;
        }

        [JsonProperty]
        public virtual ABWinnerSelectionType ABWinnerSelectionType
        {
            get;
            set;
        }

        [JsonProperty]
        public virtual DateTime? DeliveredOnA
        {
            get;
            set;
        }

        [JsonProperty]
        public virtual DateTime? DeliveredOnB
        {
            get;
            set;
        }
    }
}