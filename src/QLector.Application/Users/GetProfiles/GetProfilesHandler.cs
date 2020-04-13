using Dapper;
using QLector.Application.Core;
using QLector.Application.Users.GetProfile;
using System;
using System.Threading.Tasks;

namespace QLector.Application.Users.GetProfiles
{
    public class GetProfilesHandler : BaseQueryHandler<GetProfilesQuery, UserProfileDto>
    {
        public GetProfilesHandler(IServiceProvider services) : base(services) { }

        protected override async Task<PagedResponse<UserProfileDto>> Handle(PagedQueryRequest<GetProfilesQuery, UserProfileDto> request)
        {
            const string query = "SELECT " +
                                 "Id, UserName, Email, Created, Modified, LastLogged " +
                                 "FROM dbo.Users " +
                                 "Order By Id " +
                                 "OFFSET @Offset ROWS " +
                                 "FETCH NEXT @Next ROWS ONLY " +
                                 "SELECT COUNT(*) from dbo.Users";

            await using var conn = DbConnectionFactory.Create();
            using var multiQuery = await conn.QueryMultipleAsync(query, request.Pager);
            var results = await multiQuery.ReadAsync<UserProfileDto>();
            var totalCount = await multiQuery.ReadFirstAsync<int>();

            return new PagedResponse<UserProfileDto>(results, request.Pager, totalCount);
        }
    }
}
