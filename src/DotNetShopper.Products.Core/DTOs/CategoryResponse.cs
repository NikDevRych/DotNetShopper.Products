namespace DotNetShopper.Products.Core.DTOs;

/// <summary>
/// Represents category response DTO
/// </summary>
public class CategoryResponse
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Link { get; set; }
    public required bool IsActive { get; set; }
}
