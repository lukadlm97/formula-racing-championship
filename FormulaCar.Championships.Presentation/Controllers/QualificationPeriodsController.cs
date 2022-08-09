using FormulaCar.Championships.Service.Abstraction;
using Microsoft.AspNetCore.Mvc;

namespace FormulaCar.Championships.Presentation.Controllers;

[ApiController]
[Route("api/qualificationPeriods")]
public class QualificationPeriodsController : ControllerBase
{
    private readonly IServiceManager _serviceManager;

    public QualificationPeriodsController(IServiceManager serviceManager)
    {
        _serviceManager = serviceManager;
    }

    [HttpGet]
    public async Task<IActionResult> GetQualificationPeriods(CancellationToken cancellationToken)
    {
        var sectors = await _serviceManager.QualificationPeriodsService.GetAll(cancellationToken);

        return Ok(sectors);
    }
}