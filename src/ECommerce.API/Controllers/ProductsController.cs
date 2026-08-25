using ECommerce.API.DTOs;
using ECommerce.Application.DTOs;
using ECommerce.Application.Services;
using ECommerce.DAL.Entities;
using ECommerce.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly InterfaceProducts _productService;

    public ProductsController(InterfaceProducts productService)
    {
        _productService = productService;
    }

    [HttpGet]
    public async Task<ActionResult<List<Product>>> GetAll()
    {
        var products = await _productService.GetAllAsync();
        return Ok(products);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Product>> GetById(int id)
    {
        var product = await _productService.GetByIdAsync(id);
        if (product == null)
            return NotFound($"Product with ID {id} not found.");

        return Ok(product);
    }

    [HttpPost]
    public async Task<ActionResult<Product>> Create([FromBody] CreateProductDto dto)
    {
        var (product, errorMessage) = await _productService.CreateAsync(dto);

        if (errorMessage != null)
            return BadRequest(errorMessage);

        return CreatedAtAction(nameof(GetById), new { id = product!.Id }, product);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] Product product)
    {
        var (success, errorMessage, isNotFound) = await _productService.UpdateAsync(id, product);

        if (isNotFound)
            return NotFound(errorMessage);

        if (!success)
            return BadRequest(errorMessage);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var (success, errorMessage) = await _productService.DeleteAsync(id);

        if (!success)
            return NotFound(errorMessage);

        return NoContent();
    }
}