using Microsoft.EntityFrameworkCore;
using StoreApi.Data;
using StoreApi.Models;

namespace StoreApi.Repositories;

public class ProductRepository(AppDbContext context) : IProductRepository
{
    public async Task<(IEnumerable<Product> Items, int TotalCount)> GetProductsAsync(string? search, string? category, int page, int pageSize)
    {
        var query = context.Products.AsNoTracking().AsQueryable();

        // সার্চ ফিল্টারিং (নাম বা ক্যাটাগরিতে)
        if (!string.IsNullOrWhiteSpace(search))
        {
            var searchLower = search.Trim().ToLower();
            query = query.Where(p => p.Name.ToLower().Contains(searchLower) || 
                                     (p.Category != null && p.Category.ToLower().Contains(searchLower)));
        }

        // ক্যাটাগরি ফিল্টার
        if (!string.IsNullOrWhiteSpace(category) && category != "All")
        {
            query = query.Where(p => p.Category == category);
        }

        var totalCount = await query.CountAsync();

        // পেজিনেশন
        var items = await query
            .OrderByDescending(p => p.Id)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return (items, totalCount);
    }

    public async Task<Product?> GetByIdAsync(int id) => await context.Products.FindAsync(id);

    public async Task<Product> CreateAsync(Product product)
    {
        context.Products.Add(product);
        await context.SaveChangesAsync();
        return product;
    }

    public async Task<bool> UpdateAsync(Product product)
    {
        context.Products.Update(product);
        return await context.SaveChangesAsync() > 0;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var product = await context.Products.FindAsync(id);
        if (product is null) return false;

        context.Products.Remove(product);
        return await context.SaveChangesAsync() > 0;
    }
}