using Microsoft.AspNetCore.Mvc;
using ConcentKitchen.Models;
using ConcentKitchen.Services;

namespace ConcentKitchen.Controllers;

[ApiController]
[Route("dish")]
public class DishController : Controller
{
    private readonly IDishService _service;
    public DishController(IDishService service)
    {
        _service = service;
    }

    [HttpPost()]
    public async Task<IActionResult> CreateDish([FromBody] Dish dish)
    {
        try
        {
            var output = await _service.CreateDish(dish);
            return CreatedAtAction("GetDish", new { id = output.DishId }, output);
        }
        catch (InvalidOperationException ex)
        {
          return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteDish(Guid id)
    {
        try
        {
            await _service.DeleteDish(id);
            return NoContent();
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }
    
    [HttpGet()]
    public async Task<IActionResult> GetAllDishes([FromQuery] string? category)
    {
        try
        {
            var dishes = await _service.GetAllDishes(category);
            return Ok(dishes);
        }
        catch (InvalidOperationException ex)
        {
            return NotFound(ex.Message);
        }
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetDish(string id)
    {
        try
        {
            var dish = await _service.GetDish(id);
            return Ok(dish);
        }
        catch (InvalidOperationException ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpPut()]
    public async Task<IActionResult> UpdateDish([FromBody] Dish dish)
    {
        try
        {
            var updatedClient = await _service.UpdateDish(dish);
            return Ok(updatedClient);
        }
        catch (InvalidOperationException ex)
        {
            return NotFound(ex.Message);
        }

    }
}