using DotNetShopper.Products.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DotNetShopper.Products.Infrastructure.Data;

/// <summary>
/// Represents Product database context
/// </summary>
public class ProductDbContext(DbContextOptions<ProductDbContext> options) : DbContext(options)
{
    public DbSet<Product> Products { get; set; }
}
