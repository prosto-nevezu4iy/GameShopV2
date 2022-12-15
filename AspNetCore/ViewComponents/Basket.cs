using AspNetCore;
using BasketProject.Contracts.Queries;
using Core.Extensions;
using Core.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Web.Interfaces;
using Web.ViewModels;

namespace Web.ViewComponents
{
    public class Basket : ViewComponent
    {
        private readonly IBasketViewModelService _basketService;
        private readonly ICurrentUserService _currentUserService;
        private readonly IMediator _mediator;

        public Basket(IBasketViewModelService basketService, ICurrentUserService currentUserService, IMediator mediator)
        {
            _basketService = basketService;
            _currentUserService = currentUserService;
            _mediator = mediator;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var vm = new BasketComponentViewModel
            {
                ItemsCount = await CountTotalBasketItems()
            };
            return View(vm);
        }

        private async Task<int> CountTotalBasketItems()
        {
            var query = new GetBasketCountQuery();

            if (User.Identity.IsAuthenticated)
            {
                _currentUserService.Id.AssertNotNull(nameof(_currentUserService.Id));
                query.UserId = Guid.Parse(_currentUserService.Id);
                return await _mediator.Send(query);
            }

            Guid anonymousId = GetAnnonymousIdFromCookie();
            if (anonymousId == Guid.Empty)
                return 0;

            query.UserId = anonymousId;

            return await _mediator.Send(query);
        }

        private Guid GetAnnonymousIdFromCookie()
        {
            if (Request.Cookies.ContainsKey(Constants.BASKET_COOKIENAME))
            {
                var id = Request.Cookies[Constants.BASKET_COOKIENAME];

                if (Guid.TryParse(id, out var parsedId))
                {
                    return parsedId;
                }
            }
            return Guid.Empty;
        }
    }
}
