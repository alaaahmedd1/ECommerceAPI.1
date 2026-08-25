using ECommerce.API.Controllers;
using ECommerce.API.DTOs;
using ECommerce.DAL.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace ECommerce.Tests;

public class LegacyControllerTests
{
    private AppDbContext GetInMemoryDbContext()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        return new AppDbContext(options);
    }

    [Fact]
    public async Task CreateProduct_WithZeroPrice_ReturnsBadRequest_RequiresDbContextSetup()
    {
        var context = GetInMemoryDbContext();
        var controller = new ProductsController(context);

        var dto = new CreateProductDto { Name = "Invalid", SKU = "INV-01", Price = 0, StockQuantity = 5 };

        var result = await controller.Create(dto);

        Assert.IsType<BadRequestObjectResult>(result.Result);
    }
}
