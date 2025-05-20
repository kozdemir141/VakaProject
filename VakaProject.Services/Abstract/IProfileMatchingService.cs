using VakaProject.Domain.Concrete;

namespace VakaProject.Services.Abstract;

public interface IProfileMatchingService
{
    Task<bool> IsSameProfileAsync(IndividualData a, IndividualData b);
}