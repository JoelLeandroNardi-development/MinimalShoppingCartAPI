using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MinimalAPICrud;

public class ShoppingCartConfiguration : IEntityTypeConfiguration<ShoppingCart>
{
    public void Configure(EntityTypeBuilder<ShoppingCart> builder)
    {
        builder.HasKey(c => c.Id);
        builder.HasMany(c => c.Items)
               .WithOne()
               .OnDelete(DeleteBehavior.Cascade);
    }
}
