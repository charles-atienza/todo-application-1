﻿using System.Collections.Concurrent;
using System.Linq.Expressions;
using Exam.DbContexts.Interface;
using Exam.Entities.Complementary.Interface;
using Exam.Extensions;
using Exam.Helper;
using Microsoft.EntityFrameworkCore;

namespace Exam.Repositories.Base;

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
        return await FirstOrDefaultAsync(id).ConfigureAwait(false);
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

    public virtual Task DeleteAsync(TEntity entity)
    {
        Delete(entity);
        return Task.CompletedTask;
    }

    public virtual Task<TEntity?> FirstOrDefaultAsync(TPrimaryKey id)
    {
        var query = GetAll().FirstOrDefault(CreateEqualityExpressionForId(id)) ?? null;
        return Task.FromResult(query);
    }

    public virtual async Task<TEntity> InsertAsync(TEntity entity)
    {
        _ = GetTable().Add(entity).Entity;
        return await Task.FromResult(entity).ConfigureAwait(false);
    }

    public virtual TEntity Update(TEntity entity)
    {
        AttachIfNot(entity);
        GetContext().Entry(entity).State = EntityState.Modified;
        return entity;
    }

    public virtual Task<TEntity> UpdateAsync(TEntity entity)
    {
        entity = Update(entity);
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

    protected virtual void Delete(TEntity entity)
    {
        AttachIfNot(entity);
        GetTable().Remove(entity);
    }

    #endregion protected methods
}