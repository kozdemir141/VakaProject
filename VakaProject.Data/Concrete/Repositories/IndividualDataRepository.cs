using VakaProject.Domain.Concrete;
using Microsoft.EntityFrameworkCore;
using VakaProject.Data.Abstract;
using VakaProject.Shared.Repository;

namespace VakaProject.Data.Concrete.Repositories;

public class IndividualDataRepository : EntityRepository<IndividualData>,IIndividualDataRepository
{
    public IndividualDataRepository(DbContext context) : base(context)
    {
    }
}