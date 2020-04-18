using MediatR;
using System.Security.Claims;

namespace QLector.Application.Core
{
    public class PagedQueryRequest<TRequest, TResponseRow> : IRequest<PagedResponse<TResponseRow>>, IApplicationRequest<TRequest>
    {
        /// <summary>
        /// Data Transfer Object
        /// </summary>
        public TRequest Data { get; }

        /// <summary>
        /// Current system principal
        /// </summary>
        public ClaimsPrincipal Principal { get; }

        /// <summary>
        /// Paging request configuration 
        /// </summary>
        public Pager Pager { get; protected set; }

        public PagedQueryRequest(TRequest data, ClaimsPrincipal claimsPrincipal, int page = 0, int pageSize = 10)
        {
            Data = data;
            Principal = claimsPrincipal;

            CreatePager(page, pageSize);
        }

        protected void CreatePager(int page, int pageSize)
        {
            if (page == 0) page = 1;
            if (pageSize == 0) pageSize = 10;

            Pager = new Pager(page, pageSize);
        }
    }
}