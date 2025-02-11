using DotNetShopper.Products.Core.DTOs;
using DotNetShopper.Products.Domain.Entities;

namespace DotNetShopper.Products.Core.Interfaces;

/// <summary>
/// Represents category service
/// </summary>
public interface ICategoryService
{
    Task<int> CreateCategory(CategoryRequest request);
    Task<CategoryResponse?> GetCategory(int id);
    Task<CategoriesResponse> GetCategories(bool? isActive);
    Task<CategoryResponse?> UpdateCategory(int id, CategoryRequest request);
    Task<Category?> GetCategoryEntity(int id);
    Task RemoveCategory(Category category);
}
