using FluentValidation;

namespace Ecommerce.Application.Features.Addresses.Commands.CreateAddress;

public class CreateAddressCommandValidator : AbstractValidator<CreateAddressCommand>
{
    public CreateAddressCommandValidator()
    {
        RuleFor(p => p.Direccion).NotEmpty().WithMessage("La direccion no puede ser nula.");
        RuleFor(p => p.Ciudad).NotEmpty().WithMessage("La ciudad no puede ser nula.");
        RuleFor(p => p.Departamento).NotEmpty().WithMessage("El departamento no puede ser nulo.");
        RuleFor(p => p.CodigoPostal).NotEmpty().WithMessage("El codigo postal no puede ser nulo.");
        RuleFor(p => p.Pais).NotEmpty().WithMessage("El pais no puede ser nulo.");
    }
}
