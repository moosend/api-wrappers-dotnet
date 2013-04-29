using System;
using System.Runtime.Serialization;
using Moosend.API.Client;

namespace Moosend.API.Client.Models
{
    [Serializable]
    public class ABCampaignData
    {
        [DataMember]
        public virtual int ID
        {
            get;
            internal set;
        }

        [DataMember]
        public virtual String SubjectB
        {
            get;
            set;
        }

        [DataMember]
        public virtual String PlainContentB
        {
            get;
            set;
        }

        [DataMember]
        public virtual String HTMLContentB
        {
            get;
            set;
        }

        [DataMember]
        public virtual String WebLocationB
        {
            get;
            set;
        }

        [DataMember]
        public virtual Sender SenderB
        {
            get;
            set;
        }

        [DataMember]
        public virtual int HoursToTest
        {
            get;
            set;
        }

        [DataMember]
        public virtual int ListPercentage
        {
            get;
            set;
        }

        [DataMember]
        public virtual ABCampaignType ABCampaignType
        {
            get;
            set;
        }

        [DataMember]
        public virtual ABWinnerSelectionType ABWinnerSelectionType
        {
            get;
            set;
        }

        [DataMember]
        public virtual DateTime? DeliveredOnA
        {
            get;
            set;
        }

        [DataMember]
        public virtual DateTime? DeliveredOnB
        {
            get;
            set;
        }
    }
}