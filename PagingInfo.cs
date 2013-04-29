using System;

namespace Moosend.API.Client
{
    /// <summary>
    /// Represents an object to store paging information
    /// </summary>
    [Serializable]
    public class PagingInfo
    {
        public static PagingInfo All
        {
            get { return new PagingInfo(0, 1); }
        }

        /// <summary>
        /// Instantiate a new paging information object starting at the first page with the given number of entries (pageSize) per page
        /// </summary>
        public PagingInfo(int pageSize)
        {
            PageSize = pageSize;
            CurrentPage = 1;
        }

        /// <summary>
        /// Instantiate a new paging information object starting at currentPage with the given number of entries (pageSize) per page
        /// </summary>
        public PagingInfo(int pageSize, int currentPage)
        {
            PageSize = pageSize;
            CurrentPage = currentPage;
        }

        /// <summary>
        /// Get the page size
        /// </summary>
        public int PageSize
        {
            get;
            set;
        }

        /// <summary>
        /// Get the current page
        /// </summary>
        public int CurrentPage
        {
            get;
            set;
        }

        public string SortExpression
        {
            get;
            set;
        }

        public Boolean SortIsAscending
        {
            get;
            set;
        }

        public override bool Equals(object obj)
        {
            PagingInfo p = obj as PagingInfo;
            if (p == null) return false;

            if (this.PageSize != p.PageSize) return false;
            if (this.CurrentPage != p.CurrentPage) return false;

            return true;
        }
    }
}
