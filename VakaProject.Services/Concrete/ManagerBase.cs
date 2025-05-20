using VakaProject.Data.Abstract;

namespace VakaProject.Services.Concrete;

public class ManagerBase
{
    public ManagerBase(IUnitOfWork unitOfWork)
    {
        UnitOfWork = unitOfWork;
        
    }

    protected IUnitOfWork UnitOfWork { get; }

}