using Ecommerce.Application.Features.Reviews.Queries.Vms;
using Ecommerce.Application.Features.Shared.Queries;
using MediatR;

namespace Ecommerce.Application.Features.Reviews.Queries.PaginationReviews;

public class PaginationReviewsQuery : PaginationBaseQuery, IRequest<PaginationVm<ReviewVm>>
{
    public int?  ProductId { get; set; }
}