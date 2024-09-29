using FluentAssertions;
using Newtonsoft.Json;
using ProductApp.Api.Controllers;
using ProductApp.Core.Domain;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ProductApp.Tests.Controllers;

public class ProductControllerTests(CustomWebApplicationFactory<ProductController> factory) 
    : IClassFixture<CustomWebApplicationFactory<ProductController>>
{
    private readonly HttpClient _client = factory.CreateClient();

    [Fact]
    public async Task GetProducts_ShouldReturnAllProducts()
    {
        // Act
        var response = await _client.GetAsync("/api/product");
        response.EnsureSuccessStatusCode();

        // Deserialize response
        var products = await response.Content.ReadFromJsonAsync<List<Product>>(new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            Converters = { new JsonStringEnumConverter(JsonNamingPolicy.CamelCase) }
        });

        // Assert
        products.Should().NotBeNull();
        products.Should().HaveCountGreaterThan(0);
    }

    [Fact]
    public async Task GetProduct_ShouldReturnProductById()
    {
        // Act
        var response = await _client.GetAsync("/api/product/3");
        response.EnsureSuccessStatusCode();

        // Deserialize response
        var product = await response.Content.ReadFromJsonAsync<Product>(new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            Converters = { new JsonStringEnumConverter(JsonNamingPolicy.CamelCase) }
        });

        // Assert
        product.Should().NotBeNull();
        product!.Id.Should().Be(3);
    }

    [Fact]
    public async Task CreateProduct_ShouldReturnCreatedProduct()
    {
        // Arrange
        var product = new Product
        {
            Name = $"TestProduct_{Guid.NewGuid()}",
            Description = "Test description",
            Price = 100
        };

        var json = JsonConvert.SerializeObject(product);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        // Act
        var response = await _client.PostAsync("/api/product", content);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Created);
        var createdProduct = await response.Content.ReadFromJsonAsync<Product>(new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            Converters = { new JsonStringEnumConverter(JsonNamingPolicy.CamelCase) }
        });
        
        createdProduct.Should().NotBeNull();
        createdProduct!.Name.Should().Be(product.Name);
        createdProduct.Description.Should().Be(product.Description);
    }

    [Fact]
    public async Task UpdateProduct_ShouldReturnNoContent()
    {
        // Arrange
        var product = new Product
        {
            Id = 1, 
            Name = $"UpdatedProduct_{Guid.NewGuid()}",
            Description = "Updated description",
            Price = 150
        };

        var json = JsonConvert.SerializeObject(product);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        // Act
        var response = await _client.PutAsync("/api/product/1", content);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }

    [Fact]
    public async Task DeleteProduct_ShouldReturnNoContent()
    {
        // Act
        var response = await _client.DeleteAsync("/api/product/3");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }
}

