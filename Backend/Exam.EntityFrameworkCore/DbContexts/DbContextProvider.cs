using Exam.Database.DbContexts.Interface;

namespace Exam.Database.DbContexts;

public class DbContextProvider<TDbContext> : IDbContextProvider<TDbContext>
    where TDbContext : ExamDbContext
{
    private readonly TDbContext _context;

    public DbContextProvider(TDbContext context)
    {
        _context = context;
    }

    public TDbContext GetDbContext()
    {
        return _context;
    }

    public Task<TDbContext> GetDbContextAsync()
    {
        return Task.FromResult(_context);
    }
}