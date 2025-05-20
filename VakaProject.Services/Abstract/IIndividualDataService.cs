using VakaProject.Domain.Concrete;
using VakaProject.Domain.Dtos;

namespace VakaProject.Services.Abstract;

public interface IIndividualDataService
{
    Task CreateAsync(IndividualData individualData);

    public Task<IList<IndividualWithProfileDto>> GetWithProfilesAsync(CancellationToken cancellationToken = default);
}