using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCore.Controllers
{
    public class MvcController : Controller
    {
        private ISender _mediator = null!;

        protected ISender Mediator => _mediator ??= HttpContext.RequestServices.GetRequiredService<ISender>();
    }
}
