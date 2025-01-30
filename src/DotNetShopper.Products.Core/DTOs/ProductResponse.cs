namespace DotNetShopper.Products.Core.DTOs;

/// <summary>
/// Represents product response DTO
/// </summary>
public sealed class ProductResponse
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required float Price { get; set; }
    public float? SalePrice { get; set; }
    public required bool IsActive { get; set; }
    public required bool IsSale { get; set; }
}
