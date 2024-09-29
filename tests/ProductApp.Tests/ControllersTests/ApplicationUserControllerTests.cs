using System.Net;
using System.Text;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using ProductApp.Core.Domain;
using ProductApp.Api.Controllers;

namespace ProductApp.Tests.Controllers;

public class ApplicationUserControllerTests(WebApplicationFactory<ApplicationUserController> factory) 
    : IClassFixture<WebApplicationFactory<ApplicationUserController>>
{
    private readonly HttpClient _client = factory.CreateClient();

    [Fact]
    public async Task Register_ShouldReturnOk_WhenUserIsRegisteredSuccessfully()
    {
        // Arrange
        var timestamp = DateTime.UtcNow.ToString("yyyyMMddHHmmss");
        var registerModel = new Register
        {
            FirstName = $"first_{timestamp}",
            LastName = $"last_{timestamp}",
            Email = $"user_{timestamp}@example.com",
            Password = "User123"
        };

        var json = JsonConvert.SerializeObject(registerModel);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        // Act
        var response = await _client.PostAsync("/api/ApplicationUser/register", content);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var responseBody = await response.Content.ReadAsStringAsync();
        responseBody.Should().NotBeNullOrEmpty();
        responseBody.Should().Contain("User registered successfully.");
    }

    [Fact]
    public async Task LoginAsAdmin_ShouldReturnOk_WhenLoginIsSuccessful()
    {
        // Arrange
        var loginModel = new Login
        {
            Email = "admin@example.com",
            Password = "Admin123"
        };

        var json = JsonConvert.SerializeObject(loginModel);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        // Act
        var response = await _client.PostAsync("/api/ApplicationUser/login", content);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var responseBody = await response.Content.ReadAsStringAsync();
        responseBody.Should().Contain("Login successful.");
    }
}


