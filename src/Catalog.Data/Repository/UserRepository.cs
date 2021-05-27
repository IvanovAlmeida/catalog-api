using Catalog.Data.Context;
using Catalog.Domain.Interfaces;
using Catalog.Domain.Models;

namespace Catalog.Data.Repository
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(DataDbContext db) : base(db)
        { }
    }
}