using Ecommerce.Application.Features.Orders.Vms;
using MediatR;

namespace Ecommerce.Application.Features.Orders.Queries.GetOrdersById;

public class GetOrdersByIdQuery : IRequest<OrderVm>
{
    public int OrderId { get; set; }

    public GetOrdersByIdQuery(int orderId)
    {
        OrderId = orderId == 0 ? throw new ArgumentNullException(nameof(orderId)) : orderId;
    }
}