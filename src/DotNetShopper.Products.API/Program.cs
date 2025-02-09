using DotNetShopper.Products.Core.Constants;
using DotNetShopper.Products.Core.Interfaces;
using DotNetShopper.Products.Core.Services;
using DotNetShopper.Products.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString(SetupConstants.DefaultConnection);

builder.Services.AddCors(x => x.AddDefaultPolicy(c =>
{
    c.AllowAnyMethod();
    c.AllowAnyOrigin();
    c.AllowAnyHeader();
}));
builder.Services.Configure<RouteOptions>(options
    => options.LowercaseUrls = true);
builder.Services.AddControllers();
builder.Services.AddDbContext<ProductDbContext>(options =>
    options.UseNpgsql(connectionString));
builder.Services.AddScoped<IProductServices, ProductServices>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseCors();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

app.Run();
