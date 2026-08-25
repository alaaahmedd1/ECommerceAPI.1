using ECommerce.Application.DTOs;
using ECommerce.Application.Services;
using ECommerce.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CustomersController : ControllerBase
{
    private readonly InterfaceCustomerService _customerService;

    public CustomersController(InterfaceCustomerService customerService)
    {
        _customerService = customerService;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Customer>> GetById(int id)
    {
        var customer = await _customerService.GetByIdAsync(id);
            

        if (customer == null) 
            return NotFound($"Customer with ID {id} not found.");

        return Ok(customer);
    }

    [HttpPost]
    public async Task<ActionResult<Customer>> Create([FromBody] CreateCustomerDto dto)
    {
        var (customer, errorMessage) = await _customerService.CreateCustomerAsync(dto);

        if (errorMessage != null)
            return BadRequest(errorMessage);

        return CreatedAtAction(nameof(GetById), new { id = customer!.Id }, customer);

        
    }

    [HttpPost("{id}/upgrade-vip")]
    public async Task<IActionResult> UpgradeToVip(int id)
    {
        var (success, errorMessage, isNotFound) = await _customerService.UpgradeToVipAsync(id);

        if (isNotFound)
            return NotFound();

        if (!success)
            return BadRequest(errorMessage);

        return Ok(new { message = "Customer upgraded to VIP successfully." });
    }
}
