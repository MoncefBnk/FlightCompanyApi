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
    public class ClientsController : ControllerBase
    {
        private readonly ClientsService _clientsService;

        public ClientsController(ClientsService clientsService) =>
            _clientsService = clientsService;

        [HttpGet]
        public async Task<List<Client>> Get() =>
            await _clientsService.GetAsync();

        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<Client>> Get(string id)
        {
            var client = await _clientsService.GetAsync(id);

            if (client is null)
            {
                return NotFound();
            }

            return client;
        }

        [HttpPost]
        public async Task<IActionResult> Post(Client newClient)
        {
            await _clientsService.CreateAsync(newClient);

            return CreatedAtAction(nameof(Get), new { id = newClient.Id }, newClient);
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, Client updatedClient)
        {
            var client = await _clientsService.GetAsync(id);

            if (client is null)
            {
                return NotFound();
            }

            updatedClient.Id = client.Id;

            await _clientsService.UpdateAsync(id, updatedClient);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var client = await _clientsService.GetAsync(id);

            if (client is null)
            {
                return NotFound();
            }

            await _clientsService.RemoveAsync(id);

            return NoContent();
        }
    }
}
