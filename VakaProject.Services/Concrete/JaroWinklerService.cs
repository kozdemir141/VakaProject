using System.Net.Http.Json;
using VakaProject.Domain.Concrete;
using VakaProject.Domain.Dtos;
using VakaProject.Services.Abstract;

namespace VakaProject.Services.Concrete;

public class JaroWinklerService : IJaroWinklerService
{
    private readonly HttpClient _client;

    public JaroWinklerService(IHttpClientFactory httpFactory)
    {
        _client = httpFactory.CreateClient("SimilarityApi");
    }

    public async Task<double> ComputeSimilarityAsync(string a, string b)
    {
        var req = new JaroWinklerRequest { First = a, Second = b };
        var resp = await _client.PostAsJsonAsync("api/similarity/jarowinkler", req);
        resp.EnsureSuccessStatusCode();
        var jw = await resp.Content.ReadFromJsonAsync<JaroWinklerResponse>();
        return jw?.Score ?? 0;
    }
}