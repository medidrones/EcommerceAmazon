using Ecommerce.Domain.Common;

namespace Ecommerce.Domain;

public class OrderAddress : BaseDomainModel
{
    public string? Direccion { get; set; }
    public string? Ciudad { get; set;}
    public string? Departamento { get;}
    public string? CodigoPostal { get;}
    public string? UserName { get; set; }
    public string? Pais { get; set; }
}