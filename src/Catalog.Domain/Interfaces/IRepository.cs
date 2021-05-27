using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

using Catalog.Domain.Models;

namespace Catalog.Domain.Interfaces
{
    public interface IRepository<TEntity> : IDisposable where TEntity : Entity
    {
        void Add(TEntity entity);
        Task<TEntity> GetById(int id);
        Task<IEnumerable<TEntity>> GetAll();
        void Update(TEntity entity);
        void Disable(int id);
        void Reactivate(int id);
        Task<IEnumerable<Entity>> Find(Expression<Func<TEntity, bool>> predicate);
    }
}