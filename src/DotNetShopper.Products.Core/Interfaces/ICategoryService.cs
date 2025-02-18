using DotNetShopper.Products.Core.Abstractions;
using DotNetShopper.Products.Core.DTOs;

namespace DotNetShopper.Products.Core.Interfaces;

/// <summary>
/// Represents category service
/// </summary>
public interface ICategoryService
{
    Task<Result<int>> CreateCategoryAsync(CategoryRequest request);
    Task<Result<CategoryResponse>> GetCategoryAsync(int id);
    Task<Result<CategoriesResponse>> GetCategoriesAsync(bool? isActive);
    Task<Result> UpdateCategoryAsync(int id, CategoryRequest request);
    Task<Result> RemoveCategoryAsync(int id);
}
