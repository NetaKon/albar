using System.Text.Json;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StoreAPI.Domain.Entities;

namespace StoreAPI.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : IdentityDbContext<IdentityUser>(options)
{
    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }

    // Method to seed data into the database
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Category>().HasData(LoadSeedData<Category>("DummyData/categories.json"));
        builder.Entity<Product>().HasData(LoadSeedData<Product>("DummyData/products.json"));
    }

    private static List<T> LoadSeedData<T>(string filePath)
    {
        if (!File.Exists(filePath)) return [];

        var jsonData = File.ReadAllText(filePath);
        return JsonSerializer.Deserialize<List<T>>(jsonData) ?? [];
    }
}