﻿using Catalog.Contracts.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Catalog
{
    public class CatalogDbContextSeed
    {
        public static async Task SeedAsync(CatalogDbContext catalogDbContext,
        ILogger logger,
        int retry = 0)
        {
            var retryForAvailability = retry;
            try
            {
                if (catalogDbContext.Database.IsSqlServer())
                {
                    catalogDbContext.Database.Migrate();
                }

                if (!await catalogDbContext.Genres.AnyAsync())
                {
                    await catalogDbContext.Genres.AddRangeAsync(
                        GetPreconfiguredGenres());

                    await catalogDbContext.SaveChangesAsync();
                }

                if (!await catalogDbContext.Products.AnyAsync())
                {
                    var genres = await catalogDbContext.Genres.ToListAsync();

                    await catalogDbContext.Products.AddRangeAsync(
                        GetPreconfiguredProducts(genres));

                    await catalogDbContext.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                if (retryForAvailability >= 10) throw;

                retryForAvailability++;

                logger.LogError(ex.Message);
                await SeedAsync(catalogDbContext, logger, retryForAvailability);
                throw;
            }
        }

        static IEnumerable<Genre> GetPreconfiguredGenres()
        {
            return new List<Genre>
            {
                new("Action"),
                new("RPG"),
                new("MMORPG"),
                new("Adventure")
            };
        }

        static IEnumerable<Product> GetPreconfiguredProducts(IEnumerable<Genre> genres)
        {
            var action = genres.FirstOrDefault(g => g.Name == "Action");
            var rpg = genres.FirstOrDefault(g => g.Name == "RPG");
            var mmoRpg = genres.FirstOrDefault(g => g.Name == "MMORPG");
            var adventure = genres.FirstOrDefault(g => g.Name == "Adventure");
            return new List<Product>
            {
                new(mmoRpg.Id, "World of Warcraft", "Best game ever", 19.5M,  "http://catalogbaseurltobereplaced/images/products/1.jpg"),
                new(action.Id, "Doom", "Best game ever", 8.50M, "http://catalogbaseurltobereplaced/images/products/2.jpg"),
                new(action.Id, "Elden Ring", "Best game ever", 12,  "http://catalogbaseurltobereplaced/images/products/3.jpg"),
                new(rpg.Id, "Skyrim", "Best game ever", 12, "http://catalogbaseurltobereplaced/images/products/4.jpg"),
                new(rpg.Id, "Fable", "Best game ever", 8.5M, "http://catalogbaseurltobereplaced/images/products/5.jpg"),
                new(rpg.Id, "Fallout 4", "Best game ever", 13, "http://catalogbaseurltobereplaced/images/products/6.jpg"),
                new(action.Id, "Half life", "Best game ever", 1.25M, "http://catalogbaseurltobereplaced/images/products/7.jpg"),
                new(rpg.Id, "Mass Effect", "Best game ever", 30, "http://catalogbaseurltobereplaced/images/products/8.jpg"),
                new(rpg.Id, "The Outer Worlds", "Best game ever", 12.75M, "http://catalogbaseurltobereplaced/images/products/9.jpg"),
                new(adventure.Id, "Portal", "Best game ever", 3, "http://catalogbaseurltobereplaced/images/products/10.jpg"),
                new(rpg.Id, "Witcher 3", "Best game ever", 4.99M, "http://catalogbaseurltobereplaced/images/products/11.jpg"),
            };
        }
    }
}
