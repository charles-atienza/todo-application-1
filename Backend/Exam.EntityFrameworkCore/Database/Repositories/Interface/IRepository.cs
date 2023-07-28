using Exam.Database.Complementary.Interface;

namespace Exam.Database.Repositories.Interface;

public interface IRepository<TEntity, TPrimaryKey>
    where TEntity : class, IEntity<TPrimaryKey>
{
    IQueryable<TEntity> GetAll();
    Task<IQueryable<TEntity>> GetAllAsync();
    Task<TEntity?> GetAsync(TPrimaryKey id);
    Task<TEntity> InsertAsync(TEntity entity, long? addedByUserId = null);
    Task<TPrimaryKey> InsertAndGetIdAsync(TEntity entity, long? addedByUserId = null);
    Task<TEntity> UpdateAsync(TEntity entity, long? updatedByUserId = null);
    Task DeleteAsync(TEntity entity, long? deletedByUserId = null);
    Task<int> SaveChangesAsync();
}

public interface IRepository<TEntity> : IRepository<TEntity, int>
    where TEntity : class, IEntity<int>
{
}