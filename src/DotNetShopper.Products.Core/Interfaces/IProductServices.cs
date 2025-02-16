using DotNetShopper.Products.Core.Abstractions;
using DotNetShopper.Products.Core.DTOs;
using DotNetShopper.Products.Domain.Entities;

namespace DotNetShopper.Products.Core.Interfaces;

/// <summary>
/// Represents product service
/// </summary>
public interface IProductServices
{
    Task<Result<int>> CreateProductAsync(ProductRequest request);
    Task<Result> AddProductCategoriesAsync(int productId, int categoryId);
    Task<Result<ProductResponse>> GetProductAsync(int id, bool category);
    Task<Result<ProductsResponse>> GetProductsAsync(int count, int skip, bool? isActive);
    Task<Result> UpdateProductAsync(int id, ProductRequest request);
    Task<Result> RemoveProductAsync(int id);
}
