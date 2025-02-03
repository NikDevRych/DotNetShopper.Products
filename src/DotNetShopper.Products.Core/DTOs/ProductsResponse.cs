namespace DotNetShopper.Products.Core.DTOs;
public class ProductsResponse
{
    public required List<ProductResponse> Products { get; set; }
    public required int MaxPages { get; set; }
}
