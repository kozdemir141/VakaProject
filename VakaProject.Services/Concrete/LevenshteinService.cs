using VakaProject.Services.Abstract;

namespace VakaProject.Services.Concrete;

public class LevenshteinService : ILevenshteinService
{
    public double ComputeSimilarity(string a, string b)
    {
        if (string.IsNullOrEmpty(a) && string.IsNullOrEmpty(b)) return 1.0;
        if (string.IsNullOrEmpty(a) || string.IsNullOrEmpty(b)) return 0.0;

        int dist = Distance(a, b);
        int maxLen = Math.Max(a.Length, b.Length);
        return 1.0 - (dist / (double)maxLen);
    }

    private static int Distance(string s, string t)
    {
        int n = s.Length, m = t.Length;
        var d = new int[n + 1, m + 1];

        for (int i = 0; i <= n; i++) d[i, 0] = i;
        for (int j = 0; j <= m; j++) d[0, j] = j;

        for (int i = 1; i <= n; i++)
        for (int j = 1; j <= m; j++)
        {
            int cost = s[i - 1] == t[j - 1] ? 0 : 1;
            d[i, j] = Math.Min(
                Math.Min(d[i - 1, j] + 1, 
                    d[i, j - 1] + 1),  
                d[i - 1, j - 1] + cost);
        }

        return d[n, m];
    }
}