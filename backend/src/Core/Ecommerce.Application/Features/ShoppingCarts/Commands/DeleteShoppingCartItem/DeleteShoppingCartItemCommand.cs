using Ecommerce.Application.Features.ShoppingCarts.Vms;
using MediatR;

namespace Ecommerce.Application.Features.ShoppingCarts.Commands.DeleteShoppingCartItem;

public class DeleteShoppingCartItemCommand : IRequest<ShoppingCartVm>
{
    public int Id { get; set; }
}