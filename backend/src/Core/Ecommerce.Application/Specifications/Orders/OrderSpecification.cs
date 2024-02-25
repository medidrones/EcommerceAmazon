using Ecommerce.Domain;

namespace Ecommerce.Application.Specifications.Orders;

public class OrderSpecification : BaseSpecification<Order>
{
    public OrderSpecification(OrderSpecificationParams orderParams) : base(
        x => (string.IsNullOrEmpty(orderParams.Username) ||
        x.CompradorUserName!.Contains(orderParams.Username)) && (!orderParams.Id.HasValue ||
        x.Id == orderParams.Id))
    {
        AddInclude(p => p.OrderItems!);
        ApplyPaging(orderParams.PageSize * (orderParams.PageIndex - 1), orderParams.PageSize);

        if (!string.IsNullOrEmpty(orderParams.Sort))
        {
            switch (orderParams.Sort) 
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
