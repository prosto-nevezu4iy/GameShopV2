using Catalog.Contracts.Abstracts;
using Catalog.Repositories;
using Catalog.Services;
using Core.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Catalog
{
    public static class ConfigureServices
    {
        public static void AddCatalog(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            serviceCollection.AddDbContext<CatalogDbContext>(c =>
                c.UseSqlServer(configuration.GetConnectionString("DefaultConnection"), 
                x => x.MigrationsHistoryTable("__EFMigrationsHistory", "catalog")));

            serviceCollection.AddMediatR(Assembly.GetExecutingAssembly());

            serviceCollection.AddSingleton<IUriComposer>(new UriComposer(configuration.Get<CatalogSettings>()));

            serviceCollection.AddScoped<IProductRepository, ProductRepository>();
            serviceCollection.AddScoped<IGenreRepository, GenreRepository>();
        }
    }
}
