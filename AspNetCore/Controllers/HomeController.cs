using Catalog.Contracts.Queries;
using Catalog.QueryHandlers;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Web.Interfaces;
using Web.ViewModels;

namespace AspNetCore.Controllers
{
    public class HomeController : MvcController
    {
        private readonly ICatalogViewModelService _catalogViewModelService;

        public HomeController(ICatalogViewModelService catalogViewModelService)
        {
            _catalogViewModelService = catalogViewModelService;
        }

        public async Task<IActionResult> Index(CatalogIndexViewModel catalogModel, int? pageId)
        {
            var query = new GetCatalogItemsQuery
            {
                PageIndex = pageId ?? 0,
                ItemsPage = 10,
                GenreId = catalogModel.GenresFilterApplied
            };

            var model = await Mediator.Send(query);

            return View(_catalogViewModelService.ConvertToViewModel(query, model));
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
