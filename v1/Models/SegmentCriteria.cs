using System;
using Newtonsoft.Json;

namespace Moosend.API.Client.Models
{
    public class SegmentCriteria
    {
        [JsonProperty]
        public virtual int ID
        {
            get;
            internal set;
        }

        [JsonProperty]
        public virtual int SegmentID
        {
            get;
            internal set;
        }

        [JsonProperty]
        public virtual SegmentCriteriaField Field
        {
            get;
            set;
        }

        [JsonProperty]
        public virtual Guid? CustomFieldID
        {
            get;
            set;
        }

        [JsonProperty]
        public virtual SegmentCriteriaComparer Comparer
        {
            get;
            set;
        }

        [JsonProperty]
        public virtual String Value
        {
            get;
            set;
        }

        [JsonProperty]
        public virtual DateTime? DateFrom
        {
            get;
            set;
        }

        [JsonProperty]
        public virtual DateTime? DateTo
        {
            get;
            set;
        }

    }
}
