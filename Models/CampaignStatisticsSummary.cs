using System;
using System.Runtime.Serialization;

namespace Moosend.API.Client.Models
{
    [Serializable]
    [DataContract(Namespace = "")]
    public class CampaignStatisticsSummary
    {
        [DataMember]
        public virtual int ID
        {
            get;
            internal set;
        }

        [DataMember]
        public virtual ABVersion ABVersion
        {
            get;
            internal set;
        }

        [DataMember]
        public virtual Guid CampaignID
        {
            get;
            internal set;
        }

        [DataMember]
        public virtual string CampaignName
        {
            get;
            internal set;
        }

        [DataMember]
        public virtual Guid MailingListID
        {
            get;
            internal set;
        }

        [DataMember]
        public virtual string MailingListName
        {
            get;
            internal set;
        }

        [DataMember]
        public virtual DateTime? CampaignDeliveredOn
        {
            get;
            internal set;
        }

        [DataMember]
        public virtual DateTime To
        {
            get;
            internal set;
        }

        [DataMember]
        public virtual DateTime From
        {
            get;
            internal set;
        }

        [DataMember]
        public virtual int TotalOpens
        {
            get;
            internal set;
        }

        [DataMember]
        public virtual int UniqueOpens
        {
            get;
            internal set;
        }

        [DataMember]
        public virtual int TotalBounces
        {
            get;
            internal set;
        }

        [DataMember]
        public virtual int TotalForwards
        {
            get;
            internal set;
        }

        [DataMember]
        public virtual int UniqueForwards
        {
            get;
            internal set;
        }

        [DataMember]
        public virtual int TotalUnsubscribes
        {
            get;
            internal set;
        }

        [DataMember]
        public virtual int TotalLinkClicks
        {
            get;
            internal set;
        }

        [DataMember]
        public virtual int UniqueLinkClicks
        {
            get;
            internal set;
        }

        [DataMember]
        public virtual int Sent
        {
            get;
            internal set;
        }

        public virtual double LinkClicksPercentage
        {
            get
            {
                if (Sent == 0) return 0;
                return ((double)UniqueLinkClicks / (double)Sent);
            }
        }

        public virtual double OpenedPercentage
        {
            get
            {
                if (Sent == 0) return 0;
                return ((double)UniqueOpens / (double)Sent);
            }
        }

        public virtual double BouncedPercentage
        {
            get
            {
                if (Sent == 0) return 0;
                return ((double)TotalBounces / (double)Sent);
            }
        }

        public virtual double UnsubscribedPercentage
        {
            get
            {
                if (Sent == 0) return 0;
                return ((double)TotalUnsubscribes / (double)Sent);
            }
        }

        public virtual double UndeliveredPercentage
        {
            get
            {
                if (Sent == 0) return 0;
                return ((double)TotalBounces / (double)Sent);
            }
        }

        public virtual int NotOpenedCount
        {
            get
            {
                return Sent - UniqueOpens - TotalBounces;
            }
        }
    }
}
