using Microsoft.AspNetCore.Mvc;
using VakaProject.Domain.Concrete;
using VakaProject.Services.Abstract;

namespace VakaProject.WepApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DataProfileController : ControllerBase
{
    private readonly IDataProfileService _dataProfileService;

    public DataProfileController(IDataProfileService dataProfileService)
    {
        _dataProfileService = dataProfileService;
    }
    
    [HttpPost]
    public async Task<IActionResult> Create(DataProfile individualData)
    {
        try
        {
            await _dataProfileService.CreateAsync(individualData);
            return Ok(500);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}