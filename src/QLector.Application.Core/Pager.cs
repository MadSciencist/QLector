using Newtonsoft.Json;

namespace QLector.Application.Core
{
    public class Pager
    {
        public int Page { get; set; }
        public int PageSize { get; set; }

        [JsonIgnore]
        public int Offset { get; set; }
        [JsonIgnore]
        public int Next { get; set; }

        public Pager(int page, int pageSize = 10)
        {
            Page = page < 1 ? 1 : page;
            PageSize = pageSize < 1 ? 10 : pageSize;

            Next = pageSize;
            Offset = (Page - 1) * Next;
        }
    }
}