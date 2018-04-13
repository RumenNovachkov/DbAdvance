namespace AllInOffice.Data.Config
{
    using AllInOffice.Data.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class ProductConfig : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(e => e.ProductId);

            builder
            .Property(e => e.Name)
            .IsRequired()
            .IsUnicode()
            .HasMaxLength(100);

            builder
            .Property(e => e.Description)
            .IsRequired(false)
            .IsUnicode()
            .HasMaxLength(250);

            builder
            .Property(e => e.Categorie)
            .IsRequired();

            builder
            .Property(e => e.Price)
            .IsRequired()
            .HasDefaultValue(0.00m);
            
            builder
                .HasOne(e => e.Brand)
                .WithMany(b => b.Products)
                .HasForeignKey(e => e.BrandId);
        }
    }
}
