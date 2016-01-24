namespace Moosend.Api.Common.Models
{
    public struct AnalyticsDetails
    {
        public string Context { get; set; }

        public string ContextName { get; set; }

        public int TotalCount { get; set; }

        public int UniqueCount { get; set; }

        public string ContextDescription { get; set; }
    }
}
