using FormulaCar.Championships.Contracts;
using FormulaCar.Championships.Service.Abstraction;
using Microsoft.AspNetCore.Mvc;

namespace FormulaCar.Championships.Presentation.Controllers;

[ApiController]
[Route("api/countries")]
public class CountryController : ControllerBase
{
    private readonly IServiceManager _serviceManager;

    public CountryController(IServiceManager serviceManager)
    {
        _serviceManager = serviceManager;
    }

    [HttpGet]
    public async Task<IActionResult> GetCountries(CancellationToken cancellationToken)
    {
        var countries = await _serviceManager.CountryService.GetCountries(cancellationToken);

        return Ok(countries);
    }

    [HttpPost]
    public async Task<IActionResult> CreateCountry([FromBody] CountryForCreationDto countryForCreationDto,
        CancellationToken cancellationToken)
    {
        var country = await _serviceManager.CountryService.Create(countryForCreationDto, cancellationToken);

        return Ok(country);
    }
}