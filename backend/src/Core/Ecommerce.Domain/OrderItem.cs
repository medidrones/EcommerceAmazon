using System.ComponentModel.DataAnnotations.Schema;
using Ecommerce.Domain.Common;

namespace Ecommerce.Domain;

public class OrderItem : BaseDomainModel
{ 
    [Column(TypeName = "DECIMAL(10,2)")]
    public decimal Precio { get; set; } 
    
    public int ProductId { get; set; }
    public int Cantidad { get; set; }    
    public int OrderId { get; set; }
    public int ProductItemId { get; set; }
    public string? ProductNombre { get; set; }
    public string? ImageUrl { get; set; }

    public Product? Product { get; set; }
    public Order? Order { get; set; }   
}