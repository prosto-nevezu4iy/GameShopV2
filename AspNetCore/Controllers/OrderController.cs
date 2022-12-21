using Core.Extensions;
using Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrderProject.Contracts.Queries;
using Web.Interfaces;

namespace AspNetCore.Controllers
{
    [Authorize] // Controllers that mainly require Authorization still use Controller/View; other pages use Pages
    public class OrderController : MvcController
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IOrderViewModelService _orderViewModelService;

        public OrderController(ICurrentUserService currentUserService, IOrderViewModelService orderViewModelService)
        {
            _currentUserService = currentUserService;
            _orderViewModelService = orderViewModelService;
        }

        [HttpGet]
        public async Task<IActionResult> MyOrders()
        {
            _currentUserService.Id.AssertNotNull(nameof(_currentUserService.Id));

            var model = await Mediator.Send(new GetMyOrdersQuery
            {
                UserId = Guid.Parse(_currentUserService.Id)
            });

            return View(_orderViewModelService.ConvertToViewModel(model));
        }

        [HttpGet("Order/Detail/{orderId}")]
        public async Task<IActionResult> Detail(int orderId)
        {
            _currentUserService.Id.AssertNotNull(nameof(_currentUserService.Id));
            var model = await Mediator.Send(new GetOrderDetailsQuery
            {
                OrderId = orderId
            });

            if (model == null)
            {
                return BadRequest("No such order found for this user.");
            }

            return View(_orderViewModelService.ConvertToViewModel(model));
        }
    }
}
