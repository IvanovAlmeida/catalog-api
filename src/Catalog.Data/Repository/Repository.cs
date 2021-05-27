using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Catalog.Data.Context;
using Catalog.Domain.Interfaces;
using Catalog.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Data.Repository
{
    public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity, new()
    {
        protected readonly DataDbContext Db;
        protected readonly DbSet<TEntity> DbSet;

        protected Repository(DataDbContext db)
        {
            Db = db;
            DbSet = db.Set<TEntity>();
        }

        public virtual void Add(TEntity entity)
        {
            Db.Add(entity);
        }

        public virtual async Task<TEntity> GetById(int id)
        {
            return await DbSet.FindAsync(id);
        }

        public virtual async Task<IEnumerable<TEntity>> GetAll()
        {
            return await DbSet.AsNoTrackingWithIdentityResolution().ToListAsync();
        }

        public virtual void Update(TEntity entity)
        {
            DbSet.Update(entity);
        }

        public virtual void Disable(int id)
        {
            var entity = new TEntity
            {
                Id = id,
                DisabledAt = DateTime.Now
            };
            Db.Entry(entity).Property(p => p.DisabledAt).IsModified = true;
        }

        public virtual void Reactivate(int id)
        {
            var entity = new TEntity
            {
                Id = id,
                DisabledAt = null
            };
            Db.Entry(entity).Property(p => p.DisabledAt).IsModified = true;
        }

        public virtual async Task<IEnumerable<Entity>> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return await DbSet.AsNoTrackingWithIdentityResolution().Where(predicate).ToListAsync();
        }

        public void Dispose()
        {
            Db?.Dispose();
        }
    }
}