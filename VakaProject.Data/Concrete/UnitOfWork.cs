using VakaProject.Data.Abstract;
using VakaProject.Data.Concrete.Context;
using VakaProject.Data.Concrete.Repositories;

namespace VakaProject.Data.Concrete;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;
    private IDataProfileRepository? _dataProfileRepository;
    private IIndividualDataRepository? _ındividualDataRepository;

    public UnitOfWork(ApplicationDbContext context)
    {
        _context = context;
    }

    public IDataProfileRepository DataProfile => _dataProfileRepository ??= new DataProfileRepository(_context);
    public IIndividualDataRepository IndividualData => _ındividualDataRepository ??= new IndividualDataRepository(_context);
    
    public async Task<int> SaveAsync()
    {
        return await _context.SaveChangesAsync();
    }
    
    public void Dispose()
    {
        _context.Dispose();
    }
}