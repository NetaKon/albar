using Microsoft.EntityFrameworkCore;
using AutoMapper;
using StoreAPI.Data;
using StoreAPI.Application.Interfaces;
using StoreAPI.Application.Exceptions;
using StoreAPI.Application.DTOs;
using StoreAPI.Domain.Entities;


namespace StoreAPI.Application.Services;

public class ProductsServices(AppDbContext context, IMapper mapper, ICategoriesService categoriesService) : IProductsService
{
    private readonly AppDbContext _context = context;
    private readonly IMapper _mapper = mapper;
    private readonly ICategoriesService _categoriesService = categoriesService;

    // Retrieve all products
    public async Task<IEnumerable<Product>> GetProducts() =>
        await _context.Products.Include(p => p.Category).ToListAsync();

    // Retrieve specific product
    public async Task<Product> GetProduct(int id) =>
        await _context.Products.Include(p => p.Category)
               .FirstOrDefaultAsync(p => p.Id == id)
        ?? throw new NotFoundException($"Product with ID {id} not found.");


    // Create a new product
    public async Task<Product> AddProduct(CreateProductDto model)
    {
        await ValidateCategory(model.CategoryId);

        var product = _mapper.Map<Product>(model);
        _context.Products.Add(product);
        await _context.SaveChangesAsync();

        return product;
    }

    // Update a product
    public async Task UpdateProduct(int id, UpdateProductDto model)
    {
        var product = await _context.Products.FindAsync(id)
            ?? throw new NotFoundException($"Product with ID {id} not found.");

        product.Name = model.Name ?? product.Name;
        product.CategoryId = model.CategoryId ?? product.CategoryId;
        product.Price = model.Price ?? product.Price;
        product.UnitsInStock = model.UnitsInStock ?? product.UnitsInStock;

        if (model.CategoryId.HasValue)
        {
            await ValidateCategory(model.CategoryId.Value);
            product.CategoryId = model.CategoryId.Value;
        }

        await _context.SaveChangesAsync();
    }

    // Delete all products (for testing purposes)
    public async Task DeleteAllProducts()
    {
        _context.Products.RemoveRange(await _context.Products.ToListAsync());
        await _context.SaveChangesAsync();
    }

    private async Task ValidateCategory(int categoryId) =>
        await _categoriesService.GetCategory(categoryId);
}
