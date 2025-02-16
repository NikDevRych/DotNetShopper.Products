﻿using DotNetShopper.Products.Core.Abstractions;

namespace DotNetShopper.Products.Core.Errors;

public sealed record ProductErrors
{
    public static readonly Error NotFound = new("Product.NotFound", "Product is not found.");
    public static readonly Error ConflictCategory = new("Product.ConflictCategory", "The product already has this category.");
    public static readonly Error NotHaveCategory = new("Product.NotHaveCategory", "The product does not belong to this category.");
}
