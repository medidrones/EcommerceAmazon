using System.Net;
using Ecommerce.Application.Contracts.Infrastructure;
using Ecommerce.Application.Features.Products.Commands.CreateProduct;
using Ecommerce.Application.Features.Products.Commands.DeleteProduct;
using Ecommerce.Application.Features.Products.Commands.UpdateProduct;
using Ecommerce.Application.Features.Products.Queries.GetProductById;
using Ecommerce.Application.Features.Products.Queries.GetProductList;
using Ecommerce.Application.Features.Products.Queries.PaginationProducts;
using Ecommerce.Application.Features.Products.Queries.Vms;
using Ecommerce.Application.Features.Shared.Queries;
using Ecommerce.Application.Models.Authorization;
using Ecommerce.Application.Models.ImageManagement;
using Ecommerce.Domain;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class ProductController : ControllerBase
{
    private IMediator _mediator;
    private IManageImageService _manageImageService;

    public ProductController(IMediator mediator, IManageImageService manageImageService)
    {
        _mediator = mediator;
        _manageImageService = manageImageService;
    }

    [AllowAnonymous]
    [HttpGet("list", Name = "GetProductList")]
    [ProducesResponseType(typeof(IReadOnlyList<ProductVm>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<IReadOnlyList<ProductVm>>> GetProductList()
    {
        var query = new GetProductListQuery();
        var productos = await _mediator.Send(query);

        return Ok(productos);
    }

    [AllowAnonymous]
    [HttpGet("pagination", Name = "PaginationProduct")]
    [ProducesResponseType(typeof(PaginationVm<ProductVm>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<PaginationVm<ProductVm>>> PaginationProduct([FromQuery] PaginationProductsQuery paginationProductsQuery)
    {
        paginationProductsQuery.Status = ProductStatus.Activo;
        var paginationProduct = await _mediator.Send(paginationProductsQuery);

        return Ok(paginationProduct);
    }

    [Authorize(Roles = Role.ADMIN)]
    [HttpGet("paginationAdmin", Name = "PaginationProductAdmin")]
    [ProducesResponseType(typeof(PaginationVm<ProductVm>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<PaginationVm<ProductVm>>> PaginationAdmin([FromQuery] PaginationProductsQuery paginationProductsQuery)
    {
        var paginationProduct = await _mediator.Send(paginationProductsQuery);

        return Ok(paginationProduct);
    }

    [AllowAnonymous]
    [HttpGet("{id}", Name = "GetProductById")]
    [ProducesResponseType(typeof(ProductVm), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<ProductVm>> GetProductById(int id)
    {
        var query = new GetProductByIdQuery(id);

        return Ok(await _mediator.Send(query));
    }

    [Authorize(Roles = Role.ADMIN)]
    [HttpPost("create", Name = "CreateProduct")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public async Task<ActionResult<ProductVm>> CreateProduct([FromForm] CreateProductCommand request)
    {
        var listFotoUrls = new List<CreateProductImageCommand>();

        if (request.Fotos is not null)
        {
            foreach (var foto in request.Fotos)
            {
                var resultImage = await _manageImageService.UploadImage(new ImageData
                {
                    ImageStream = foto.OpenReadStream(),
                    Nombre = foto.Name
                });

                var fotoCommand = new CreateProductImageCommand
                {
                    PublicCode = resultImage.PublicId,
                    Url = resultImage.Url
                };

                listFotoUrls.Add(fotoCommand);
            }

            request.ImageUrls = listFotoUrls;
        }

        return await _mediator.Send(request);
    }

    [Authorize(Roles = Role.ADMIN)]
    [HttpPut("update", Name = "UpdateProduct")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public async Task<ActionResult<ProductVm>> UpdateProduct([FromForm] UpdateProductCommand request)
    {
        var listFotoUrls = new List<CreateProductImageCommand>();

        if (request.Fotos is not null)
        {
            foreach (var foto in request.Fotos)
            {
                var resultImage = await _manageImageService.UploadImage(new ImageData
                {
                    ImageStream = foto.OpenReadStream(),
                    Nombre = foto.Name
                });

                var fotoCommand = new CreateProductImageCommand
                {
                    PublicCode = resultImage.PublicId,
                    Url = resultImage.Url
                };

                listFotoUrls.Add(fotoCommand);
            }

            request.ImageUrls = listFotoUrls;
        }

        return await _mediator.Send(request);
    }

    [Authorize(Roles = Role.ADMIN)]
    [HttpDelete("status/{id}", Name = "UpdateStatusProduct")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public async Task<ActionResult<ProductVm>> UpdateStatusProduct(int id)
    {
        var request = new DeleteProductCommand(id);

        return await _mediator.Send(request);
    }
}