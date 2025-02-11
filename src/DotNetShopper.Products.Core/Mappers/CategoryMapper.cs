using DotNetShopper.Products.Core.DTOs;
using DotNetShopper.Products.Domain.Entities;

namespace DotNetShopper.Products.Core.Mappers;

/// <summary>
/// Represents category entity mappers
/// </summary>
public static class CategoryMapper
{
    public static Category RequestToEntity(this CategoryRequest request)
    {
        ArgumentNullException.ThrowIfNullOrEmpty(request.Name);
        ArgumentNullException.ThrowIfNullOrEmpty(request.Link);
        ArgumentNullException.ThrowIfNull(request.IsActive);

        return new Category
        {
            Name = request.Name,
            Link = request.Link,
            IsActive = request.IsActive.Value,
        };
    }

    public static Category EntityToEntity(this Category target, Category category)
    {
        target.Name = category.Name;
        target.Link = category.Link;
        target.IsActive = category.IsActive;

        return target;
    }
}
