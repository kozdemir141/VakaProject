namespace VakaProject.Data.Abstract;

public interface IUnitOfWork : IDisposable
{
    IDataProfileRepository DataProfile { get; }
    
    IIndividualDataRepository IndividualData { get; }
    
    Task<int> SaveAsync();
}