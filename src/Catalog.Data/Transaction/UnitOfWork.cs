using System.Threading.Tasks;
using Catalog.Data.Context;
using Catalog.Domain.Interfaces;

namespace Catalog.Data.Transaction
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataDbContext _context;

        public UnitOfWork(DataDbContext context)
        {
            _context = context;
        }

        public async Task Commit()
        {
            await _context.SaveChangesAsync();
        }

        public void Rollback()
        {
            //
        }
    }
}