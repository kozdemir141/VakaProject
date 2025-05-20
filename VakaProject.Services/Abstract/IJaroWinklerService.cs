namespace VakaProject.Services.Abstract;

public interface IJaroWinklerService
{
    Task<double> ComputeSimilarityAsync(string a, string b);
}