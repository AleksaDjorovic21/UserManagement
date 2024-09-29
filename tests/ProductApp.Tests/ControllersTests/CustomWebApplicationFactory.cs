using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ProductApp.Core.Domain;
using ProductApp.Infrastructure.Persistence;

namespace ProductApp.Tests.Controllers;

public class CustomWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            // Remove the existing database registration
            var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<ProductAppDbContext>));
            if (descriptor != null)
            {
                services.Remove(descriptor);
            }

            // Add an in-memory database for testing
            services.AddDbContext<ProductAppDbContext>(options =>
            {
                options.UseInMemoryDatabase("InMemoryDbForTesting");
            });

            // Optionally, seed the database with test data here
            using var scope = services.BuildServiceProvider().CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ProductAppDbContext>();
            CustomWebApplicationFactory<TStartup>.SeedDatabase(context);
        });
    }

    private static void SeedDatabase(ProductAppDbContext context)
    {
        context.Products.AddRange(
        [
            new Product { Id = 1, Name = "Product1", Description = "Description1", Price = 10 },
            new Product { Id = 2, Name = "Product2", Description = "Description2", Price = 20 },
            new Product { Id = 3, Name = "Product3", Description = "Description3", Price = 30 },
        ]);
        context.SaveChanges();
    }
}
