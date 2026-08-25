using ECommerce.API.DTOs;
using ECommerce.Application.DTOs;
using ECommerce.DAL.Context;
using ECommerce.DAL.Entities;
using ECommerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace ECommerce.Application.Services;

public class ProductServices : InterfaceProducts
{
    private readonly AppDbContext _context;

    public ProductServices(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Product>> GetAllAsync()
    {
        return await _context.Products.ToListAsync();
    }

    public async Task<Product?> GetByIdAsync(int id)
    {
        return await _context.Products.FindAsync(id);
    }

    public async Task<(Product? Product, string? ErrorMessage)> CreateAsync(CreateProductDto dto)
    {
        if (dto.Price <= 0)
            return (null, "Product price must be greater than zero.");

        if (dto.StockQuantity < 0)
            return (null, "Stock quantity cannot be negative.");

        var skuExists = await _context.Products.AnyAsync(p => p.SKU.ToLower() == dto.SKU.ToLower());
        if (skuExists)
            return (null, $"Product with SKU '{dto.SKU}' already exists.");

        var product = new Product
        {
            Name = dto.Name,
            SKU = dto.SKU.ToUpper(),
            Price = dto.Price,
            StockQuantity = dto.StockQuantity
        };

        await _context.Products.AddAsync(product);
        await _context.SaveChangesAsync();

        return (product, null);
    }

    public async Task<(bool Success, string? ErrorMessage, bool IsNotFound)> UpdateAsync(int id, Product product)
    {
        var existing = await _context.Products.FindAsync(id);
        if (existing == null)
            return (false, $"Product with ID {id} not found.", true);

        if (product.Price <= 0)
            return (false, "Price must be positive.", false);

        existing.Name = product.Name;
        existing.SKU = product.SKU;
        existing.Price = product.Price;
        existing.StockQuantity = product.StockQuantity;

        _context.Products.Update(existing);
        await _context.SaveChangesAsync();

        return (true, null, false);
    }

    public async Task<(bool Success, string? ErrorMessage)> DeleteAsync(int id)
    {
        var product = await _context.Products.FindAsync(id);
        if (product == null)
            return (false, $"Product with ID {id} not found.");

        _context.Products.Remove(product);
        await _context.SaveChangesAsync();

        return (true, null);
    }
}