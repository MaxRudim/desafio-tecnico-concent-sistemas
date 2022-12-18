using Microsoft.AspNetCore.Mvc;
using ConcentKitchen.Models;
using ConcentKitchen.Services;

namespace ConcentKitchen.Controllers;

[ApiController]
[Route("order")]
public class OrderController : Controller
{
    private readonly IOrderService _service;
    public OrderController(IOrderService service)
    {
        _service = service;
    }

    [HttpPost()]
    public async Task<IActionResult> CreateOrder([FromBody] Order order)
    {
        try
        {
          var output = await _service.CreateOrder(order);
          return CreatedAtAction("GetOrder", new { id = output.OrderId }, output);
        }
        catch (InvalidOperationException ex)
        {
          return BadRequest(ex.Message);
        }
        // catch (Exception err)
        // {
        //   return BadRequest(err.Message);
        // }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteOrder(Guid id)
    {
        try
        {
            await _service.DeleteOrder(id);
            return NoContent();
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }
    
    [HttpGet()]
    public async Task<IActionResult> GetAllOrders()
    {
        try
        {
            var orders = await _service.GetAllOrders();
            return Ok(orders);
        }
        catch (InvalidOperationException ex)
        {
            return NotFound(ex.Message);
        }
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetOrder(string id)
    {
        try
        {
            var order = await _service.GetOrder(id);
            return Ok(order);
        }
        catch (InvalidOperationException ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpGet("client/{id}")]
    public async Task<IActionResult> GetOrdersByClient(string id)
    {
        try
        {
            var orders = await _service.GetOrdersByClient(new Guid(id));
            return Ok(orders);
        }
        catch (InvalidOperationException ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpPut()]
    public async Task<IActionResult> UpdateOrder([FromBody] Order order)
    {
        try
        {
            var updateOrder = await _service.UpdateOrder(order);
            return Ok(updateOrder);
        }
        catch (InvalidOperationException ex)
        {
            return NotFound(ex.Message);
        }
    }
}