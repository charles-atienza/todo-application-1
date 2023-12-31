﻿using Exam.Database.DbContexts;
using Exam.Database.DbContexts.Interface;
using Exam.Database.Entities.Complementary.Interface;

namespace Exam.Database.Repositories.Abstract;

/// <summary>
///     Base class for custom repositories of the application.
/// </summary>
/// <typeparam name="TEntity">Entity type</typeparam>
/// <typeparam name="TPrimaryKey">Primary key type of the entity</typeparam>
public abstract class ExamRepositoryBase<TEntity, TPrimaryKey> : RepositoryBase<ExamDbContext, TEntity, TPrimaryKey>
    where TEntity : class, IEntity<TPrimaryKey>
{
    protected ExamRepositoryBase(IDbContextProvider<ExamDbContext> dbContextProvider)
        : base(dbContextProvider)
    {
    }

    //add your common methods for all repositories
}

/// <summary>
///     Base class for custom repositories of the application.
///     This is a shortcut of <see cref="ExamRepositoryBase{TEntity,TPrimaryKey}" /> for <see cref="int" /> primary key.
/// </summary>
/// <typeparam name="TEntity">Entity type</typeparam>
public abstract class ExamRepositoryBase<TEntity> : ExamRepositoryBase<TEntity, int>
    where TEntity : class, IEntity<int>
{
    protected ExamRepositoryBase(IDbContextProvider<ExamDbContext> dbContextProvider)
        : base(dbContextProvider)
    {
    }

    //do not add any method here, add to the class above (since this inherits it)!!!
}