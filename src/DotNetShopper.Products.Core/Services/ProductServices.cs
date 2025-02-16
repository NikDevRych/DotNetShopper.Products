using DotNetShopper.Products.Core.Abstractions;
using DotNetShopper.Products.Core.DTOs;
using DotNetShopper.Products.Core.Errors;
using DotNetShopper.Products.Core.Interfaces;
using DotNetShopper.Products.Core.Mappers;
using DotNetShopper.Products.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DotNetShopper.Products.Core.Services;

public class ProductServices : IProductServices
{
    private readonly ProductDbContext _dbContext;

    public ProductServices(ProductDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Result<int>> CreateProductAsync(ProductRequest request)
    {
        var product = request.RequestToEntity();

        await _dbContext.Products.AddAsync(product);
        await _dbContext.SaveChangesAsync();

        return Result.Success(product.Id);
    }

    public async Task<Result> AddProductCategoryAsync(int productId, int categoryId)
    {
        var product = await _dbContext.Products.FirstOrDefaultAsync(p => p.Id == productId);
        if (product is null) return Result.Failure(ProductErrors.NotFound);

        var category = await _dbContext.Categories.FirstOrDefaultAsync(c => c.Id == categoryId);
        if (category is null) return Result.Failure(CategoryErrors.NotFound);

        var categoryInUse = await _dbContext.Products
            .Where(p => p.Id == productId)
            .AnyAsync(p => p.Categories.Any(c => c.Id == categoryId));
        if (categoryInUse) return Result.Failure(ProductErrors.ConflictCategory);

        product.Categories.Add(category);
        await _dbContext.SaveChangesAsync();

        return Result.Success();
    }

    public async Task<Result<ProductResponse>> GetProductAsync(int id, bool category)
    {
        var product = await _dbContext.Products.Where(p => p.Id == id)
            .Select(p => new ProductResponse
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price,
                ImageUrl = p.ImageUrl,
                IsActive = p.IsActive,
                Categories = category ? p.Categories.Select(c => new CategoryResponse
                {
                    Id = c.Id,
                    Name = c.Name,
                    Link = c.Link,
                    IsActive = c.IsActive
                }).ToList() : null
            })
            .FirstOrDefaultAsync();

        if (product is null) return Result.Failure<ProductResponse>(ProductErrors.NotFound);

        return Result.Success(product);
    }

    public async Task<Result<ProductsResponse>> GetProductsAsync(int count, int skip, bool? isActive)
    {
        var query = _dbContext.Products.AsQueryable();

        if (isActive.HasValue)
        {
            query = query.Where(p => p.IsActive == isActive.Value);
        }

        var products = await query
            .Skip(skip).Take(count)
            .Select(p => new ProductResponse
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price,
                ImageUrl = p.ImageUrl,
                IsActive = p.IsActive,
            })
            .ToListAsync();

        var productsCount = await query.CountAsync();
        var maxPages = (productsCount + count - 1) / count;
        var response = new ProductsResponse
        {
            Products = products,
            MaxPages = maxPages
        };

        return Result.Success(response);
    }

    public async Task<Result> UpdateProductAsync(int id, ProductRequest request)
    {
        var product = request.RequestToEntity();
        product.Id = id;

        var productExist = await _dbContext.Products.AnyAsync(p => p.Id == id);
        if (!productExist) return Result.Failure(ProductErrors.NotFound);

        _dbContext.Products.Update(product);
        await _dbContext.SaveChangesAsync();

        return Result.Success();
    }

    public async Task<Result> RemoveProductAsync(int id)
    {
        var product = await _dbContext.Products.FirstOrDefaultAsync(p => p.Id == id);
        if (product is null) return Result.Failure(ProductErrors.NotFound);

        _dbContext.Products.Remove(product);
        await _dbContext.SaveChangesAsync();

        return Result.Success();
    }

    public async Task<Result> RemoveProductCategoryAsync(int productId, int categoryId)
    {
        var product = await _dbContext.Products.Include(p => p.Categories).FirstOrDefaultAsync(p => p.Id == productId);
        if (product is null) return Result.Failure(ProductErrors.NotFound);

        var category = await _dbContext.Categories.FirstOrDefaultAsync(c => c.Id == categoryId);
        if (category is null) return Result.Failure(CategoryErrors.NotFound);

        var categoryInUse = await _dbContext.Products
             .Where(p => p.Id == productId)
             .AnyAsync(p => p.Categories.Any(c => c.Id == categoryId));
        if (!categoryInUse) return Result.Failure(ProductErrors.NotHaveCategory);

        product.Categories.Remove(category);
        await _dbContext.SaveChangesAsync();

        return Result.Success();
    }
}
