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
    public class VolsController : ControllerBase
    {
        private readonly VolsService _volsService;

        public VolsController(VolsService volsService) =>
            _volsService = volsService;

        [HttpGet]
        public async Task<List<Vol>> Get() =>
            await _volsService.GetAsync();

        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<Vol>> Get(string id)
        {
            var vol = await _volsService.GetAsync(id);

            if (vol is null)
            {
                return NotFound();
            }

            return vol;
        }


        [HttpGet("villeDep&villeArr")]
        public async Task<ActionResult<Vol>> GetByVilles(string villeDep, string villeArr)
        {
            var vol = await _volsService.GetByVillesAsync(villeDep, villeArr);

            if (vol is null)
            {
                return NotFound();
            }

            return vol;
        }

        [HttpPost]
        public async Task<IActionResult> Post(Vol newVol)
        {
            await _volsService.CreateAsync(newVol);

            return CreatedAtAction(nameof(Get), new { id = newVol.Id }, newVol);
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, Vol updatedVol)
        {
            var vol = await _volsService.GetAsync(id);

            if (vol is null)
            {
                return NotFound();
            }

            updatedVol.Id = vol.Id;

            await _volsService.UpdateAsync(id, updatedVol);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var vol = await _volsService.GetAsync(id);

            if (vol is null)
            {
                return NotFound();
            }

            await _volsService.RemoveAsync(id);

            return NoContent();
        }
    }
}
