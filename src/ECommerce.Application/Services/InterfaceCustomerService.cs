using ECommerce.API.DTOs;
using ECommerce.DAL.Entities;
using ECommerce.Application.DTOs;
using ECommerce.Domain.Entities;

namespace ECommerce.Application.Services;

public interface InterfaceCustomerService
{
    Task<Customer?> GetByIdAsync(int id);
    Task<(Customer? Customer, string? ErrorMessage)> CreateCustomerAsync(CreateCustomerDto dto);
    Task<(bool Success, string? ErrorMessage, bool IsNotFound)> UpgradeToVipAsync(int id);
}