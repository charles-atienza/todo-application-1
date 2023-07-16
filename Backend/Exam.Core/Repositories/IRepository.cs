using Exam.Entities.Complementary.Interface;

namespace Exam.Repositories;

public interface IRepository<TEntity, TPrimaryKey>
    where TEntity : class, IEntity<TPrimaryKey>
{
    IQueryable<TEntity> GetAll();
    Task<IQueryable<TEntity>> GetAllAsync();
    Task<TEntity?> GetAsync(TPrimaryKey id);
    Task<TEntity> InsertAsync(TEntity entity);
    Task<TPrimaryKey> InsertAndGetIdAsync(TEntity entity);
    Task<TEntity> UpdateAsync(TEntity entity);
    Task DeleteAsync(TEntity entity);
    Task<int> SaveChangesAsync();
}

public interface IRepository<TEntity> : IRepository<TEntity, int>
    where TEntity : class, IEntity<int>
{
}