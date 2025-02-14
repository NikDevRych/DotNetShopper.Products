using DotNetShopper.Products.Core.Abstractions;

namespace DotNetShopper.Products.Core.Errors;

public sealed record CategoryErrors
{
    public static readonly Error SomeNotFound = new("Category.SomeNotFound", "Some categories were not found.");
}
