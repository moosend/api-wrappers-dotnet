using System;
using Newtonsoft.Json;

namespace Moosend.API.Client.Serialization
{
    internal class SerializablePagingInfo
    {
        [JsonProperty]
        public int PageSize { get; set; }

        [JsonProperty]
        public int CurrentPage { get; set; }

        [JsonProperty]
        public string SortExpression { get; set; }

        [JsonProperty]
        public Boolean SortIsAscending { get; set; }

        [JsonProperty]
        public long TotalResults { get; set; }

    }
}
