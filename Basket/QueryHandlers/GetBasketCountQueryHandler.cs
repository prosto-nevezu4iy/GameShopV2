using BasketProject.Contracts.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BasketProject.QueryHandlers
{
    public class GetBasketCountQueryHandler : IRequestHandler<GetBasketCountQuery, int>
    {
        private readonly BasketDbContext _dbContext;

        public GetBasketCountQueryHandler(BasketDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> Handle(GetBasketCountQuery request, CancellationToken cancellationToken)
        {
            var totalItems = await _dbContext.Baskets
               .Where(basket => basket.BuyerId == request.UserId)
               .SelectMany(item => item.Items)
               .CountAsync();

            return totalItems;
        }
    }
}
