using FormulaCar.Championships.Service.Abstraction;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormulaCar.Championships.Presentation.Controllers
{
    [ApiController]
    [Route("api/raceResults")]
    public class RaceResultController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;

        public RaceResultController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetRaceResults(CancellationToken cancellationToken)
        {
            var resultItemDtos = await _serviceManager.RaceClassificationService.GetAll();

            return Ok(resultItemDtos);
        }
    }
}
