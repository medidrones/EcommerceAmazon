﻿using Ecommerce.Application.Contracts.Infrastructure;
using Ecommerce.Application.Exceptions;
using Ecommerce.Application.Models.Email;
using Ecommerce.Domain;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Text;

namespace Ecommerce.Application.Features.Auths.Users.Commands.SendPassword;

public class SendPasswordCommandHandler : IRequestHandler<SendPasswordCommand, string>
{
    private readonly IEmailService _emailService;
    private readonly UserManager<Usuario> _userManager;

    public SendPasswordCommandHandler(IEmailService emailService, UserManager<Usuario> userManager)
    {
        _emailService = emailService;
        _userManager = userManager;
    }

    public async Task<string> Handle(SendPasswordCommand request, CancellationToken cancellationToken)
    {
        var usuario = await _userManager.FindByEmailAsync(request.Email!);

        if (usuario is null) 
        {
            throw new BadRequestException("El usuario no existe.");
        }

        var token = await _userManager.GeneratePasswordResetTokenAsync(usuario);
        var plainTextBytes = Encoding.UTF8.GetBytes(token);

        token = Convert.ToBase64String(plainTextBytes);

        var emailMessage = new EmailMessage
        {
            To = request.Email,
            Body = "Resetar el password, dale click aqui.",
            Subject = "Cambiar el password"
        };

        var result = await _emailService.SendEmail(emailMessage, token);

        if (!result)
        {
            throw new Exception("No se pudo enviar el email.");
        }

        return $"Se envio el email a la cuenta {request.Email}";
    }
}
