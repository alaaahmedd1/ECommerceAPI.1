using ECommerce.API.DTOs;
using ECommerce.Application.DTOs;
using ECommerce.Application.Services;
using ECommerce.DAL.Entities;
using ECommerce.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
    private readonly InterfaceOrders _orderService;

    public OrdersController(InterfaceOrders orderService)
    {
        _orderService = orderService;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Order>> GetOrder(int id)
    {
        var order = await _orderService.GetOrderByIdAsync(id);
        if (order == null) return NotFound();
        return Ok(order);
    }

    [HttpGet("customer/{customerId}")]
    public async Task<ActionResult<List<Order>>> GetCustomerOrders(int customerId)
    {
        var orders = await _orderService.GetCustomerOrdersAsync(customerId);
        return Ok(orders);
    }

    [HttpPost("cancel/{id}")]
    public async Task<IActionResult> CancelOrder(int id)
    {
        var (success, errorMessage, isNotFound) = await _orderService.CancelOrderAsync(id);

        if (isNotFound) return NotFound(errorMessage);
        if (!success) return BadRequest(errorMessage);

        return Ok(new { message = "Order cancelled successfully" });
    }

    [HttpPost("checkout")]
    public async Task<IActionResult> Checkout([FromBody] CreateOrderDto request)
    {
        var (result, errorMessage, statusCode) = await _orderService.CheckoutAsync(request);

        if (statusCode == 404) return NotFound(errorMessage);
        if (statusCode == 400) return BadRequest(errorMessage);
        if (statusCode == 500) return StatusCode(500, errorMessage);

        return Ok(result);
    }
}