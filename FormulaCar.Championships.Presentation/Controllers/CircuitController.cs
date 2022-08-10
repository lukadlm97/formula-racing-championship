using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FormulaCar.Championships.Service.Abstraction;
using Microsoft.AspNetCore.Mvc;

namespace FormulaCar.Championships.Presentation.Controllers
{
    [ApiController]
    [Route("api/circuites")]
    public class CircuitController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;

        public CircuitController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetCountries(CancellationToken cancellationToken)
        {
            var circuitDtos = await _serviceManager.CircuiteService.GetAll(cancellationToken);

            return Ok(circuitDtos);
        }
    }
}
