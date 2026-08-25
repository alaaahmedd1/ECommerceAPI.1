namespace ECommerce.API.DTOs;

public class OrderItemRequestDto
{
    public int ProductId { get; set; }
    public int Quantity { get; set; }
}

public class CreateOrderDto
{
    public int CustomerId { get; set; }
    public List<OrderItemRequestDto> Items { get; set; } = new();
    public string? CouponCode { get; set; }
}
