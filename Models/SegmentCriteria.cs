using System;
using System.Runtime.Serialization;

namespace Moosend.API.Client.Models
{
    [Serializable]
    [DataContract(Namespace = "")]
    public class SegmentCriteria
    {
        [DataMember]
        public virtual int ID
        {
            get;
            internal set;
        }

        [DataMember]
        public virtual int SegmentID
        {
            get;
            internal set;
        }

        [DataMember]
        public virtual SegmentCriteriaField Field
        {
            get;
            set;
        }

        [DataMember]
        public virtual Guid? CustomFieldID
        {
            get;
            set;
        }

        [DataMember]
        public virtual SegmentCriteriaComparer Comparer
        {
            get;
            set;
        }

        [DataMember]
        public virtual String Value
        {
            get;
            set;
        }

        [DataMember]
        public virtual DateTime? DateFrom
        {
            get;
            set;
        }

        [DataMember]
        public virtual DateTime? DateTo
        {
            get;
            set;
        }

    }
}
