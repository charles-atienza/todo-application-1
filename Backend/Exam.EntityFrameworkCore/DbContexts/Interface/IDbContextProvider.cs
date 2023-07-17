using Microsoft.EntityFrameworkCore;

namespace Exam.Database.DbContexts.Interface;

public interface IDbContextProvider<TDbContext>
    where TDbContext : DbContext
{
    Task<TDbContext> GetDbContextAsync();
    TDbContext GetDbContext();
}