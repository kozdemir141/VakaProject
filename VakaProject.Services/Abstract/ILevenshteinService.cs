namespace VakaProject.Services.Abstract;

public interface ILevenshteinService
{
    double ComputeSimilarity(string a, string b);
}