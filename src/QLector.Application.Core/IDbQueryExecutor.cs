using System.Collections.Generic;
using System.Threading.Tasks;

namespace QLector.Application.Core
{
    /// <summary>
    /// Query executor
    /// </summary>
    public interface IDbQueryExecutor<T> where T : class
    {
        Task<T> GetSingle(string rawSql);
        Task<IEnumerable<T>> GetMany(string rawSql);
    }
}
