using System.Data.Common;

namespace QLector.Application.Core.Infrastructure
{
    public interface IDbConnectionFactory
    {
        DbConnection Create();
    }
}
