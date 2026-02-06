using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MinimalAPICrud;

public class ClientConfiguration : IEntityTypeConfiguration<Client>
{
    public void Configure(EntityTypeBuilder<Client> builder)
    {
        builder.HasKey(c => c.Id);
        builder.Property(c => c.Name).IsRequired().HasMaxLength(150);
        builder.Property(c => c.PhoneNumber).HasMaxLength(50);
        builder.HasMany(c => c.ShoppingCarts)
               .WithOne()
               .OnDelete(DeleteBehavior.Cascade);
    }
}
