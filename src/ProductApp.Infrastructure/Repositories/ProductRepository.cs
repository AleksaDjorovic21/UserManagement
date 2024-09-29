using Microsoft.EntityFrameworkCore;
using ProductApp.Core.Domain;
using ProductApp.Infrastructure.Persistence;

namespace ProductApp.Infrastructure.Repositories;

public class ProductRepository(ProductAppDbContext productAppDbContext) : IProductRepository
{
    private readonly ProductAppDbContext _productAppDbContext = productAppDbContext;

    public async Task AddProductAsync(Product product)
    {
        if (product == null)
        {
            throw new ArgumentNullException(nameof(product), "Product cannot be null");
        }
        await _productAppDbContext.Products.AddAsync(product);
        await _productAppDbContext.SaveChangesAsync();
    }

    public async Task DeleteProductAsync(int id)
    {
        var product = await _productAppDbContext.Products.FindAsync(id) 
            ?? throw new InvalidOperationException($"Product with id {id} not found.");

        _productAppDbContext.Products.Remove(product);
        await _productAppDbContext.SaveChangesAsync();
    }

    public async Task<IEnumerable<Product>> GetAllProductsAsync()
    {
        var products = await _productAppDbContext.Products.ToListAsync();
            return products ?? Enumerable.Empty<Product>();
    }

    public async Task<Product> GetProductByIdAsync(int id)
    {
        var product = await _productAppDbContext.Products
            .AsNoTracking()  
            .FirstOrDefaultAsync(p => p.Id == id) 
                ?? throw new Exception($"Product with id: {id} not found.");
        return product;
    }

    public async Task UpdateProductAsync(Product product)
    {
        if (product == null)
        {
            throw new ArgumentNullException(nameof(product), "Product cannot be null");
        }

        var existingProduct = await _productAppDbContext.Products.FindAsync(product.Id) 
            ?? throw new InvalidOperationException($"Product with id {product.Id} not found.");

        // Detach the existing tracked entity to avoid conflicts
        _productAppDbContext.Entry(existingProduct).State = EntityState.Detached;

        // Attach the new product instance and mark it as modified
        _productAppDbContext.Products.Attach(product);
        _productAppDbContext.Entry(product).State = EntityState.Modified;

        await _productAppDbContext.SaveChangesAsync();
    }
}