﻿using DotNetShopper.Products.Core.Abstractions;
using DotNetShopper.Products.Core.DTOs;

namespace DotNetShopper.Products.Core.Interfaces;

/// <summary>
/// Represents product service
/// </summary>
public interface IProductServices
{
    Task<Result<int>> CreateProductAsync(ProductRequest request);
    Task<Result> AddProductCategoryAsync(int productId, int categoryId);
    Task<Result<ProductResponse>> GetProductAsync(int id, bool category);
    Task<Result<ProductsResponse>> GetProductsAsync(int count, int skip, bool? isActive, string? categoryLink);
    Task<Result> UpdateProductAsync(int id, ProductRequest request);
    Task<Result> RemoveProductAsync(int id);
    Task<Result> RemoveProductCategoryAsync(int productId, int categoryId);
}
