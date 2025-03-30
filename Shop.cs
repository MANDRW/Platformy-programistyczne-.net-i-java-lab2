using Microsoft.EntityFrameworkCore;

namespace Lab2;

public class Shop:DbContext
{
    public DbSet<Product> products { get; set; }
    public DbSet<Category> categories { get; set; }

    public Shop()
    {
        Database.EnsureCreated();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseSqlite(@"Data Source=shop.db");
    }
}