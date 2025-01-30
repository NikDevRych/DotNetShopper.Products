using DotNetShopper.Products.Core.DTOs;
using DotNetShopper.Products.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DotNetShopper.Products.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    private readonly IProductServices _productService;

    public ProductController(IProductServices productService)
    {
        _productService = productService;
    }

    public async Task<IActionResult> CreateProduct(CreateProductRequest request)
    {
        await _productService.CreateProduct(request);
        return Created();
    }
}
