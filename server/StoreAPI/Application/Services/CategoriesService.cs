using Microsoft.EntityFrameworkCore;
using StoreAPI.Application.Exceptions;
using StoreAPI.Application.Interfaces;
using StoreAPI.Data;
using StoreAPI.Domain.Entities;


namespace StoreAPI.Application.Services;

public class CategoriesService(AppDbContext context) : ICategoriesService
{
    private readonly AppDbContext _context = context;

    public async Task<IEnumerable<Category>> GetCategories() =>
        await _context.Categories.ToListAsync();

    public async Task<Category> GetCategory(int id) =>
        await _context.Categories.FirstOrDefaultAsync(c => c.Id == id)
        ?? throw new NotFoundException($"Category with ID {id} does not exist.");
}
