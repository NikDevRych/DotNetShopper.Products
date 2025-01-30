using DotNetShopper.Products.Core.DTOs;
using DotNetShopper.Products.Core.Interfaces;
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

    public async Task<int> CreateProduct(CreateProductRequest request)
    {
        var productForCreate = new Product
        {
            Name = request.Name,
            Price = request.Price,
            SalePrice = request.SalePrice,
            IsActive = request.IsActive,
            IsSale = request.IsSale
        };

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
                SalePrice = p.SalePrice,
                IsActive = p.IsActive,
                IsSale = p.IsSale
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
                SalePrice = p.SalePrice,
                IsActive = p.IsActive,
                IsSale = p.IsSale
            })
            .ToListAsync();
    }
}
