using DotNetShopper.Products.Core.DTOs;
using DotNetShopper.Products.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DotNetShopper.Products.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class CategoryController : ControllerBase
{
    private readonly ICategoryService _categoryService;

    public CategoryController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateCategory(CategoryRequest request)
    {
        var result = await _categoryService.CreateCategoryAsync(request);
        return CreatedAtAction(nameof(GetCategory), new { id = result.Value }, null);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetCategory(int id)
    {
        var result = await _categoryService.GetCategoryAsync(id);
        if (result.IsFailure) return NotFound();
        return Ok(result.Value);
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetCategories(bool? isActive)
    {
        var result = await _categoryService.GetCategoriesAsync(isActive);
        return Ok(result.Value);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateCategory(int id, [FromBody] CategoryRequest request)
    {
        var result = await _categoryService.UpdateCategoryAsync(id, request);
        if (result.IsFailure) return NotFound();
        return NoContent();
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> RemoveCategory(int id)
    {
        var result = await _categoryService.RemoveCategoryAsync(id);
        if (result.IsFailure) return NotFound();
        return NoContent();
    }
}
