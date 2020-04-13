using System.Collections.Generic;
using System.Linq;

namespace QLector.Application.Core
{
    public class PagedResponse<TSingleResult> : Response<IEnumerable<TSingleResult>>
    {
        public int PageSize { get; }
        public int Page { get;  }
        public int TotalCount { get; }
        public bool HasNext { get; }

        public PagedResponse(IEnumerable<TSingleResult> data, Pager pager, int? totalCount)
        {
            Data = data;
            Page = pager.Page;

            if (Data != null && Data.Any())
            {
                PageSize = data.Count();
                TotalCount = totalCount ?? 0;
                HasNext = PageSize * Page < totalCount;
            }
        }
    }
}
