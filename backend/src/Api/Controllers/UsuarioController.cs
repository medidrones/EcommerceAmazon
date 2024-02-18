using Ecommerce.Application.Contracts.Infrastructure;
using Ecommerce.Application.Features.Auths.Users.Commands.LoginUser;
using Ecommerce.Application.Features.Auths.Users.Commands.RegisterUser;
using Ecommerce.Application.Features.Auths.Users.Commands.ResetPassword;
using Ecommerce.Application.Features.Auths.Users.Commands.ResetPasswordByToken;
using Ecommerce.Application.Features.Auths.Users.Commands.SendPassword;
using Ecommerce.Application.Features.Auths.Users.Commands.UpdateUser;
using Ecommerce.Application.Features.Auths.Users.Vms;
using Ecommerce.Application.Models.ImageManagement;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Ecommerce.Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class UsuarioController : ControllerBase
{
    private IMediator _mediator;
    private IManageImageService _manageImageService;

    public UsuarioController(IMediator mediator, IManageImageService manageImageService)
    {
        _mediator = mediator;
        _manageImageService = manageImageService;
    }

    [AllowAnonymous]
    [HttpPost("login", Name = "Login")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public async Task<ActionResult<AuthResponse>> Login([FromBody] LoginUserCommand request)
    {
        return await _mediator.Send(request);
    }

    [AllowAnonymous]
    [HttpPost("register", Name = "Register")]
    [ProducesResponseType((int) HttpStatusCode.OK)]
    public async Task<ActionResult<AuthResponse>> Register([FromForm] RegisterUserCommand request)
    {
        if (request.Foto is not null) 
        {
            var resultImage = await _manageImageService.UploadImage(new ImageData
            {
                ImageStream = request.Foto!.OpenReadStream(),
                Nombre = request.Foto.Name
            });

            request.FotoId = resultImage.PublicId;
            request.FotoUrl = resultImage.Url;
        }

        return await _mediator.Send(request);
    }

    [AllowAnonymous]
    [HttpPost("forgotPassword", Name = "ForgotPassword")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public async Task<ActionResult<string>> ForgotPassword([FromBody] SendPasswordCommand request)
    {
        return await _mediator.Send(request);
    }

    [AllowAnonymous]
    [HttpPost("resetPassword", Name = "ResetPassword")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public async Task<ActionResult<string>> ResetPassword([FromBody] ResetPasswordByTokenCommand request)
    {
        return await _mediator.Send(request);
    }
        
    [HttpPost("updatePassword", Name = "UpdatePassword")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public async Task<ActionResult<Unit>> UpdatePassword([FromBody] ResetPasswordCommand request)
    {
        return await _mediator.Send(request);
    }

    [HttpPut("update", Name = "Update")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public async Task<ActionResult<AuthResponse>> Update([FromForm] UpdateUserCommand request)
    {
        if (request is not null)
        {
            var resultImage = await _manageImageService.UploadImage(new ImageData
            {
                ImageStream = request.Foto!.OpenReadStream(),
                Nombre = request.Foto!.Name
            });

            request.FotoId = resultImage.PublicId;
            request.FotoUrl = resultImage.Url;
        }

        return await _mediator.Send(request!);
    }
}