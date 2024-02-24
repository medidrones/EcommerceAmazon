using FluentValidation;

namespace Ecommerce.Application.Features.Reviews.Commands.CreateReview;

public class CreateReviewCommandValidator : AbstractValidator<CreateReviewCommand>
{
    public CreateReviewCommandValidator()
    {
        RuleFor(x => x.Nombre).NotNull().WithMessage("Nombre no permite valores nulos.");     
        RuleFor(x => x.Comentario).NotNull().WithMessage("Comentario no permite valores nulos.");     
        RuleFor(x => x.Rating).NotEmpty().WithMessage("Rating no permite valores nulos.");     
    }
}
