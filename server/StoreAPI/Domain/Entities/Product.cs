using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace StoreAPI.Domain.Entities;
public class Product
{
    public int Id { get; set; }
    public required string Name { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal Price { get; set; }
    public int UnitsInStock { get; set; }

    // Foreign key property
    public int CategoryId { get; set; }

    // Navigation property
    [ValidateNever]
    public Category? Category { get; set; }
}
