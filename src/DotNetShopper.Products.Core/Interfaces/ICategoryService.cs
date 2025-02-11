using DotNetShopper.Products.Core.DTOs;

namespace DotNetShopper.Products.Core.Interfaces;

/// <summary>
/// Represents category service
/// </summary>
public interface ICategoryService
{
    public Task<int> CreateCategory(CategoryRequest request);
    public Task<CategoryResponse?> GetCategory(int id);
}
