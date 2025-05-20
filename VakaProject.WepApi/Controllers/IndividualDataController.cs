using Microsoft.AspNetCore.Mvc;
using VakaProject.Domain.Concrete;
using VakaProject.Domain.Dtos;
using VakaProject.Services.Abstract;

namespace VakaProject.WepApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class IndividualDataController : ControllerBase
{
    
    private readonly IIndividualDataService individualDataService;

    public IndividualDataController(IIndividualDataService individualDataService)
    {
        this.individualDataService = individualDataService;
    }
    
    [HttpGet("with-profiles")]
    public async Task<IActionResult> GetWithProfiles(CancellationToken cancellationToken)
    {
        IList<IndividualWithProfileDto> result = await individualDataService.GetWithProfilesAsync(cancellationToken);

        return Ok(result);
    }
    
    [HttpPost]
    public async Task<IActionResult> Create(IndividualData individualData)
    {
        try
        {
            await individualDataService.CreateAsync(individualData);
            return Ok(500);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}