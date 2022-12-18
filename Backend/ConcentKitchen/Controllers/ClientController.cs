using Microsoft.AspNetCore.Mvc;
using ConcentKitchen.Models;
using ConcentKitchen.Services;

namespace ConcentKitchen.Controllers;

[ApiController]
[Route("client")]
public class ClientController : Controller
{
    private readonly IClientService _service;
    public ClientController(IClientService service)
    {
        _service = service;
    }

    [HttpPost()]
    public async Task<IActionResult> CreateClient([FromBody] Client client)
    {
        try
        {
            var output = await _service.CreateClient(client);
            return CreatedAtAction("GetClient", new { id = output.ClientId }, output);
        }
        catch (InvalidOperationException ex)
        {
          return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteClient(Guid id)
    {
        try
        {
            await _service.DeleteClient(id);
            return NoContent();
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }
    
    [HttpGet()]
    public async Task<IActionResult> GetAllClients()
    {
        try
        {
            var clients = await _service.GetAllClients();
            return Ok(clients);
        }
        catch (InvalidOperationException ex)
        {
            return NotFound(ex.Message);
        }
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetClient(string id)
    {
        try
        {
            var client = await _service.GetClient(id);
            return Ok(client);
        }
        catch (InvalidOperationException ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpPut()]
    public async Task<IActionResult> UpdateClient([FromBody] Client client)
    {
        try
        {
            var updatedClient = await _service.UpdateClient(client);
            return Ok(updatedClient);
        }
        catch (InvalidOperationException ex)
        {
            return NotFound(ex.Message);
        }

    }
}