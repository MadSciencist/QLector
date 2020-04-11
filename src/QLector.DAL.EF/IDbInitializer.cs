using System.Threading.Tasks;

namespace QLector.DAL.EF
{
    public interface IDbInitializer
    {
        Task Initialize();
        Task Seed();
    }
}
