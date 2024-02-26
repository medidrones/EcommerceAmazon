using Ecommerce.Application.Features.ShoppingCarts.Vms;
using MediatR;

namespace Ecommerce.Application.Features.ShoppingCarts.Commands.UpdateShoppingCart;

public class UpdateShoppingCartCommand : IRequest<ShoppingCartVm>
{
    public Guid? ShoppingCartId { get; set; }
    public List<ShoppingCartItemVm>? ShoppingCartItems { get; set; }
}