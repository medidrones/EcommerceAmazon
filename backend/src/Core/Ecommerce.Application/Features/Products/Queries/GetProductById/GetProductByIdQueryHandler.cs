using System.Linq.Expressions;
using AutoMapper;
using Ecommerce.Application.Features.Products.Queries.Vms;
using Ecommerce.Application.Persistence;
using Ecommerce.Domain;
using MediatR;

namespace Ecommerce.Application.Features.Products.Queries.GetProductById;

public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, ProductVm>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetProductByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<ProductVm> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var includes = new List<Expression<Func<Product, object>>>();
        includes.Add(x => x.Images!);
        includes.Add(x => x.Reviews!.OrderByDescending(p => p.CreateDate));

        var product = await _unitOfWork.Repository<Product>().GetEntityAsync(
            x => x.Id == request.ProductId,
            includes,
            true);
        
        return _mapper.Map<ProductVm>(product);
    }
}