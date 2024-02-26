using Ecommerce.Application.Features.Categories.Vms;
using MediatR;

namespace Ecommerce.Application.Features.Categories.Queries.GetCategoryList;

public class GetCategoryListQuery : IRequest<IReadOnlyList<CategoryVm>>
{    
}
