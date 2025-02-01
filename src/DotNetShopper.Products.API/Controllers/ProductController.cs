using DotNetShopper.Products.Core.DTOs;
using DotNetShopper.Products.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

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
        var productId = await _productService.CreateProduct(request);
        return CreatedAtAction(nameof(GetProduct), new { id = productId }, null);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetProduct(int id)
    {
        var product = await _productService.GetProduct(id);
        if (product == null) return NotFound();
        return Ok(product);
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetProduct(int count = 10, int skip = 0)
    {
        var products = await _productService.GetProducts(count, skip);
        return Ok(products);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateProduct(int id, [FromBody] ProductRequest request)
    {
        var product = await _productService.UpdateProduct(id, request);
        if (product == null) return NotFound();
        return Ok(product);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> RemoveProduct(int id)
    {
        var productToRemove = await _productService.GetProductEntity(id);
        if (productToRemove == null) return NotFound();
        await _productService.RemoveProduct(productToRemove);
        return Ok();
    }
}
