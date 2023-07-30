using Exam.Database.Complementary;
using Exam.Database.Complementary.Interface;
using Exam.Database.DbContexts.Interface;
using Exam.Database.Repositories.Interface;
using Exam.Extensions;
using Exam.Utilities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Concurrent;
using System.Linq.Expressions;

namespace Exam.Database.Repositories.Abstract;

/// <summary>
///     The class that has all additional necessary and methods for cleaner code
///     to perform on the database operations and the required db sets.
/// </summary>
public abstract class RepositoryBase<TDbContext, TEntity, TPrimaryKey> : IRepository<TEntity, TPrimaryKey>
    where TDbContext : DbContext
    where TEntity : class, IEntity<TPrimaryKey>
{
    private readonly IDbContextProvider<TDbContext> _dbContextProvider;

    /// <summary>
    ///     Constructor
    /// </summary>
    /// <param name="dbContextProvider"></param>
    protected RepositoryBase(IDbContextProvider<TDbContext> dbContextProvider)
    {
        _dbContextProvider = dbContextProvider;
    }

    // ReSharper disable once StaticMemberInGenericType
    private static ConcurrentDictionary<Type, bool> EntityIsDbQuery { get; } = new();

    #region public methods

    /// <summary>
    ///     Get the entity for the given id.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public virtual async Task<TEntity?> GetAsync(TPrimaryKey id)
    {
        return await GetAll().FirstOrDefaultAsync(CreateEqualityExpressionForId(id)).ConfigureAwait(false);
    }

    public virtual IQueryable<TEntity> GetAll()
    {
        return GetQueryable();
    }

    public virtual Task<IQueryable<TEntity>> GetAllAsync()
    {
        return Task.FromResult(GetAll());
    }

    public virtual IQueryable<TEntity> GetAllIncluding(params Expression<Func<TEntity, object>>[] propertySelectors)
    {
        var queryable = GetQueryable();
        if (propertySelectors.IsNullOrEmpty())
        {
            return queryable;
        }

        foreach (var navigationPropertyPath in propertySelectors)
        {
            queryable = queryable.Include(navigationPropertyPath);
        }

        return queryable;
    }

    /// <summary>
    ///     Soft Deletes the entity by setting the IsDeleted property to true.
    /// </summary>
    /// <param name="entity"></param>
    /// <typeparam name="T"></typeparam>

    public virtual Task DeleteAsync(TEntity entity, long? userId = null)
    {
        Delete(entity, userId);
        return Task.CompletedTask;
    }

    public virtual async Task<TEntity> InsertAsync(TEntity entity, long? userId = null)
    {
        if (entity is IAuditedEntity<TPrimaryKey> auditedEntity)
        {
            auditedEntity.AddedByUserId = userId ?? 0;
            auditedEntity.AddedDate = DateTime.Now;
        }
        _ = GetTable().Add(entity).Entity;
        return await Task.FromResult(entity).ConfigureAwait(false);
    }

    public virtual async Task<bool> InsertAsync(TEntity[] entity, long? userId = null)
    {
        if (entity is IAuditedEntity<TPrimaryKey>[] auditedEntities)
        {
            foreach (var auditedEntity in auditedEntities)
            {
                auditedEntity.AddedByUserId = userId ?? 0;
                auditedEntity.AddedDate = DateTime.Now;
            }
        }

        GetTable().AddRange(entity);
        return await Task.FromResult(await GetContext().SaveChangesAsync().ConfigureAwait(false) > 0).ConfigureAwait(false);
    }

    public virtual async Task<TPrimaryKey> InsertAndGetIdAsync(TEntity entity, long? userId = null)
    {
        if (entity is IAuditedEntity<TPrimaryKey> auditedEntity)
        {
            auditedEntity.AddedByUserId = userId ?? 0;
            auditedEntity.AddedDate = DateTime.Now;
        }

        var result = GetTable().Add(entity);

        await GetContext().SaveChangesAsync().ConfigureAwait(false); // Save changes to the database
        return await Task.FromResult(result.Entity.Id).ConfigureAwait(false);
    }

    public virtual TEntity Update(TEntity entity, long? userId = null)
    {
        AttachIfNot(entity);
        if (entity is IAuditedEntity<TPrimaryKey> auditedEntity)
        {
            auditedEntity.ModifiedByUserId = userId ?? 0;
            auditedEntity.ModifiedDate = DateTime.Now;
        }
        GetContext().Entry(entity).State = EntityState.Modified;
        return entity;
    }

    public virtual Task<TEntity> UpdateAsync(TEntity entity, long? userId = null)
    {
        entity = Update(entity, userId);
        return Task.FromResult(entity);
    }

    /// <summary>
    ///     Save user changes
    /// </summary>
    /// <returns></returns>
    public virtual async Task<int> SaveChangesAsync()
    {
        var db = await _dbContextProvider.GetDbContextAsync();
        return await db.SaveChangesAsync();
    }

    #endregion public methods

    #region protected methods
    protected virtual void AttachIfNot(TEntity entity)
    {
        if (GetContext().ChangeTracker.Entries().FirstOrDefault(ent => ent.Entity == entity) == null)
        {
            GetTable().Attach(entity);
        }
    }

    protected virtual IQueryable<TEntity> GetQueryable()
    {
        if (EntityIsDbQuery.GetOrAdd(typeof(TEntity),
                _ => GetContext().GetType().GetProperties().Any(property =>
                    ReflectionHelper.IsAssignableToGenericType(property.PropertyType, typeof(DbSet<>)) &&
                    ReflectionHelper.IsAssignableToGenericType(property.PropertyType.GenericTypeArguments[0],
                        typeof(IEntity<>)) &&
                    property.PropertyType.GetGenericArguments().Any(x => x == typeof(TEntity)))))
        {
            return GetTable().AsQueryable();
        }

        return GetTable().AsQueryable();
    }

    protected virtual Expression<Func<TEntity, bool>> CreateEqualityExpressionForId(TPrimaryKey id)
    {
        var parameterExpression = Expression.Parameter(typeof(TEntity));
        var memberExpression = Expression.PropertyOrField(parameterExpression, "Id");
        var idValue = Convert.ChangeType(id, typeof(TPrimaryKey)) ?? throw new ArgumentNullException();
        var right =
            Expression.Convert(((Expression<Func<object>>)(() => idValue)).Body, memberExpression.Type);
        return Expression.Lambda<Func<TEntity, bool>>(Expression.Equal(memberExpression, right), parameterExpression);
    }

    protected virtual TDbContext GetContext()
    {
        return _dbContextProvider.GetDbContext();
    }

    protected virtual DbSet<TEntity> GetTable()
    {
        return GetContext().Set<TEntity>();
    }

    protected virtual void Delete(TEntity entity, long? deletedByUserId = null)
    {
        AttachIfNot(entity);
        if (entity is ISoftDeletedEntity<TPrimaryKey> softDeletedEntity)
        {
            softDeletedEntity.IsActive = false;
            return;
        }
        if (entity is IAuditedEntity<TPrimaryKey> obj)
        {
            obj.ModifiedByUserId = deletedByUserId;
            obj.ModifiedDate = DateTime.Now;
        }

        GetTable().Remove(entity);
    }

    #endregion protected methods
}