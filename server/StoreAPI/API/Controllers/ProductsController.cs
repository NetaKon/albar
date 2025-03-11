using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StoreAPI.Application.DTOs;
using StoreAPI.Application.Interfaces;

namespace StoreAPI.API.Controllers;

[Authorize]
[ApiController]
[Route("api/products")]
public class ProductsController(IProductsService productsServices, IMapper mapper) : ControllerBase
{
    private readonly IProductsService _productsServices = productsServices;
    private readonly IMapper _mapper = mapper;

    // GET /api/products
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> GetProducts()
    {
        var products = await _productsServices.GetProducts();

        if (!products.Any())
        {
            return NoContent();
        }

        return Ok(_mapper.Map<List<ProductDto>>(products));
    }

    // GET /api/products/{id} 
    [HttpGet("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetProduct(int id)
    {
        var product = await _productsServices.GetProduct(id);
        return Ok(_mapper.Map<ProductDto>(product));
    }

    // POST /api/products 
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<IActionResult> CreateProduct([FromBody] CreateProductDto model)
    {
        var product = await _productsServices.AddProduct(model);
        return CreatedAtAction(nameof(CreateProduct), product);
    }

    // PUT /api/products/{id} 
    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateProduct(int id, [FromBody] UpdateProductDto model)
    {
        await _productsServices.UpdateProduct(id, model);
        return NoContent();
    }

    // DELETE /api/products - (for testing purposes)
    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> DeleteAllProducts()
    {
        await _productsServices.DeleteAllProducts();
        return NoContent();
    }
}
