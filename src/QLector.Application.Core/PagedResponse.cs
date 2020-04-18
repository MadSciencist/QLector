using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace QLector.Application.Core
{
    /// <summary>
    /// Container of paged response
    /// </summary>
    /// <typeparam name="TSingleResult"></typeparam>
    public class PagedResponse<TSingleResult> : Response<IEnumerable<TSingleResult>>
    {
        /// <summary>
        /// Items per page
        /// </summary>
        [JsonProperty("pageSize")]
        public int PageSize { get; }

        /// <summary>
        /// Current page
        /// </summary>
        [JsonProperty("page")]
        public int Page { get;  }

        /// <summary>
        /// Total results
        /// </summary>
        [JsonProperty("totalCount")]
        public int TotalCount { get; }

        /// <summary>
        /// Has next page
        /// </summary>
        [JsonProperty("hasNext")]
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
