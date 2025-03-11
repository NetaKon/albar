using StoreAPI.Application.DTOs;
using StoreAPI.Domain.Entities;


namespace StoreAPI.Application.Interfaces;

public interface IProductsService
{
    Task<IEnumerable<Product>> GetProducts();
    Task<Product> GetProduct(int id);
    Task<Product> AddProduct(CreateProductDto model);
    Task UpdateProduct(int id, UpdateProductDto model);
    Task DeleteAllProducts();
}
