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
    internal class SerializableMailingListCollection : SerializablePagedList<MailingList>
    {
        [DataMember(Name = "MailingLists")]
        public new PagedList<MailingList> PagedList
        {
            get { return base.PagedList; }
            set { base.PagedList = value; }
        }
    }
}
