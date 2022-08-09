using FormulaCar.Championships.Service.Abstraction;
using Microsoft.AspNetCore.Mvc;

namespace FormulaCar.Championships.Presentation.Controllers;

[ApiController]
[Route("api/sectors")]
public class SectorController : ControllerBase
{
    private readonly IServiceManager _serviceManager;

    public SectorController(IServiceManager serviceManager)
    {
        _serviceManager = serviceManager;
    }

    [HttpGet]
    public async Task<IActionResult> GetSectors(CancellationToken cancellationToken)
    {
        var sectors = await _serviceManager.SectorService.GetAll(cancellationToken);

        return Ok(sectors);
    }
}