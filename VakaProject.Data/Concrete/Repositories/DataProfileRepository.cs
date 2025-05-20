using VakaProject.Domain.Concrete;
using Microsoft.EntityFrameworkCore;
using VakaProject.Data.Abstract;
using VakaProject.Shared.Repository;

namespace VakaProject.Data.Concrete.Repositories;

public class DataProfileRepository : EntityRepository<DataProfile>,IDataProfileRepository
{
    public DataProfileRepository(DbContext context) : base(context)
    {
    }
}