using MediatR;
using System.Security.Claims;

namespace QLector.Application.Core
{
    public class PagedQueryRequest<TRequest, TResponseRow> : IRequest<PagedResponse<TResponseRow>>, IApplicationRequest<TRequest>
    {
        public TRequest Data { get; }
        public ClaimsPrincipal Principal { get; }
        public Pager Pager { get; private set; }

        public PagedQueryRequest(TRequest data, ClaimsPrincipal claimsPrincipal)
        {
            Data = data;
            Principal = claimsPrincipal;
        }

        public void CreatePager(int page, int pageSize = 10)
        {
            if (page == 0) page = 1;
            if (pageSize == 0) pageSize = 10;

            Pager = new Pager(page, pageSize);
        }
    }
}