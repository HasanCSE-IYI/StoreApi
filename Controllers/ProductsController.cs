using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StoreApi.Data;
using StoreApi.Models;

namespace StoreApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController(AppDbContext context) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
    {
        return await context.Products.AsNoTracking().ToListAsync();
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Product>> GetProduct(int id)
    {
        var product = await context.Products.FindAsync(id);
        return product is null ? NotFound(new { message = "Product not found!" }) : product;
    }

    [HttpPost]
    public async Task<ActionResult<Product>> CreateProduct(ProductCreateDto dto)
    {
        var product = new Product
        {
            Name = dto.Name,
            Category = dto.Category,
            Price = dto.Price,
            Stock = dto.Stock
        };

        context.Products.Add(product);
        await context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateProduct(int id, ProductUpdateDto dto)
    {
        var product = await context.Products.FindAsync(id);
        if (product is null) return NotFound(new { message = "Product not found!" });

        product.Name = dto.Name;
        product.Category = dto.Category;
        product.Price = dto.Price;
        product.Stock = dto.Stock;

        await context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteProduct(int id)
    {
        var product = await context.Products.FindAsync(id);
        if (product is null) return NotFound(new { message = "Product not found!" });

        context.Products.Remove(product);
        await context.SaveChangesAsync();
        return NoContent();
    }
}