using SearchAPI.Domain.Contracts;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace SearchAPI.Persistance.Repositories
{
    public class BaseRepository<T> : IAsyncRepository<T>
        where T : class
    {
        protected readonly int defaultPageSize = 10;
        protected readonly SearchAPIDbContext dbContext;

        public BaseRepository(SearchAPIDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public virtual async Task<T> GetById(int id)
        {
            return await this.dbContext.Set<T>().FindAsync(id);
        }

        public virtual async Task<IReadOnlyList<T>> ListAll(Func<T, bool> filter = null)
        {
            var query = await this.dbContext.Set<T>().ToListAsync();

            if (filter != null)
            {
                query = query.Where(filter).ToList();
            }

            return query;
        }

        public virtual async Task<IReadOnlyList<T>> List(int? take, int? skip, Expression<Func<T, bool>> filter = null)
        {
            take = take.HasValue ? (take.Value > 0 ? take.Value : this.defaultPageSize) : this.defaultPageSize;
            skip = skip.HasValue ? (skip.Value > 0 ? skip.Value : 0) : 0;

            var result = await this.dbContext.Set<T>()
                .Where(filter)
                .Skip(skip.Value)
                .Take(take.Value)
                .ToListAsync();

            return result;
        }

        public virtual async Task<T> Add(T entity, bool save = true)
        {
            await this.dbContext.Set<T>().AddAsync(entity);
            if (save)
            {
                await this.dbContext.SaveChangesAsync();
            }

            return entity;
        }

        public virtual async Task<ICollection<T>> AddRange(ICollection<T> entities, bool save = true)
        {
            await this.dbContext.Set<T>().AddRangeAsync(entities);
            if (save)
            {
                await this.dbContext.SaveChangesAsync();
            }

            return entities;
        }

        public virtual async Task<bool> Update(T entity, bool save = true)
        {
            this.dbContext.Update<T>(entity);
            return !save || await this.dbContext.SaveChangesAsync() > 0;
        }

        public virtual async Task<bool> UpdateRange(ICollection<T> entities, bool save = true)
        {
            this.dbContext.UpdateRange(entities);
            return !save || await this.dbContext.SaveChangesAsync() > 0;
        }

        public virtual async Task<bool> Delete(T entity, bool save = true)
        {
            this.dbContext.Set<T>().Remove(entity);
            return !save || await this.dbContext.SaveChangesAsync() > 0;
        }

        public virtual async Task<bool> DeleteRange(ICollection<T> entities, bool save = true)
        {
            this.dbContext.Set<T>().RemoveRange(entities);
            return !save || await this.dbContext.SaveChangesAsync() > 0;
        }
    }
}
