using Databases;
using Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Services.Helper.Extensions;
using Databases.Interfaces;

namespace Repositories
{
    public class Repository<T, TKey> : IRepository<T, TKey>
        where T : AggregateRoot<TKey>
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IDateTimeProvider _dateTimeProvider;

        protected DbSet<T> DbSet => _dbContext.Set<T>();

        public IUnitOfWork UnitOfWork
        {
            get
            {
                return _dbContext;
            }
        }

        public Repository(ApplicationDbContext dbContext, IDateTimeProvider dateTimeProvider)
        {
            _dbContext = dbContext;
            _dateTimeProvider = dateTimeProvider;
        }

        public void AddOrUpdate(T entity)
        {
            if (entity.Key.Equals(default(TKey)))
            {
                entity.CreatedAt = _dateTimeProvider.OffsetUtcNow.ToUnixTimeMilliseconds();
                DbSet.Add(entity);
            }
            else
            {
                entity.UpdatedAt = _dateTimeProvider.OffsetUtcNow.ToUnixTimeMilliseconds();
            }
        }

        public void Add(T entity)
        {
            var now = _dateTimeProvider.OffsetUtcNow.ToUnixTimeMilliseconds();
            entity.CreatedAt = now;
            entity.UpdatedAt = now;
            DbSet.Add(entity);
        }

        public void AddRange(List<T> entities)
        {
            var now = _dateTimeProvider.OffsetUtcNow.ToUnixTimeMilliseconds();
            entities.ForEach(entity => 
            {
                entity.CreatedAt = now;
                entity.UpdatedAt = now;
            });
            DbSet.AddRange(entities);
        }

        public async Task AddRangeAsync(List<T> entities)
        {
            var now = _dateTimeProvider.OffsetUtcNow.ToUnixTimeMilliseconds();
            entities.ForEach(entity => 
            {
                entity.CreatedAt = now;
                entity.UpdatedAt = now;
            });
            await DbSet.AddRangeAsync(entities);
        }

        public void Update(T entity)
        {
            entity.UpdatedAt = DateTime.UtcNow.ToTimeStamp();
            DbSet.Update(entity);
        }

        public void UpdateRange(List<T> entities)
        {
            var updatedAt = DateTime.UtcNow.ToTimeStamp();
            entities.ForEach(entity =>
            {
                entity.UpdatedAt = updatedAt;
            });
            DbSet.UpdateRange(entities);
        }

        public void Update(T entity, List<Expression<Func<T, object>>> propertyList) 
        {
            _dbContext.Attach<T>(entity);
            foreach (Expression<Func<T, object>> property in propertyList)
                _dbContext.Entry<T>(entity).Property(property).IsModified = true;
        }

        public void Delete(T entity)
        {
            DbSet.Remove(entity);
        }

        public void DeleteRange(List<T> entities)
        {
            DbSet.RemoveRange(entities);
        }

        public void Detached(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Detached;
        }

        public void MarkDeleted(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Deleted;
        }

        public void MarkDeletedRange(List<T> entities)
        {
            entities.ForEach(entity =>
            {
                _dbContext.Entry(entity).State = EntityState.Deleted;
            });
        }

        public void MarkAdded(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Added;
        }

        public void MarkAddedRange(List<T> entities)
        {
            entities.ForEach(entity =>
            {
                _dbContext.Entry(entity).State = EntityState.Added;
            });
        }

        public IQueryable<T> GetAll()
        {
            return _dbContext.Set<T>();
        }

        protected DbSet<ChildSet> CreateChildDbSet<ChildSet>() where ChildSet : class
        {
            return _dbContext.Set<ChildSet>();
        }
    }
}
