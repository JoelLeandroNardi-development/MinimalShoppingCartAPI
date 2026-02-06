using Microsoft.EntityFrameworkCore;

namespace MinimalAPICrud;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }

    public DbSet<CatalogItem> CatalogItems => Set<CatalogItem>();
    public DbSet<Client> Clients => Set<Client>();
    public DbSet<ShoppingCart> ShoppingCarts => Set<ShoppingCart>();
    public DbSet<ShoppingCartItem> ShoppingCartItems => Set<ShoppingCartItem>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new CatalogItemConfiguration());
        modelBuilder.ApplyConfiguration(new ClientConfiguration());
        modelBuilder.ApplyConfiguration(new ShoppingCartConfiguration());
        base.OnModelCreating(modelBuilder);
    }
}
