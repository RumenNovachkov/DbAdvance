namespace ProductsShop.Data.Config
{
    using Microsoft.EntityFrameworkCore;
    using ProductsShop.Models;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class CategoryConfig : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(e => e.CategoryId);

            builder.HasMany(e => e.Products)
                .WithOne(p => p.Category)
                .HasForeignKey(e => e.CategoryId);
        }
    }
}
