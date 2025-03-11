using System.ComponentModel.DataAnnotations;

namespace StoreAPI.Application.DTOs;

public class UpdateProductDto
{
    [StringLength(100, MinimumLength = 2, ErrorMessage = "Name must be between 2 and 100 characters.")]
    public string? Name { get; set; }

    public int? CategoryId { get; set; }

    [Range(0, 10000, ErrorMessage = "Price must be between 0 and 10,000.")]
    public decimal? Price { get; set; }

    [Range(0, 100000, ErrorMessage = "Units in stock must be between 0 and 100,000.")]
    public int? UnitsInStock { get; set; }
}
