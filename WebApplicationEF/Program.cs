using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplicationEF;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var dbPath = "myapp.db";
builder.Services.AddDbContext<AppDbContext>(
    options => options.UseSqlite($"Data Source={dbPath}"));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapGet("/products", async (AppDbContext context) =>
{
    var products = await context.Products.ToListAsync();
    return products;
});

app.MapPost("/addProduct",
    async (AppDbContext context, [FromBody] Product product) =>
    {
        await context.AddAsync(product);
        await context.SaveChangesAsync();
    });

app.MapGet("/getProduct",
    async (AppDbContext context, [FromQuery] long id) =>
    {
        var product = await context.Products.FirstAsync(p => p.Id == id);
        return product;
    });

app.MapPost("/updateProduct", async (AppDbContext context, [FromQuery] long id, [FromBody] Product updatedProduct) =>
{
    var product = await context.Products.FirstAsync(p => p.Id == id);
    product.Name = updatedProduct.Name;
    product.Price = updatedProduct.Price;
    product.Amount = updatedProduct.Amount;
    context.Products.Update(product);
    await context.SaveChangesAsync();
});

app.MapPost("/deleteProduct", async (AppDbContext context, [FromQuery] long id) =>
{
    context.Remove(await context.Products.FirstAsync(p => p.Id == id));
    await context.SaveChangesAsync();
});

app.Run();