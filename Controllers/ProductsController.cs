using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StoreApi.Data;
using StoreApi.Models;

namespace StoreApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController(AppDbContext context) : ControllerBase
{
    // ১. সব প্রোডাক্টের লিস্ট দেখা: GET /api/products
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
    {
        return await context.Products.AsNoTracking().ToListAsync();
    }

    // ২. নির্দিষ্ট ID দিয়ে একটি প্রোডাক্ট দেখা: GET /api/products/1
    [HttpGet("{id:int}")]
    public async Task<ActionResult<Product>> GetProduct(int id)
    {
        var product = await context.Products.FindAsync(id);
        return product is null ? NotFound(new { message = "Product not found!" }) : product;
    }

    // ৩. নতুন প্রোডাক্ট যুক্ত করা: POST /api/products
    [HttpPost]
    public async Task<ActionResult<Product>> CreateProduct(Product product)
    {
        context.Products.Add(product);
        await context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);
    }

    // ৪. প্রোডাক্ট আপডেট করা: PUT /api/products/1
    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateProduct(int id, Product updatedProduct)
    {
        var product = await context.Products.FindAsync(id);
        if (product is null) return NotFound();

        product.Name = updatedProduct.Name;
        product.Category = updatedProduct.Category;
        product.Price = updatedProduct.Price;
        product.Stock = updatedProduct.Stock;

        await context.SaveChangesAsync();
        return NoContent();
    }

    // ৫. প্রোডাক্ট ডিলিট করা: DELETE /api/products/1
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteProduct(int id)
    {
        var product = await context.Products.FindAsync(id);
        if (product is null) return NotFound();

        context.Products.Remove(product);
        await context.SaveChangesAsync();
        return NoContent();
    }
}