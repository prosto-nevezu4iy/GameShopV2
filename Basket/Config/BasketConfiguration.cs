using BasketProject.Contracts.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BasketProject.Config
{
    public class BasketConfiguration : IEntityTypeConfiguration<Basket>
    {
        public void Configure(EntityTypeBuilder<Basket> builder)
        {
            builder.OwnsMany(b => b.Items, bi =>
            {
                bi.Property(i => i.UnitPrice)
                    .HasPrecision(10, 2);
            });
        }
    }
}
