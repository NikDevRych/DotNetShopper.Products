using System.ComponentModel.DataAnnotations;

namespace DotNetShopper.Products.Core.DTOs;

/// <summary>
/// Represents create product request DTO
/// </summary>
public sealed class CreateProductRequest
{
    [Required]
    [StringLength(120)]
    public string Name { get; set; } = null!;

    [Required]
    [Range(0, float.MaxValue)]
    public float Price { get; set; }

    [Range(0, float.MaxValue)]
    public float? SalePrice { get; set; }

    [Required]
    public bool IsActive { get; set; }

    [Required]
    public bool IsSale { get; set; }
}
