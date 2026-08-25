using ECommerce.API.DTOs;
using ECommerce.Application.DTOs;
using ECommerce.DAL.Entities;
using ECommerce.Domain.Entities;

namespace ECommerce.Application.Services;

public interface InterfaceProducts
{
    Task<List<Product>> GetAllAsync();
    Task<Product?> GetByIdAsync(int id);
    Task<(Product? Product, string? ErrorMessage)> CreateAsync(CreateProductDto dto);
    Task<(bool Success, string? ErrorMessage, bool IsNotFound)> UpdateAsync(int id, Product product);
    Task<(bool Success, string? ErrorMessage)> DeleteAsync(int id);
}