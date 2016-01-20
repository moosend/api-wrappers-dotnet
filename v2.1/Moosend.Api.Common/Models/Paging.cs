namespace Moosend.Api.Common.Models
{
    /// <summary>
    ///     Represents an object to store paging information
    /// </summary>
    public class Paging
    {
        public Paging()
        {
        }

        /// <summary>
        ///     Instantiate a new paging information object starting at the first page with the given number of entries (pageSize)
        ///     per page
        /// </summary>
        public Paging(int pageSize)
        {
            PageSize = pageSize;
            CurrentPage = 1;
        }

        /// <summary>
        ///     Instantiate a new paging information object starting at currentPage with the given number of entries (pageSize) per
        ///     page
        /// </summary>
        public Paging(int pageSize, int currentPage)
        {
            PageSize = pageSize;
            CurrentPage = currentPage;
        }

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

        public override bool Equals(object obj)
        {
            var p = obj as Paging;
            if (p == null) return false;

            if (PageSize != p.PageSize) return false;
            if (CurrentPage != p.CurrentPage) return false;

            return true;
        }
    }
}
