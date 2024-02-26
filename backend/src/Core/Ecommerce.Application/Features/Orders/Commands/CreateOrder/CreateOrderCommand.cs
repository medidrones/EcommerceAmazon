using Ecommerce.Application.Features.Orders.Vms;
using MediatR;

namespace Ecommerce.Application.Features.Orders.Commands.CreateOrder;

public class CreateOrderCommand : IRequest<OrderVm>
{
    public Guid? ShoppingCartId { get; set; }
}