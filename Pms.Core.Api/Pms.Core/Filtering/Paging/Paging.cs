using Pms.Shared.Constants;

namespace Pms.Core.Filtering
{
    public class Paging : IPaging
    {
        private int _page;
        private int _pageSize;

        public Paging() { }

        public Paging(int page, int pageSize)
        {
            _page = page;
            _pageSize = pageSize;
        }

        public int Page
        {
            get { return Math.Max(0, _page); }
            set { _page = value; }
        }

        public int PageSize
        {
            get { return _pageSize <= 0 ? 1000 : _pageSize; }
            set { _pageSize = value; }
        }

        public static Paging Default => new Paging(GlobalConstant.DefaultPageIndex, GlobalConstant.DefaultPageSize);
    }
}
