﻿using Ecommerce.Application.Contracts.Infrastructure;
using Ecommerce.Application.Features.Auths.Users.Commands.LoginUser;
using Ecommerce.Application.Features.Auths.Users.Commands.RegisterUser;
using Ecommerce.Application.Features.Auths.Users.Commands.ResetPassword;
using Ecommerce.Application.Features.Auths.Users.Commands.ResetPasswordByToken;
using Ecommerce.Application.Features.Auths.Users.Commands.SendPassword;
using Ecommerce.Application.Features.Auths.Users.Commands.UpdateAdminStatusUser;
using Ecommerce.Application.Features.Auths.Users.Commands.UpdateAdminUser;
using Ecommerce.Application.Features.Auths.Users.Commands.UpdateUser;
using Ecommerce.Application.Features.Auths.Users.Queries.GetUserByIdQuery;
using Ecommerce.Application.Features.Auths.Users.Queries.GetUserByToken;
using Ecommerce.Application.Features.Auths.Users.Queries.GetUserByUserName;
using Ecommerce.Application.Features.Auths.Users.Queries.PaginationUsers;
using Ecommerce.Application.Features.Auths.Users.Vms;
using Ecommerce.Application.Features.Shared.Queries;
using Ecommerce.Application.Models.Authorization;
using Ecommerce.Application.Models.ImageManagement;
using Ecommerce.Domain;
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

    [Authorize(Roles = Role.ADMIN)]
    [HttpPut("updateAdminUser", Name = "UpdateAdminUser")]
    [ProducesResponseType(typeof(Usuario), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<Usuario>> UpdateAdminUser([FromBody] UpdateAdminUserCommand request)
    {
        return await _mediator.Send(request);
    }

    [Authorize(Roles = Role.ADMIN)]
    [HttpPut("updateAdminStatusUser", Name = "UpdateAdminStatusUser")]
    [ProducesResponseType(typeof(Usuario), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<Usuario>> UpdateAdminStatusUser([FromBody] UpdateAdminStatusUserCommand request)
    {
        return await _mediator.Send(request);
    }

    [Authorize(Roles = Role.ADMIN)]
    [HttpGet("{id}", Name = "GetUsuarioById")]
    [ProducesResponseType(typeof(AuthResponse), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<AuthResponse>> GetUsuarioById(string id)
    {
        var query = new GetUserByIdQuery(id);

        return await _mediator.Send(query);
    }
        
    [HttpGet("", Name = "CurrentUser")]
    [ProducesResponseType(typeof(AuthResponse), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<AuthResponse>> CurrentUser()
    {
        var query = new GetUserByTokenQuery();

        return await _mediator.Send(query);
    }

    [Authorize(Roles = Role.ADMIN)]
    [HttpGet("username/{username}", Name = "GetUsuarioUserName")]
    [ProducesResponseType(typeof(AuthResponse), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<AuthResponse>> GetUsuarioUserName(string username)
    {
        var query = new GetUserByUserNameQuery(username);

        return await _mediator.Send(query);
    }

    [Authorize(Roles = Role.ADMIN)]
    [HttpGet("paginationAdmin", Name = "PaginationUser")]
    [ProducesResponseType(typeof(PaginationVm<Usuario>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<PaginationVm<Usuario>>> PaginationUser([FromQuery] PaginationUsersQuery paginationUsersQuery)
    {
        var paginationUser = await _mediator.Send(paginationUsersQuery);

        return Ok(paginationUser);
    }
}