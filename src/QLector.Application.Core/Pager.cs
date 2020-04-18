using Newtonsoft.Json;

namespace QLector.Application.Core
{
    /// <summary>
    /// Represents request for paging
    /// </summary>
    public class Pager
    {
        /// <summary>
        /// Number of the page
        /// </summary>
        public int Page { get; }

        /// <summary>
        /// Items on page
        /// </summary>
        public int PageSize { get; }

        /// <summary>
        /// Number of rows to skip
        /// </summary>
        [JsonIgnore]
        public int Offset { get; }

        /// <summary>
        /// Number of rows to fetch
        /// </summary>
        [JsonIgnore]
        public int Next { get; }

        public Pager(int page, int pageSize = 10)
        {
            Page = page < 1 ? 1 : page;
            PageSize = pageSize < 1 ? 10 : pageSize;

            Next = pageSize;
            Offset = (Page - 1) * Next;
        }
    }
}