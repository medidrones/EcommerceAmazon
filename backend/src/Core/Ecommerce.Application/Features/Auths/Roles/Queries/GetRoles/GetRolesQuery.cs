using MediatR;

namespace Ecommerce.Application.Features.Auths.Queries.Roles.GetRoles;

public class GetRolesQuery : IRequest<List<string>>
{
}