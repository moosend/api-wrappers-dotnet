namespace Moosend.Api.Common.Models
{
    /// <summary>
    ///     Represents an object to store paging information
    /// </summary>
    public class Paging
    {
        /// <summary>
        ///     Get the page size
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        ///     Get the current page
        /// </summary>
        public int CurrentPage { get; set; }

        /// <summary>
        ///     Get the total results
        /// </summary>
        public int TotalResults { get; set; }

        /// <summary>
        ///     Get the total page count
        /// </summary>
        public int TotalPageCount { get; set; }

        /// <summary>
        ///     Get the sort expression
        /// </summary>
        public string SortExpression { get; set; }

        /// <summary>
        ///     Returns if sorting is in an ascending order
        /// </summary>
        public bool SortIsAscending { get; set; }
    }
}
