using Ecommerce.Application.Features.Orders.Vms;
using Ecommerce.Application.Features.Shared.Queries;
using MediatR;

namespace Ecommerce.Application.Features.Orders.Queries.PaginationOrdersQuery;

public class PaginationOrdersQuery : PaginationBaseQuery, IRequest<PaginationVm<OrderVm>>
{
    public int? Id { get; set; }
    public string? Username { get; set; }
}
