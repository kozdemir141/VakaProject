using VakaProject.Domain.Concrete;

namespace VakaProject.Services.Abstract;

public interface IDataProfileService
{
    Task CreateAsync(DataProfile dataProfile);
}