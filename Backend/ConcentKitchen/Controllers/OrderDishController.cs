using Microsoft.AspNetCore.Mvc;
using ConcentKitchen.Models;
using ConcentKitchen.Services;

namespace ConcentKitchen.Controllers;

[ApiController]
[Route("order/dish")]
public class OrderDishController : Controller
{
    private readonly IOrderDishService _service;
    public OrderDishController(IOrderDishService service)
    {
        _service = service;
    }

    [HttpPost()]
    public async Task<IActionResult> CreateOrderDish([FromBody] OrderDish orderdish)
    {
        try
        {
          var output = await _service.CreateOrderDish(orderdish);
          return CreatedAtAction("GetOrderDish", new { id = output.Id }, output);
        }
        catch (InvalidOperationException ex)
        {
          return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteOrderDish(Guid id)
    {
        try
        {
            await _service.DeleteOrderDish(id);
            return NoContent();
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }
    
    [HttpGet()]
    public async Task<IActionResult> GetAllOrderDishes()
    {
        try
        {
            var orderDishes = await _service.GetAllOrderDishes();
            return Ok(orderDishes);
        }
        catch (InvalidOperationException ex)
        {
            return NotFound(ex.Message);
        }
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetOrderDish(string id)
    {
        try
        {
            var orderDish = await _service.GetOrderDish(id);
            return Ok(orderDish);
        }
        catch (InvalidOperationException ex)
        {
            return NotFound(ex.Message);
        }
    }
}