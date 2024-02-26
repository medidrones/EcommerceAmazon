using AutoMapper;
using Ecommerce.Application.Features.Reviews.Queries.Vms;
using Ecommerce.Application.Persistence;
using Ecommerce.Domain;
using MediatR;

namespace Ecommerce.Application.Features.Reviews.Commands.CreateReview;

public class CreateReviewCommandHandler : IRequestHandler<CreateReviewCommand, ReviewVm>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public CreateReviewCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<ReviewVm> Handle(CreateReviewCommand request, CancellationToken cancellationToken)
    {
        var reviewEntity = new Review 
        {
            Comentario = request.Comentario,
            Rating = request.Rating,
            Nombre = request.Nombre,
            ProductId = request.ProductId
        };

         _unitOfWork.Repository<Review>().AddEntity(reviewEntity);

         var resultado = await _unitOfWork.Complete();

         if(resultado <=0 )
         {
            throw new Exception("No se pudo guardar el comentario");
         }

        return _mapper.Map<ReviewVm>(reviewEntity);
    }
}
