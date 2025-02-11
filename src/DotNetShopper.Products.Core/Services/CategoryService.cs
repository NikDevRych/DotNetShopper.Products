using DotNetShopper.Products.Core.DTOs;
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
}
