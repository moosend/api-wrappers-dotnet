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
    internal class SerializableMailingListMemberCollection : SerializablePagedList<Subscriber>
    {
        [DataMember(Name = "Subscribers")]
        public new PagedList<Subscriber> PagedList
        {
            get { return base.PagedList; }
            set { base.PagedList = value; }
        }
    }
}
