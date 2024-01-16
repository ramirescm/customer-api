using Customer.Core.UoW;

namespace Customer.Infra;

public class UnitOfWork : IUnitOfWork
{
    private readonly CustomerContext _context;
    public UnitOfWork(CustomerContext context)
    {
        _context = context;
    }

    public bool Commit()
    {
        return _context.SaveChanges() > 0;
    }

    public async Task<bool> CommitAsync(CancellationToken ct)
    {
        return await _context.SaveChangesAsync(ct) > 0;
    }
    public void BeginTransaction()
    {
        _context.Database.BeginTransaction();
    }

    public void CommitTransaction()
    {
        _context.Database.CommitTransaction();
    }

    public void RollbackTransaction()
    {
        _context.Database.RollbackTransaction();
    }

    public void DisposeTransaction()
    {
        _context.Database.CurrentTransaction?.Dispose();
    }
}