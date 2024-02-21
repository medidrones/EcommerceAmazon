using Ecommerce.Application.Features.Auths.Users.Vms;
using MediatR;

namespace Ecommerce.Application.Features.Auths.Users.Queries.GetUserByUserName;

public class GetUserByUserNameQuery : IRequest<AuthResponse>
{
    public string? Username { get; set; }

    public GetUserByUserNameQuery(string? username)
    {
        Username = username ?? throw new ArgumentNullException(nameof(Username));
    }
}
