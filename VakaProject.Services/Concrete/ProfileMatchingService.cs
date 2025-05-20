using VakaProject.Domain.Concrete;
using VakaProject.Services.Abstract;

namespace VakaProject.Services.Concrete;

public class ProfileMatchingService : IProfileMatchingService
{
    private readonly ILevenshteinService  _levService;
    private readonly IJaroWinklerService  _jwService;
    private const   double Threshold = 0.85;

    public ProfileMatchingService(
        ILevenshteinService levService,
        IJaroWinklerService jwService)
    {
        _levService = levService;
        _jwService = jwService;
    }

    public async Task<bool> IsSameProfileAsync(IndividualData a, IndividualData b)
    {
        if (a.IdentityNumber != 0 &&
            a.IdentityNumber == b.IdentityNumber)
            return true;

        var nameA = $"{a.FirstName} {a.MiddleName} {a.LastName}".Trim();
        var nameB = $"{b.FirstName} {b.MiddleName} {b.LastName}".Trim();
        double nameScore = _levService.ComputeSimilarity(nameA, nameB);

        double placeScore = _levService.ComputeSimilarity(a.BirthPlace, b.BirthPlace);

        double dateScore = Math.Abs((a.BirthDate - b.BirthDate).TotalDays) <= 2
            ? 1.0 : 0.0;

        double natScore = string.Equals(a.Nationality, b.Nationality,
            StringComparison.OrdinalIgnoreCase)
            ? 1.0 : 0.0;

        double total = nameScore  * 0.50
                       + placeScore * 0.20
                       + dateScore  * 0.15
                       + natScore   * 0.15;

        return total >= Threshold;
    }
}