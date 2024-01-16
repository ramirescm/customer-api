using System.Linq.Expressions;
using Customer.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Customer.Infra.Repositories;

public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
{
    protected readonly CustomerContext _context;
    protected readonly DbSet<TEntity> _dbSet;

    protected Repository(CustomerContext context)
    {
        _context = context;
        _dbSet = _context.Set<TEntity>();
    }
    
    public virtual async Task<TEntity> AddAsync(TEntity entity, CancellationToken ct = default)
    {
        await _context.AddAsync(entity, ct);
        return entity;
    }
    
    public virtual async Task<TEntity?> FindFirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken ct = default)
    {
        return await _dbSet.Where(predicate).FirstOrDefaultAsync(ct);
    }
}