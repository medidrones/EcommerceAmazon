using AutoMapper;
using Ecommerce.Application.Exceptions;
using Ecommerce.Application.Features.Products.Queries.Vms;
using Ecommerce.Application.Persistence;
using Ecommerce.Domain;
using MediatR;

namespace Ecommerce.Application.Features.Products.Commands.DeleteProduct;

public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, ProductVm>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public DeleteProductCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<ProductVm> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var productToUpdate = await _unitOfWork.Repository<Product>().GetByIdAsync(request.ProductId);

        if(productToUpdate is null)
        {
            throw new NotFoundException(nameof(Product), request.ProductId);
        }

        productToUpdate.Status = productToUpdate.Status == ProductStatus.Inactivo ? ProductStatus.Activo : ProductStatus.Inactivo;
            
        await _unitOfWork.Repository<Product>().UpdateAsync(productToUpdate);

        return _mapper.Map<ProductVm>(productToUpdate);
    }
}