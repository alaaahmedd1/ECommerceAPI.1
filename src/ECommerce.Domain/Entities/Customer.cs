using System.ComponentModel.DataAnnotations;

namespace ECommerce.DAL.Entities;

public class Customer
{
    public int Id { get; set; }
    
    [Required]
    [MaxLength(100)]
    public string FullName { get; set; } = string.Empty;
    
    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;
    
    public bool IsVip { get; set; }
    
    public List<Order> Orders { get; set; } = new();
}
