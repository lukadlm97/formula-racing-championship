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
    [Route("api/constructors")]
    public class ConstructorController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;

        public ConstructorController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetConstructors(CancellationToken cancellationToken)
        {
            var constructorDtos = await _serviceManager.ConstructorService.GetConstructors();

            return Ok(constructorDtos);
        }
    }
}
