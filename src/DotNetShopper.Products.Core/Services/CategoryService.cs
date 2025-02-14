using DotNetShopper.Products.Core.Abstractions;
using DotNetShopper.Products.Core.DTOs;
using DotNetShopper.Products.Core.Errors;
using DotNetShopper.Products.Core.Interfaces;
using DotNetShopper.Products.Core.Mappers;
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

    public async Task<Result<int>> CreateCategoryAsync(CategoryRequest request)
    {
        var category = request.RequestToEntity();

        await _dbContext.Categories.AddAsync(category);
        await _dbContext.SaveChangesAsync();

        return Result.Success(category.Id);
    }

    public async Task<Result<CategoryResponse>> GetCategoryAsync(int id)
    {
        var category = await _dbContext.Categories.Where(c => c.Id == id)
            .Select(c => new CategoryResponse
            {
                Id = c.Id,
                Name = c.Name,
                Link = c.Link,
                IsActive = c.IsActive
            })
            .FirstOrDefaultAsync();

        if (category is null) return Result.Failure<CategoryResponse>(CategoryErrors.NotFound);

        return Result.Success(category);
    }

    public async Task<Result<CategoriesResponse>> GetCategoriesAsync(bool? isActive)
    {
        var query = _dbContext.Categories.AsQueryable();

        if (isActive.HasValue)
        {
            query = query.Where(c => c.IsActive == isActive.Value);
        }

        var response = new CategoriesResponse
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

        return Result.Success(response);
    }

    public async Task<Result> UpdateCategoryAsync(int id, CategoryRequest request)
    {
        var category = request.RequestToEntity();
        var categoryToUpdate = await _dbContext.Categories.FirstOrDefaultAsync(c => c.Id == id);
        if (categoryToUpdate is null) return Result.Failure(CategoryErrors.NotFound);

        category.EntityToEntity(category);
        await _dbContext.SaveChangesAsync();

        return Result.Success();
    }

    public async Task<Result> RemoveCategoryAsync(int id)
    {
        var category = await _dbContext.Categories.FirstOrDefaultAsync(c => c.Id == id);
        if (category is null) return Result.Failure(CategoryErrors.NotFound);

        _dbContext.Categories.Remove(category);
        await _dbContext.SaveChangesAsync();

        return Result.Success();
    }
}
