using AutoMapper;
using Ecommerce.Application.Features.Orders.Vms;
using Ecommerce.Application.Features.Shared.Queries;
using Ecommerce.Application.Persistence;
using Ecommerce.Application.Specifications.Orders;
using Ecommerce.Domain;
using MediatR;

namespace Ecommerce.Application.Features.Orders.Queries.PaginationOrdersQuery;

public class PaginationOrdersQueryHandler : IRequestHandler<PaginationOrdersQuery, PaginationVm<OrderVm>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public PaginationOrdersQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<PaginationVm<OrderVm>> Handle(PaginationOrdersQuery request, CancellationToken cancellationToken)
    {
        var orderSpecificationParams = new OrderSpecificationParams
        {
            PageIndex = request.PageIndex,
            PageSize = request.PageSize,
            Search = request.Search,
            Sort = request.Sort,
            Id =request.Id,
            Username =request.Username
        };

        var spec = new OrderSpecification(orderSpecificationParams);
        var orders = await _unitOfWork.Repository<Order>().GetAllWithSpec(spec);

        var specCount = new OrderForCountingSpecification(orderSpecificationParams);
        var totalOrders = await _unitOfWork.Repository<Order>().CountAsync(specCount);

        var rounded = Math.Ceiling(Convert.ToDecimal(totalOrders) / Convert.ToDecimal(request.PageSize));
        var totalPages = Convert.ToInt32(rounded);

        var data = _mapper.Map<IReadOnlyList<Order>, IReadOnlyList<OrderVm>>(orders);
        var ordersByPage = orders.Count();

        var pagination = new PaginationVm<OrderVm>
        {
            Count = totalOrders,
            Data = data,
            PageCount = totalPages,
            PageIndex = request.PageIndex,
            PageSize = request.PageSize,
            ResultByPage = ordersByPage
        };

        return pagination;
    }
}