using DotNetShopper.Products.Core.DTOs;
using DotNetShopper.Products.Core.Errors;
using DotNetShopper.Products.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace DotNetShopper.Products.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class ProductController : ControllerBase
{
    private readonly IProductServices _productService;

    public ProductController(IProductServices productService)
    {
        _productService = productService;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateProduct(ProductRequest request)
    {
        var result = await _productService.CreateProductAsync(request);
        return CreatedAtAction(nameof(GetProduct), new { id = result.Value }, null);
    }

    [HttpPost("{id}/category")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AddProductCategories(int id, [FromBody] List<int> categoryIds)
    {
        var result = await _productService.AddProductCategoriesAsync(id, categoryIds);

        if (result.IsFailure && result.Error == ProductErrors.NotFound)
        {
            return NotFound();
        }
        if (result.IsFailure)
        {
            return Problem(statusCode: (int)HttpStatusCode.BadRequest, detail: result.Error.Message);
        }

        return NoContent();
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetProduct(int id, bool category = false)
    {
        var result = await _productService.GetProductAsync(id, category);
        if (result.IsFailure) return NotFound();
        return Ok(result.Value);
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetProducts(bool? isActive, int count = 10, int skip = 0)
    {
        var result = await _productService.GetProductsAsync(count, skip, isActive);
        return Ok(result.Value);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateProduct(int id, [FromBody] ProductRequest request)
    {
        var result = await _productService.UpdateProductAsync(id, request);
        if (result.IsFailure) return NotFound();
        return NoContent();
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> RemoveProduct(int id)
    {
        var result = await _productService.RemoveProductAsync(id);
        if (result.IsFailure) return NotFound();
        return NoContent();
    }
}
