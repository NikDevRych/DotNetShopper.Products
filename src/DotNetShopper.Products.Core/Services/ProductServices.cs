﻿using DotNetShopper.Products.Core.DTOs;
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
        ArgumentNullException.ThrowIfNullOrEmpty(request.Name);
        ArgumentNullException.ThrowIfNull(request.Price);
        ArgumentNullException.ThrowIfNull(request.IsActive);
        ArgumentNullException.ThrowIfNull(request.IsSale);

        var productForCreate = new Product
        {
            Name = request.Name,
            Price = request.Price.Value,
            SalePrice = request.SalePrice,
            IsActive = request.IsActive.Value,
            IsSale = request.IsSale.Value
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

    public async Task<ProductResponse?> UpdateProduct(UpdateProductRequest request)
    {
        ArgumentNullException.ThrowIfNullOrEmpty(request.Name);
        ArgumentNullException.ThrowIfNull(request.Price);
        ArgumentNullException.ThrowIfNull(request.IsActive);
        ArgumentNullException.ThrowIfNull(request.IsSale);

        var productToUpdate = await _dbContext.Products.FirstOrDefaultAsync(p => p.Id == request.Id);
        if (productToUpdate == null) return null;

        productToUpdate.Name = request.Name;
        productToUpdate.Price = request.Price.Value;
        productToUpdate.SalePrice = request.SalePrice;
        productToUpdate.IsActive = request.IsActive.Value;
        productToUpdate.IsSale = request.IsSale.Value;

        await _dbContext.SaveChangesAsync();

        var productResponse = new ProductResponse
        {
            Name = productToUpdate.Name,
            Price = productToUpdate.Price,
            SalePrice = productToUpdate.SalePrice,
            IsActive = productToUpdate.IsActive,
            IsSale = productToUpdate.IsSale
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
