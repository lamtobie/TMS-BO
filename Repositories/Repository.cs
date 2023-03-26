using Databases;
using Databases.Interfaces;
using Microsoft.EntityFrameworkCore;
using Repositories;
using System.Linq.Expressions;


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
                DbSet.Add(entity);
        }

        public void Add(T entity)
        {
            DbSet.Add(entity);
        }

        public void AddRange(List<T> entities)
        {
            DbSet.AddRange(entities);
        }

        public async Task AddRangeAsync(List<T> entities)
        {
            await DbSet.AddRangeAsync(entities);
        }

        public void Update(T entity)
        {
            DbSet.Update(entity);
        }

        public void UpdateRange(List<T> entities)
        {
            entities.ForEach(entity =>
            {
                _dbContext.Entry(entity).State = EntityState.Modified;
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
