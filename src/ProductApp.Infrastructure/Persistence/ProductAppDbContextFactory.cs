using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace ProductApp.Infrastructure.Persistence;

// IDesignTimeDbContextFactory<>: An interface that allows you to create a DbContext at design time.
public class ProductAppDbContextFactory : IDesignTimeDbContextFactory<ProductAppDbContext>
{
    public ProductAppDbContext CreateDbContext(string[] args)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "..\\ProductApp.Api"))
            .AddJsonFile("appsettings.json")
            .Build();

        var optionsBuilder = new DbContextOptionsBuilder<ProductAppDbContext>();
        optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
            b => b.MigrationsAssembly("ProductApp.Infrastructure"));

        // Creates a new instance of ProductAppDbContext with the configured options.
        return new ProductAppDbContext(optionsBuilder.Options);
    }
}

