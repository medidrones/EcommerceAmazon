using Ecommerce.Domain;

namespace Ecommerce.Application.Specifications.Reviews;

public class ReviewSpecification : BaseSpecification<Review>
{
    public ReviewSpecification(ReviewSpecificationParams reviewParams)
        : base(x => (!reviewParams.ProductId.HasValue || x.ProductId == reviewParams.ProductId))
    {
        ApplyPaging(reviewParams.PageSize * (reviewParams.PageIndex - 1), reviewParams.PageSize);

        if (!string.IsNullOrEmpty(reviewParams.Sort))
        {
            switch(reviewParams.Sort)
            {
                case "createDateAsc":
                    AddOrderBy(p => p.CreateDate!);
                    break;

                case "createDateDesc":
                    AddOrderByDescending(p => p.CreateDate!);
                    break;
                
                default:
                    AddOrderBy(p => p.CreateDate!); 
                    break;
            }
        }
        else
        {
            AddOrderByDescending(p => p.CreateDate!);
        }
    }
}
