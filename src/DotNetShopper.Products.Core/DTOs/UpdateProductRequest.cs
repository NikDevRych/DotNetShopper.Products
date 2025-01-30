using System.ComponentModel.DataAnnotations;

namespace DotNetShopper.Products.Core.DTOs;

/// <summary>
/// Represents update product request
/// </summary>
public sealed class UpdateProductRequest
{
    [Required]
    [Range(0, int.MaxValue)]
    public int Id { get; set; }

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
