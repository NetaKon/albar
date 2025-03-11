using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using StoreAPI.Application.DTOs;
using StoreAPI.Application.Interfaces;

namespace StoreAPI.API.Controllers;

[ApiController]
[Route("api/categories")]
public class CategoriesController(ICategoriesService categoriesService, IMapper mapper) : ControllerBase
{
    // GET /api/categories
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> GetCategories()
    {
        var categories = await categoriesService.GetCategories();

        if (!categories.Any())
        {
            return NoContent();
        }

        return Ok(mapper.Map<List<CategoryDto>>(categories));
    }
}
