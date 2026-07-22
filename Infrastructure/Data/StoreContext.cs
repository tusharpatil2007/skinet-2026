using Core.Entities;
using Microsoft.EntityFrameworkCore;

public class StoreContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Product> Products {get; set;}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ProductConfiguration).Assembly);
    }
}