using FluentValidation;

namespace Ecommerce.Application.Features.Products.Commands.UpdateProduct;

public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductCommandValidator()
    {
        RuleFor(p => p.Nombre)
            .NotEmpty().WithMessage("El nombre no puede estar en blanco")
            .MaximumLength(50).WithMessage("El nombre no puede exceder los 50 caracteres");

        RuleFor(p => p.Descripcion).NotEmpty().WithMessage("La descripcion no puede ser nula");
        RuleFor(p => p.Stock).NotEmpty().WithMessage("El stock no puede ser nulo");
        RuleFor(p => p.Precio).NotEmpty().WithMessage("El precio no puede ser nulo");
    }
}
