using AspNetCore.Behavious;
using Catalog;
using Core.Interfaces;
using MediatR;
using Web.Interfaces;
using Web.Services;

namespace Web.Configuration
{
    public static class ConfigureWebServices
    {
        public static IServiceCollection AddWebServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<CatalogSettings>(configuration);
            services.AddScoped<ICatalogViewModelService, CatalogViewModelService>();
            services.AddScoped<IBasketViewModelService, BasketViewModelService>();
            services.AddScoped<IOrderViewModelService, OrderViewModelService>();

            services.AddSingleton<ICurrentUserService, CurrentUserService>();
            services.AddHttpContextAccessor();
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));

            return services;
        }
    }
}
