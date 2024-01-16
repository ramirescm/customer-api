using System.Linq.Expressions;

namespace Customer.Core.Repositories;

public interface IRepository<TEntity> where TEntity : class
{
    Task<TEntity> AddAsync(TEntity entity, CancellationToken ct = default);
    Task<TEntity?> FindFirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken ct = default);
}