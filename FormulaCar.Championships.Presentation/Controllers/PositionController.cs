
using FormulaCar.Championships.Service.Abstraction;
using Microsoft.AspNetCore.Mvc;

namespace FormulaCar.Championships.Presentation.Controllers
{
    [ApiController]
    [Route("api/positions")]
    public class PositionController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;

        public PositionController(IServiceManager serviceManager) => _serviceManager = serviceManager;

        [HttpGet]
        public async Task<IActionResult> GetOwners(CancellationToken cancellationToken)
        {
            var positions = await _serviceManager.PositionService.GetAll();

            return Ok(positions);
        }
    }
}
