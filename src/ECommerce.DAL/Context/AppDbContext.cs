using ECommerce.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.DAL.Context;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Product> Products => Set<Product>();
    public DbSet<Customer> Customers => Set<Customer>();
    public DbSet<Order> Orders => Set<Order>();
    public DbSet<OrderItem> OrderItems => Set<OrderItem>();
    public DbSet<Coupon> Coupons => Set<Coupon>();
    public DbSet<Payment> Payments => Set<Payment>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Product>().HasData(
            new Product { Id = 1, Name = "Mechanical Keyboard", SKU = "TECH-MK-01", Price = 120.00m, StockQuantity = 25 },
            new Product { Id = 2, Name = "Wireless Ergonomic Mouse", SKU = "TECH-WM-02", Price = 45.50m, StockQuantity = 40 },
            new Product { Id = 3, Name = "UltraWide Monitor 34\"", SKU = "DISP-UW-03", Price = 650.00m, StockQuantity = 8 },
            new Product { Id = 4, Name = "USB-C Multiport Dock", SKU = "ACC-DK-04", Price = 85.00m, StockQuantity = 15 },
            new Product { Id = 5, Name = "Noise Cancelling Headphones", SKU = "AUD-NC-05", Price = 220.00m, StockQuantity = 12 }
        );

        modelBuilder.Entity<Customer>().HasData(
            new Customer { Id = 1, FullName = "Sarah Connor", Email = "sarah.connor@sky.net", IsVip = true },
            new Customer { Id = 2, FullName = "John Doe", Email = "john.doe@example.com", IsVip = false },
            new Customer { Id = 3, FullName = "Jane Smith", Email = "jane.smith@example.com", IsVip = false }
        );

        modelBuilder.Entity<Coupon>().HasData(
            new Coupon { Id = 1, Code = "WELCOME10", DiscountPercentage = 10.00m, IsActive = true },
            new Coupon { Id = 2, Code = "SUMMER20", DiscountPercentage = 20.00m, IsActive = true },
            new Coupon { Id = 3, Code = "EXPIRED50", DiscountPercentage = 50.00m, IsActive = false }
        );

        modelBuilder.Entity<Order>().HasData(
            new Order
            {
                Id = 1,
                CustomerId = 1,
                CreatedAt = new DateTime(2026, 1, 15, 10, 30, 0, DateTimeKind.Utc),
                Status = OrderStatus.Paid,
                Subtotal = 120.00m,
                DiscountAmount = 18.00m,
                TaxAmount = 14.28m,
                ShippingFee = 75.00m,
                TotalAmount = 191.28m
            },
            new Order
            {
                Id = 2,
                CustomerId = 2,
                CreatedAt = new DateTime(2026, 2, 1, 14, 0, 0, DateTimeKind.Utc),
                Status = OrderStatus.Pending,
                Subtotal = 45.50m,
                DiscountAmount = 0.00m,
                TaxAmount = 6.37m,
                ShippingFee = 75.00m,
                TotalAmount = 126.87m
            }
        );

        modelBuilder.Entity<OrderItem>().HasData(
            new OrderItem { Id = 1, OrderId = 1, ProductId = 1, Quantity = 1, UnitPrice = 120.00m },
            new OrderItem { Id = 2, OrderId = 2, ProductId = 2, Quantity = 1, UnitPrice = 45.50m }
        );

        modelBuilder.Entity<Payment>().HasData(
            new Payment
            {
                Id = 1,
                OrderId = 1,
                Amount = 191.28m,
                PaymentDate = new DateTime(2026, 1, 15, 10, 35, 0, DateTimeKind.Utc),
                TransactionReference = "TX-MOCK-10001",
                IsSuccess = true
            }
        );
    }
}
