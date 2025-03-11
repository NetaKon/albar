using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using StoreAPI.Application.Interfaces;
using StoreAPI.Application.Mappings;
using StoreAPI.Application.Services;
using StoreAPI.Data;

namespace StoreAPI.API.Extensions;

public static class ServiceExtensions
{
    public static void ConfigureDatabase(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));

        services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();
    }

    public static void ConfigureApplicationServices(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(AutoMapperProfile));
        services.AddScoped<ICategoriesService, CategoriesService>();
        services.AddScoped<IProductsService, ProductsServices>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<ITokenService, TokenService>();
        services.AddSwaggerGen();
    }
}
