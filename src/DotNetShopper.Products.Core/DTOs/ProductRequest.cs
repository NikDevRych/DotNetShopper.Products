using System.ComponentModel.DataAnnotations;

namespace DotNetShopper.Products.Core.DTOs;

/// <summary>
/// Represents create product request DTO
/// </summary>
public sealed class ProductRequest
{
    [Required]
    [StringLength(120)]
    public string? Name { get; set; }

    [Required]
    [Range(0, float.MaxValue)]
    public float? Price { get; set; }

    public string? ImageUrl { get; set; }

    [Required]
    public bool? IsActive { get; set; }
}
