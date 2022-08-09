
using FormulaCar.Championships.Contracts;
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
        public async Task<IActionResult> GetPositions(CancellationToken cancellationToken)
        {
            var positions = await _serviceManager.PositionService.GetAll();

            return Ok(positions);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPosition(int id,CancellationToken cancellationToken)
        {
            var positions = await _serviceManager.PositionService.GetById(id);

            return Ok(positions);
        }


        [HttpPost]
        public async Task<IActionResult> CreatePosition([FromBody]PositionForCreationDto positionForCreationDto,CancellationToken cancellationToken)
        {
            var newPosition = await _serviceManager.PositionService.Create(positionForCreationDto);

            return Ok(newPosition);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePosition(int id, CancellationToken cancellationToken)
        {
            var forDelete =await _serviceManager.PositionService.Delete(id);
            return Ok(forDelete);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePosition(int id,[FromBody] PositionForUpdateDto positionForUpdateDto,
            CancellationToken cancellationToken)
        {
            positionForUpdateDto.Id = id;
            var updated = await _serviceManager.PositionService.Update(positionForUpdateDto);

            return Ok(updated);
        }
    }
}
