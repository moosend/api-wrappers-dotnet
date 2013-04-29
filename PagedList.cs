using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Moosend.API.Client
{
    public interface IPagableCollection : ICollection
    {
        int TotalPageCount { get; }

        long TotalResults { get; set; }

        PagingInfo PagingInfo { get; }
    }

    [Serializable]
    public class PagedList<T> : List<T>, IPagableCollection
    {
        public PagedList()
        {
            this.PagingInfo = PagingInfo.All;
        }

        public PagedList(PagingInfo pagingInfo)
        {
            if (pagingInfo == null) throw new ArgumentNullException("pagingInfo");

            this.PagingInfo = pagingInfo;
        }

        public PagedList(IList<T> list, PagingInfo pagingInfo)
        {
            if (pagingInfo == null) throw new ArgumentNullException("pagingInfo");

            if (list != null) this.AddRange(list);

            this.PagingInfo = pagingInfo;
        }

        public PagingInfo PagingInfo
        {
            get;
            private set;
        }

        public long TotalResults
        {
            get;
            set;
        }

        public int TotalPageCount
        {
            get
            {
                return (int)Math.Ceiling(TotalResults / (double)PagingInfo.PageSize);
            }
        }

        public Boolean CanPage
        {
            get
            {
                return HasNext || HasPrevious;
            }
        }


        public Boolean HasNext
        {
            get
            {
                return PagingInfo.CurrentPage < TotalPageCount;
            }
        }

        public Boolean HasPrevious
        {
            get
            {
                return PagingInfo.CurrentPage > 1;
            }
        }
    }
}
