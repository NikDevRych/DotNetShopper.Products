namespace DotNetShopper.Products.Domain.Entities;

/// <summary>
/// Represents category entity
/// </summary>
public class Category
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Link { get; set; }
    public required bool IsActive { get; set; }

    public List<Product> Products { get; } = [];
}
