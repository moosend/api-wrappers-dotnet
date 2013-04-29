using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;
using Moosend.API.Client.Models;

namespace Moosend.API.Client.Serialization
{
    [Serializable]
    [DataContract(Namespace = "")]
    public class SerializableMailingListMemberCollection : SerializablePagedList<MailingListMember>
    {
        [DataMember(Name = "MailingListMembers")]
        public new PagedList<MailingListMember> PagedList
        {
            get { return base.PagedList; }
            set { base.PagedList = value; }
        }
    }
}
