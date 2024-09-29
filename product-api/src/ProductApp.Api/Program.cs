using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProductApp.Core.Domain;
using ProductApp.Core.Interfaces; 
using ProductApp.Infrastructure.Persistence;
using ProductApp.Infrastructure.Repositories; 
using Microsoft.OpenApi.Models;
using ProductApp.Infrastructure.Services;

public class Program 
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Configure services
        builder.Services.AddDbContext<ProductAppDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

        // Configure Identity
        builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
        {
            // Password settings
            options.Password.RequireDigit = true;
            options.Password.RequiredLength = 6;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireUppercase = true;
            options.Password.RequireLowercase = true;
        })
        .AddEntityFrameworkStores<ProductAppDbContext>()
        .AddDefaultTokenProviders();

        // Register repositories
        builder.Services.AddScoped<IProductRepository, ProductRepository>();
        builder.Services.AddScoped<IApplicationUserRepository, ApplicationUserRepository>();
        builder.Services.AddScoped<IAdminRepository, AdminRepository>();

        // Add CORS policy to allow Angular app to access the API
        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowAngularApp",
                builder =>
                {
                    builder.WithOrigins("http://localhost:4200") 
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                });
        });

        // Add controllers and other services
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo { Title = "Product API", Version = "v1" });
        });

        var app = builder.Build();

        using (var scope = app.Services.CreateScope()) 
        {
            var serviceProvider = scope.ServiceProvider;
            await RoleInitializer.InitializeRoles(serviceProvider); 
        }

        // Configure middleware pipeline
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(c => 
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Product API V1");
                c.RoutePrefix = string.Empty; 
            });
        }

        app.UseHttpsRedirection();

        app.UseCors("AllowAngularApp"); 

        app.UseAuthentication();

        app.UseAuthorization();  

        app.MapControllers(); 

        app.Run();
    }
}
