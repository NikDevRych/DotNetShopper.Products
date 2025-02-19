﻿using DotNetShopper.Products.Core.DTOs;
using DotNetShopper.Products.Domain.Entities;

namespace DotNetShopper.Products.Core.Mappers;

/// <summary>
/// Represents products entity mappers
/// </summary>
public static class ProductMapper
{
    public static Product RequestToEntity(this ProductRequest request)
    {
        ArgumentNullException.ThrowIfNullOrEmpty(request.Name);
        ArgumentNullException.ThrowIfNull(request.Price);
        ArgumentNullException.ThrowIfNull(request.IsActive);

        return new Product
        {
            Name = request.Name,
            Price = request.Price.Value,
            ImageUrl = request.ImageUrl,
            IsActive = request.IsActive.Value,
        };
    }

    public static Product EntityToEntity(this Product target, Product product)
    {
        target.Name = product.Name;
        target.Price = product.Price;
        target.ImageUrl = product.ImageUrl;
        target.IsActive = product.IsActive;

        return target;
    }
}
