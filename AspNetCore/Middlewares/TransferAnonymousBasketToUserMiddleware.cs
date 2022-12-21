using BasketProject.Contracts.Commands;
using Core.Extensions;
using Core.Interfaces;
using MediatR;

namespace AspNetCore.Middlewares
{
    public class TransferAnonymousBasketToUserMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ICurrentUserService _currentUserService;

        public TransferAnonymousBasketToUserMiddleware(
            RequestDelegate next,
            ICurrentUserService currentUserService)
        {
            _next = next;
            _currentUserService = currentUserService;
        }

        public async Task InvokeAsync(HttpContext context, IMediator mediator)
        {
            if (context.User.Identity.IsAuthenticated && context.Request.Cookies.ContainsKey(Constants.BASKET_COOKIENAME))
            {
                var anonymousId = context.Request.Cookies[Constants.BASKET_COOKIENAME];
                if (Guid.TryParse(anonymousId, out var _))
                {
                    _currentUserService.AssertNotNull(nameof(_currentUserService.Id));
                    var command = new TransferBasketCommand
                    {
                        AnonymousId = anonymousId,
                        UserId = _currentUserService.Id
                    };
                    await mediator.Send(command);
                }
                context.Response.Cookies.Delete(Constants.BASKET_COOKIENAME);
            }

            // Call the next delegate/middleware in the pipeline.
            await _next(context);
        }
    }

    public static class TransferAnonymousBasketToUserMiddlewareExtensions
    {
        public static IApplicationBuilder UseTransferAnonymousBasketToUser(
            this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<TransferAnonymousBasketToUserMiddleware>();
        }
    }
}
