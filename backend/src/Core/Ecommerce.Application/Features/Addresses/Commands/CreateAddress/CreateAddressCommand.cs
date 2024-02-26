using Ecommerce.Application.Features.Addresses.Vms;
using MediatR;

namespace Ecommerce.Application.Features.Addresses.Commands.CreateAddress;

public class CreateAddressCommand : IRequest<AddressVm>
{
    public string? Direccion { get; set; }
    public string? Ciudad { get; set; }
    public string? Departamento { get; set; }
    public string? CodigoPostal { get; set; }
    public string? Pais { get; set; }
}