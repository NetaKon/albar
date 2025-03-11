namespace StoreAPI.Application.DTOs;

public class ProductDto
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public decimal Price { get; set; }
    public int UnitsInStock { get; set; }
    public required CategoryDto Category { get; set; }
}
