using DotNetShopper.Products.Core.DTOs;
using DotNetShopper.Products.Core.Interfaces;
using DotNetShopper.Products.Core.Mappers;
using DotNetShopper.Products.Domain.Entities;
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

    public async Task<int> CreateProduct(ProductRequest request)
    {
        var productForCreate = request.RequestToEntity();

        await _dbContext.Products.AddAsync(productForCreate);
        await _dbContext.SaveChangesAsync();

        return productForCreate.Id;
    }

    public async Task<ProductResponse?> GetProduct(int id)
    {
        return await _dbContext.Products.Where(p => p.Id == id)
            .Select(p => new ProductResponse
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price,
                IsActive = p.IsActive,
            })
            .FirstOrDefaultAsync();
    }

    public async Task<List<ProductResponse>> GetProducts(int count, int skip)
    {
        return await _dbContext.Products
            .Skip(skip).Take(count)
            .Select(p => new ProductResponse
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price,
                IsActive = p.IsActive,
            })
            .ToListAsync();
    }

    public async Task<ProductResponse?> UpdateProduct(int id, ProductRequest request)
    {
        var product = request.RequestToEntity();
        var productToUpdate = await _dbContext.Products.FirstOrDefaultAsync(p => p.Id == id);
        if (productToUpdate == null) return null;

        productToUpdate.EntityToEntity(product);
        await _dbContext.SaveChangesAsync();

        var productResponse = new ProductResponse
        {
            Id = productToUpdate.Id,
            Name = productToUpdate.Name,
            Price = productToUpdate.Price,
            IsActive = productToUpdate.IsActive,
        };

        return productResponse;
    }

    public async Task<Product?> GetProductEntity(int id)
    {
        return await _dbContext.Products.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task RemoveProduct(Product product)
    {
        _dbContext.Products.Remove(product);
        await _dbContext.SaveChangesAsync();
    }
}
