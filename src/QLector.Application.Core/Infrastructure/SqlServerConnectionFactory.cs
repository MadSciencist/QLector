using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data.Common;

namespace QLector.Application.Core.Infrastructure
{
    public class SqlServerConnectionFactory : IDbConnectionFactory
    {
        private readonly IConfiguration _config;

        public SqlServerConnectionFactory(IConfiguration config)
        {
            _config = config;
        }

        public DbConnection Create()
        {
            var connString = _config.GetValue<string>("ConnectionString");
            return new SqlConnection(connString);
        }
    }
}
