using StoreAPI.Domain.Entities;

namespace StoreAPI.Application.Interfaces;
public interface ICategoriesService
{
    Task<IEnumerable<Category>> GetCategories();

    Task<Category> GetCategory(int id);
}