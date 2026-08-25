using ECommerce.API.DTOs;
using ECommerce.Application.DTOs;
using ECommerce.DAL.Entities;
using ECommerce.Domain.Entities;

namespace ECommerce.Application.Services;

public interface InterfaceOrders
{
    Task<Order?> GetOrderByIdAsync(int id);
    Task<List<Order>> GetCustomerOrdersAsync(int customerId);
    Task<(bool Success, string? ErrorMessage, bool IsNotFound)> CancelOrderAsync(int id);
    Task<(object? Result, string? ErrorMessage, int StatusCode)> CheckoutAsync(CreateOrderDto request);
}