using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerce.DAL.Entities;

public class Coupon
{
    public int Id { get; set; }
    
    [Required]
    [MaxLength(50)]
    public string Code { get; set; } = string.Empty;
    
    [Column(TypeName = "decimal(5,2)")]
    public decimal DiscountPercentage { get; set; }
    
    public bool IsActive { get; set; }
}
