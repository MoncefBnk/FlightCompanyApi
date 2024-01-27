using FlightCompanyApi.Models;
using FlightCompanyApi.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace FlightCompanyApi.Controllers
{
    
    [ApiController]
    [ApiVersion("1.0")]
    [EnableCors("Policy1")]
    [Route("api/v{version:apiVersion}/[controller]")]



    [BasicAuthentication]
    public class AvionsController : ControllerBase
    {
        private readonly AvionsService _avionsService;

        public AvionsController(AvionsService avionsService) =>
            _avionsService = avionsService;

        [HttpGet]
        public async Task<List<Avion>> Get() =>
            await _avionsService.GetAsync();

        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<Avion>> Get(string id)
        {
            var avion = await _avionsService.GetAsync(id);

            if (avion is null)
            {
                return NotFound();
            }

            return avion;
        }

        [HttpPost]
        public async Task<IActionResult> Post(Avion newAvion)
        {
            await _avionsService.CreateAsync(newAvion);

            return CreatedAtAction(nameof(Get), new { id = newAvion.Id }, newAvion);
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, Avion updatedAvion)
        {
            var avion = await _avionsService.GetAsync(id);

            if (avion is null)
            {
                return NotFound();
            }

            updatedAvion.Id = avion.Id;

            await _avionsService.UpdateAsync(id, updatedAvion);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var avion = await _avionsService.GetAsync(id);

            if (avion is null)
            {
                return NotFound();
            }

            await _avionsService.RemoveAsync(id);

            return NoContent();
        }
    }
}
