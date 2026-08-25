using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerce.DAL.Entities;

public class Payment
{
    public int Id { get; set; }
    public int OrderId { get; set; }
    public Order? Order { get; set; }
    
    [Column(TypeName = "decimal(18,2)")]
    public decimal Amount { get; set; }
    
    public DateTime PaymentDate { get; set; }
    
    [MaxLength(100)]
    public string TransactionReference { get; set; } = string.Empty;
    
    public bool IsSuccess { get; set; }
}
