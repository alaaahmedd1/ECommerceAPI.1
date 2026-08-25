using ECommerce.API.DTOs;
using ECommerce.Application.DTOs;
using ECommerce.DAL.Context;
using ECommerce.DAL.Entities;
using ECommerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace ECommerce.Application.Services;

public class CustomerServices : InterfaceCustomerService
{
    private readonly AppDbContext _context;

    public CustomerServices(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Customer?> GetByIdAsync(int id)
    {
        return await _context.Customers
            .Include(c => c.Orders)
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<(Customer? Customer, string? ErrorMessage)> CreateCustomerAsync(CreateCustomerDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.FullName))
            return (null, "Full name is required.");

        if (string.IsNullOrWhiteSpace(dto.Email) || !dto.Email.Contains("@"))
            return (null, "A valid email address is required.");

        var emailExists = await _context.Customers.AnyAsync(c => c.Email.ToLower() == dto.Email.ToLower());
        if (emailExists)
            return (null, "Email is already registered.");

        var customer = new Customer
        {
            FullName = dto.FullName,
            Email = dto.Email,
            IsVip = dto.IsVip
        };

        await _context.Customers.AddAsync(customer);
        await _context.SaveChangesAsync();

        return (customer, null);
    }

    public async Task<(bool Success, string? ErrorMessage, bool IsNotFound)> UpgradeToVipAsync(int id)
    {
        var customer = await _context.Customers
            .Include(c => c.Orders)
            .FirstOrDefaultAsync(c => c.Id == id);

        if (customer == null)
            return (false, null, true);

        var totalSpent = customer.Orders
            .Where(o => o.Status == OrderStatus.Paid)
            .Sum(o => o.TotalAmount);

        if (totalSpent < 500m)
            return (false, $"Customer does not qualify for VIP. Total spend {totalSpent:C} is less than required $500.00", false);

        customer.IsVip = true;
        await _context.SaveChangesAsync();

        return (true, null, false);
    }
}