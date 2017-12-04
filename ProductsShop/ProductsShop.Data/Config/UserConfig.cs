namespace ProductsShop.Data.Config
{
    using Microsoft.EntityFrameworkCore;
    using ProductsShop.Models;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(e => e.UserId);

            builder.Property(e => e.FirstName)
                .IsRequired(false)
                .IsUnicode()
                .HasMaxLength(100);

            builder.Property(e => e.LastName)
                .IsRequired()
                .IsUnicode()
                .HasMaxLength(100);

            builder.Property(e => e.Age)
                .IsRequired(false);

            builder.HasMany(e => e.boughtProducts)
                .WithOne(bp => bp.Buyer)
                .HasForeignKey(e => e.BuyerId);

            builder.HasMany(e => e.soldProducts)
                .WithOne(sp => sp.Seller)
                .HasForeignKey(e => e.SellerId);
        }
    }
}
