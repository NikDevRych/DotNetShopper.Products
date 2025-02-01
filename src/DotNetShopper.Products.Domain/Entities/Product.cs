namespace DotNetShopper.Products.Domain.Entities;

/// <summary>
/// Represents product entity
/// </summary>
public class Product
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required float Price { get; set; }
    public required bool IsActive { get; set; }
}
