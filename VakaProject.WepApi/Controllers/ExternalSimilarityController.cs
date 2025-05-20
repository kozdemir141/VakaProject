using Microsoft.AspNetCore.Mvc;
using VakaProject.Domain.Concrete;
using VakaProject.Domain.Dtos;

namespace VakaProject.WepApi.Controllers;

[ApiController]
[Route("api/similarity")]
public class ExternalSimilarityController : ControllerBase
{
    private readonly F23.StringSimilarity.JaroWinkler _jw = new();

    [HttpPost("jarowinkler")]
    public ActionResult<JaroWinklerResponse> Post([FromBody] JaroWinklerRequest req)
    {
        var score = _jw.Similarity(req.First ?? "", req.Second ?? "");
        return Ok(new JaroWinklerResponse { Score = score });
    }
}