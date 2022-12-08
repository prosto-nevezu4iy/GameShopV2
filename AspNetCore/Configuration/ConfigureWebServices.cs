using Catalog;
using Catalog.Mapping;
using Core.Interfaces;
using Web.Interfaces;
using Web.Services;

namespace Web.Configuration
{
    public static class ConfigureWebServices
    {
        public static IServiceCollection AddWebServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(typeof(CatalogMappingProfile));

            services.Configure<CatalogSettings>(configuration);
           // services.AddScoped<CatalogViewModelService>();
            services.AddScoped<ICatalogViewModelService, CatalogViewModelService>();
           // services.AddScoped<IBasketViewModelService, BasketViewModelService>();
           // services.AddScoped<IOrderViewModelService, OrderViewModelService>();

            services.AddSingleton<ICurrentUserService, CurrentUserService>();
            services.AddHttpContextAccessor();

            return services;
        }
    }
}
