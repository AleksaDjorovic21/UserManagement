using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProductApp.Core.Domain;

namespace ProductApp.Infrastructure.Persistence;

public class ProductAppDbContext(DbContextOptions<ProductAppDbContext> options) 
    : IdentityDbContext<ApplicationUser>(options)
{
    public DbSet<Product> Products { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Product entity configuration
        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(p => p.Id); 
            entity.Property(p => p.Id).ValueGeneratedOnAdd(); 
            entity.Property(p => p.Name).IsRequired().HasMaxLength(100); 
            entity.Property(p => p.Price).HasPrecision(18, 2); 
            entity.Property(p => p.Description).HasMaxLength(500); 
        });
    }
}

