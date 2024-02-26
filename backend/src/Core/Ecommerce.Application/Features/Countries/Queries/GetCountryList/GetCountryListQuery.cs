using Ecommerce.Application.Features.Countries.Vms;
using MediatR;

namespace Ecommerce.Application.Features.Countries.Queries.GetCountryList;

public class GetCountryListQuery :  IRequest<IReadOnlyList<CountryVm>>
{
}