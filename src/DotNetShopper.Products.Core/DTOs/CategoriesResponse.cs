namespace DotNetShopper.Products.Core.DTOs;

/// <summary>
/// Represents categories response DTO
/// </summary>
public class CategoriesResponse
{
    public required List<CategoryResponse> Categories { get; set; }
}
