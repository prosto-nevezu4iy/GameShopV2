using Core.Specifications;
using OrderProject.Contracts.Entities;

namespace OrderProject.Specifications
{
    public class OrdersWithItemsSpecification : BaseSpecification<Order>
    {
        public OrdersWithItemsSpecification(Guid buyerId)
         : base(o => o.BuyerId == buyerId)
        {
            AddInclude(o => o.OrderItems);
        }
    }
}
