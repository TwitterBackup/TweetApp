using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TwitterBackup.Models.Contracts;

namespace TwitterBackup.Data.Repository
{
    public class EfRepository<TEntity> : IRepository<TEntity> where TEntity : class, IDeletable
    {
        private readonly DbContext Context;
        private readonly DbSet<TEntity> TEntities;

        public EfRepository(DbContext context, DbSet<TEntity> tentities)
        {
            this.Context = context ?? throw new ArgumentNullException(nameof(context));
            this.TEntities = this.TEntities ?? throw new ArgumentNullException(nameof(EfRepository<TEntity>.TEntities));
        }

        public TEntity Get(int id)
        {
            return TEntities.Find(id);
        }

        public async Task<TEntity> GetAsync(int id)
        {
            return await TEntities.FindAsync(id);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return Enumerable.ToList(TEntities);
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await EntityFrameworkQueryableExtensions.ToListAsync(TEntities);
        }

        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return TEntities.Where(predicate);
        }

        public async Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await TEntities.Where(predicate).ToListAsync();
        }

        public TEntity SingleOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            return TEntities.SingleOrDefault(predicate);
        }

        public void Add(TEntity entity)
        {
            TEntities.Add(entity);
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            TEntities.Add(entity);
            await Context.SaveChangesAsync();
            return entity;
        }

        public void AddRange(IEnumerable<TEntity> entities)
        {
            TEntities.AddRange(entities);
        }

        public void Remove(TEntity entity)
        {
            TEntities.Remove(entity);
        }

        public async Task RemoveAsync(TEntity entity)
        {
            TEntities.Remove(entity);
            await Context.SaveChangesAsync();

        }

        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            TEntities.RemoveRange(entities);
        }

        public async Task RemoveRangeAsync(IEnumerable<TEntity> entities)
        {
            Context.Set<TEntity>().RemoveRange(entities);
            await Context.SaveChangesAsync();
        }
    }

}

