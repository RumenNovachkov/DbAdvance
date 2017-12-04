namespace ProductsShop.Data.Config
{
    using Microsoft.EntityFrameworkCore;
    using ProductsShop.Models;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class ProductConfig : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(e => e.ProductId);

            builder.Property(e => e.Name)
                .IsRequired()
                .IsUnicode()
                .HasMaxLength(200);

            builder.Property(e => e.Price)
                .HasColumnType("DECIMAL(16, 2)")
                .IsRequired();

            builder.HasOne(e => e.Seller)
                .WithMany(s => s.soldProducts)
                .HasForeignKey(e => e.SellerId);

            builder.HasOne(e => e.Buyer)
                .WithMany(b => b.boughtProducts)
                .HasForeignKey(e => e.BuyerId);

            builder.HasMany(e => e.Categories)
                .WithOne(c => c.Product)
                .HasForeignKey(e => e.ProductId);

            builder.Ignore(e => e.Categories);
        }
    }
}
