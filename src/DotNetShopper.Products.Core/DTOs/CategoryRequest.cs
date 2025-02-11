using System.ComponentModel.DataAnnotations;

namespace DotNetShopper.Products.Core.DTOs;

/// <summary>
/// Represents category request DTO
/// </summary>
public class CategoryRequest
{
    [Required]
    [StringLength(120)]
    public string? Name { get; set; }

    [Required]
    [StringLength(120)]
    public string? Link { get; set; }

    [Required]
    public bool? IsActive { get; set; }
}
