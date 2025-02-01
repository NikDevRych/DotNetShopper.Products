using DotNetShopper.Products.Core.DTOs;
using DotNetShopper.Products.Domain.Entities;

namespace DotNetShopper.Products.Core.Interfaces;

/// <summary>
/// Represents product service
/// </summary>
public interface IProductServices
{
    Task<int> CreateProduct(ProductRequest request);
    Task<ProductResponse?> GetProduct(int id);
    Task<List<ProductResponse>> GetProducts(int count, int skip);
    Task<ProductResponse?> UpdateProduct(int id, ProductRequest request);
    Task<Product?> GetProductEntity(int id);
    Task RemoveProduct(Product product);
}
