using Microsoft.EntityFrameworkCore;
using StoreApi.Models;

namespace StoreApi.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Product> Products => Set<Product>();
    public DbSet<User> Users => Set<User>(); // new Users table
}