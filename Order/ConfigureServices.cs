using Core.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OrderProject.Contracts.Abstracts;
using OrderProject.Repositories;
using System.Reflection;

namespace OrderProject
{
    public static class ConfigureServices
    {
        public static void AddOrder(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            serviceCollection.AddDbContext<OrderDbContext>(c =>
                c.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                x => x.MigrationsHistoryTable("__EFMigrationsHistory", "order")));

            serviceCollection.AddAutoMapper(Assembly.GetExecutingAssembly());
            serviceCollection.AddMediatR(Assembly.GetExecutingAssembly());

            serviceCollection.AddScoped<IOrderRepository, OrderRepository>();
        }
    }
}
