using System;
using System.Runtime.Serialization;

namespace Moosend.API.Client.Serialization
{
    [Serializable]
    [DataContract(Namespace = "")]
    internal class SerializablePagingInfo
    {
        [DataMember]
        public int PageSize { get; set; }

        [DataMember]
        public int CurrentPage { get; set; }

        [DataMember]
        public string SortExpression { get; set; }

        [DataMember]
        public Boolean SortIsAscending { get; set; }

        [DataMember]
        public long TotalResults { get; set; }

    }
}
