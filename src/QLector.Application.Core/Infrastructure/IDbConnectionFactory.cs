using System.Data.Common;

namespace QLector.Application.Core.Infrastructure
{
    /// <summary>
    /// Factory class for creating database connections
    /// </summary>
    public interface IDbConnectionFactory
    {
        /// <summary>
        /// Creates new instance of DbConnection implementation
        /// </summary>
        /// <returns></returns>
        DbConnection Create();
    }
}
