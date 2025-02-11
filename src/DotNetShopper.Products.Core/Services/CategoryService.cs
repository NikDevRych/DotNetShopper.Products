using DotNetShopper.Products.Core.DTOs;
using DotNetShopper.Products.Core.Interfaces;
using DotNetShopper.Products.Core.Mappers;
using DotNetShopper.Products.Domain.Entities;
using DotNetShopper.Products.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DotNetShopper.Products.Core.Services;

public class CategoryService : ICategoryService
{
    private readonly ProductDbContext _dbContext;

    public CategoryService(ProductDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<int> CreateCategory(CategoryRequest request)
    {
        var categoryForCreate = request.RequestToEntity();

        await _dbContext.Categories.AddAsync(categoryForCreate);
        await _dbContext.SaveChangesAsync();

        return categoryForCreate.Id;
    }

    public async Task<CategoryResponse?> GetCategory(int id)
    {
        return await _dbContext.Categories.Where(c => c.Id == id)
            .Select(c => new CategoryResponse
            {
                Id = c.Id,
                Name = c.Name,
                Link = c.Link,
                IsActive = c.IsActive
            })
            .FirstOrDefaultAsync();
    }

    public async Task<CategoriesResponse> GetCategories(bool? isActive)
    {
        var query = _dbContext.Categories.AsQueryable();

        if (isActive.HasValue)
        {
            query = query.Where(c => c.IsActive == isActive.Value);
        }

        return new CategoriesResponse
        {
            Categories = await query.Select(c => new CategoryResponse
            {
                Id = c.Id,
                Name = c.Name,
                Link = c.Link,
                IsActive = c.IsActive
            })
            .ToListAsync()
        };
    }

    public async Task<CategoryResponse?> UpdateCategory(int id, CategoryRequest request)
    {
        var category = request.RequestToEntity();
        var categoryToUpdate = await _dbContext.Categories.FirstOrDefaultAsync(c => c.Id == id);
        if (categoryToUpdate == null) return null;

        category.EntityToEntity(category);
        await _dbContext.SaveChangesAsync();

        return new CategoryResponse
        {
            Id = categoryToUpdate.Id,
            Name = categoryToUpdate.Name,
            Link = categoryToUpdate.Link,
            IsActive = categoryToUpdate.IsActive
        };
    }

    public async Task<Category?> GetCategoryEntity(int id)
    {
        return await _dbContext.Categories.FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task RemoveCategory(Category category)
    {
        _dbContext.Categories.Remove(category);
        await _dbContext.SaveChangesAsync();
    }
}
