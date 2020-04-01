using Microsoft.EntityFrameworkCore;
using QLector.Domain.Abstractions;
using System.Threading.Tasks;

namespace QLector.DAL.EF
{
    public class EntityFrameworkUnitOfWork : IUnitOfWork
    {
        private readonly DbContext _context;

        public EntityFrameworkUnitOfWork(DbContext context)
        {
            _context = context;
        }

        public async Task Commit()
        {
            try
            {
                 await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                // TODO
                throw;
            }
            catch (DbUpdateException ex)
            {
                // TODO
                throw;
            }
        }

        public void Dispose() => _context?.Dispose();
    }
}
