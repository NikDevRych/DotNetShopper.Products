using DotNetShopper.Products.Core.DTOs;

namespace DotNetShopper.Products.Core.Interfaces;

/// <summary>
/// Represents product service
/// </summary>
public interface IProductServices
{
    Task CreateProduct(CreateProductRequest request);
}
