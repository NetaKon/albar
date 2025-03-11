using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using StoreAPI.API.Extensions;
using StoreAPI.API.Filters;
using StoreAPI.Data;


var builder = WebApplication.CreateBuilder(args);

// Load configuration
string connectionString = builder.Configuration.GetConnectionString("default")!;

// Register services
builder.Services.ConfigureDatabase(connectionString);
builder.Services.ConfigureApplicationServices();
builder.Services.ConfigureAuthentication(builder.Configuration);
builder.Services.ConfigureCors();

builder.Services.AddControllers(options =>
    options.Filters.Add(new GlobalExceptionFilter()));

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.UseCors("AllowAngularClient");
app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

// Seed Users and Roles
using (var scope = app.Services.CreateScope())
{
  var services = scope.ServiceProvider;
  var userManager = services.GetRequiredService<UserManager<IdentityUser>>();
  var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

  await DbSeeder.SeedUsersAndRoles(userManager, roleManager);
}

app.Run();
