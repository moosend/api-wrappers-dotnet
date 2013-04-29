using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Moosend.API.Client.Models
{
    [Serializable]
    [DataContract(Namespace = "")]
    public class MailingList
    {
        [DataMember]
        public virtual Guid ID
        {
            get;
            internal set;
        }

        [DataMember]
        public virtual String Name
        {
            get;
            set;
        }

        public virtual long AllMemberCount
        {
            get
            {
                return ActiveMemberCount + BouncedMemberCount + RemovedMemberCount + UnsubscribedMemberCount;
            }
        }

        [DataMember]
        public virtual long ActiveMemberCount
        {
            get;
            internal set;
        }

        [DataMember]
        public virtual long BouncedMemberCount
        {
            get;
            internal set;
        }

        [DataMember]
        public virtual long RemovedMemberCount
        {
            get;
            internal set;
        }

        [DataMember]
        public virtual long UnsubscribedMemberCount
        {
            get;
            internal set;
        }

        [DataMember]
        public virtual MailingListStatus Status
        {
            get;
            internal set;
        }

        private IList<CustomFieldDefinition> _CustomFieldsDefinition = new List<CustomFieldDefinition>();
        
        [DataMember]
        public virtual IList<CustomFieldDefinition> CustomFieldsDefinition
        {
            get
            {
                return _CustomFieldsDefinition;
            }
            internal set
            {
                _CustomFieldsDefinition = value;
            }
        }

        private IList<Segment> _Segments = new List<Segment>();

        [DataMember]
        public virtual IList<Segment> Segments
        {
            get
            {
                return _Segments;
            }
            set
            {
                _Segments = value;
            }
        }

        [DataMember]
        public virtual string CreatedBy
        {
            get;
            internal set;
        }

        [DataMember]
        public virtual DateTime CreatedOn
        {
            get;
            internal set;
        }

        [DataMember]
        public virtual string UpdatedBy
        {
            get;
            internal set;
        }

        [DataMember]
        public virtual DateTime UpdatedOn
        {
            get;
            internal set;
        }
    }
}
