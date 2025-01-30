using DotNetShopper.Products.Core.DTOs;
using DotNetShopper.Products.Domain.Entities;

namespace DotNetShopper.Products.Core.Interfaces;

/// <summary>
/// Represents product service
/// </summary>
public interface IProductServices
{
    Task CreateProduct(CreateProductRequest request);
    Task<Product?> GetProduct(int id);
    Task<List<Product>> GetProducts(int count, int skip);
}
