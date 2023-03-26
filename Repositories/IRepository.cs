using Databases;
using Databases.Interfaces;
using System.Linq.Expressions;

namespace Repositories
{
 

    public interface IRepository<TEntity, TKey>
        where TEntity : AggregateRoot<TKey>
    {
        IUnitOfWork UnitOfWork { get; }

        IQueryable<TEntity> GetAll();

        void AddOrUpdate(TEntity entity);

        void Add(TEntity entity);

        void AddRange(List<TEntity> entities);

        Task AddRangeAsync(List<TEntity> entities);
        
        void Update(TEntity entity);

        void UpdateRange(List<TEntity> entities);
        void Update(TEntity entity, List<Expression<Func<TEntity, object>>> propertyList);

        void Delete(TEntity entity);
        void DeleteRange(List<TEntity> entity);

        void Detached(TEntity entity);
    }
}
