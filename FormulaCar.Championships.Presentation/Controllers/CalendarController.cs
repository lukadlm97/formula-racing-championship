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
    [Route("api/calendars")]
    public class CalendarController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;

        public CalendarController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetCalendars(CancellationToken cancellationToken)
        {
            var calendar = await _serviceManager.RaceweekService.GetAll();

            return Ok(calendar);
        }
    }
}
