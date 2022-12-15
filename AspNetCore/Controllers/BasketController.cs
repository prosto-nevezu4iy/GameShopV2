using BasketProject.Contracts.Commands;
using BasketProject.Contracts.Exceptions;
using BasketProject.Contracts.Queries;
using Catalog.Contracts.Queries;
using Core.Extensions;
using Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using Order.Contracts.Commands;
using Order.Contracts.Entities;
using System.Net;
using Web.Interfaces;
using Web.ViewModels;

namespace AspNetCore.Controllers
{
    public class BasketController : MvcController
    {
        private readonly IBasketViewModelService _basketViewModelService;
        private readonly ICurrentUserService _currentUserService;

        public BasketController(IBasketViewModelService basketViewModelService, ICurrentUserService currentUserService)
        {
            _basketViewModelService = basketViewModelService;
            _currentUserService = currentUserService;
        }

        public async Task<IActionResult> Index()
        {
            var query = new GetBasketQuery
            {
                UserId = GetOrSetBasketCookieAndUserId()
            };

            var model = await Mediator.Send(query);
            return View(_basketViewModelService.ConvertToViewModel(model));
        }

        [HttpPost]
        public async Task<IActionResult> Index(ProductViewModel productDetails)
        {
            if (productDetails?.Id == null)
            {
                return RedirectToPage("/Index");
            }

            var query = new GetProductByIdQuery
            {
                Id = productDetails.Id
            };

            var product = await Mediator.Send(query);

            var userId = GetOrSetBasketCookieAndUserId();

            var command = new CreateBasketCommand
            {
                UserId = userId,
                ProductId = productDetails.Id,
                Price = product.Price
            };

            var basket = await Mediator.Send(command);
           
            var basketModel = _basketViewModelService.ConvertToViewModel(basket);

            return View(basketModel);
        }

        [HttpPost]
        public async Task<IActionResult> Update(IEnumerable<BasketItemViewModel> items)
        {
            var query = new GetBasketQuery
            {
                UserId = GetOrSetBasketCookieAndUserId()
            };

            var model = await Mediator.Send(query);
            var basketView = _basketViewModelService.ConvertToViewModel(model);

            if (!ModelState.IsValid)
            {
                return View(nameof(Index), basketView);
            }

            var updateModel = items.ToDictionary(b => b.Id.ToString(), b => b.Quantity);

            var command = new SetQuantitiesCommand
            {
                Id = basketView.Id,
                Items = updateModel
            };

            await Mediator.Send(command);

            return RedirectToAction(nameof(Index));
        }

        [Authorize]
        public async Task<IActionResult> Checkout()
        {
            var query = new GetBasketQuery
            {
                UserId = GetOrSetBasketCookieAndUserId()
            };

            var model = await Mediator.Send(query);
            return View(_basketViewModelService.ConvertToViewModel(model));
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Checkout(IEnumerable<BasketItemViewModel> items)
        {
            try
            {
                var query = new GetBasketQuery
                {
                    UserId = GetOrSetBasketCookieAndUserId()
                };

                var model = await Mediator.Send(query);

                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }

                var updateModel = items.ToDictionary(b => b.Id.ToString(), b => b.Quantity);

                var command = new SetQuantitiesCommand
                {
                    Id = model.Id,
                    Items = updateModel
                };

                await Mediator.Send(command);

                var orderCommand = new CreateOrderCommand
                {
                    BasketId = model.Id,
                    ShippingAddress = new Address("123 Main St.", "Kent", "OH", "United States", "44240")
                };

                await Mediator.Send(orderCommand);

                var deleteCommand = new DeleteBasketCommand
                {
                    BasketId = model.Id
                };

                await Mediator.Send(deleteCommand);
            }
            catch (EmptyBasketOnCheckoutException emptyBasketOnCheckoutException)
            {
                // _logger.LogWarning(emptyBasketOnCheckoutException.Message);
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Success));
        }

        [Authorize]
        public IActionResult Success()
        {
            return View();
        }

        private Guid GetOrSetBasketCookieAndUserId()
        {
            Request.HttpContext.User.Identity.AssertNotNull(nameof(Request.HttpContext.User.Identity));
            Guid userId = Guid.Empty;

            if (Request.HttpContext.User.Identity.IsAuthenticated)
            {
                _currentUserService.Id.AssertNotNull(nameof(_currentUserService.Id));
                return Guid.Parse(_currentUserService.Id);
            }

            if (Request.Cookies.ContainsKey(Constants.BASKET_COOKIENAME))
            {
                if (!Request.HttpContext.User.Identity.IsAuthenticated)
                {
                    if (!Guid.TryParse(Request.Cookies[Constants.BASKET_COOKIENAME].ToString(), out var parsedId))
                    {
                        userId = Guid.Empty;
                    }
                    else
                    {
                        userId = parsedId;
                    }
                }
            }

            if (userId != Guid.Empty) return userId;

            userId = Guid.NewGuid();

            var cookieOptions = new CookieOptions { IsEssential = true };
            cookieOptions.Expires = DateTime.Today.AddYears(10);
            Response.Cookies.Append(Constants.BASKET_COOKIENAME, userId.ToString(), cookieOptions);

            return userId;
        }
    }
}
