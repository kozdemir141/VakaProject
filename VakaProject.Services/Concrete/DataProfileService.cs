using VakaProject.Data.Abstract;
using VakaProject.Domain.Concrete;
using VakaProject.Services.Abstract;

namespace VakaProject.Services.Concrete;

public class DataProfileService : ManagerBase,IDataProfileService
{
    public DataProfileService(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }

    public async Task CreateAsync(DataProfile dataProfile)
    {
        try
        {
            await UnitOfWork.DataProfile.AddAsync(dataProfile);
            await UnitOfWork.SaveAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}