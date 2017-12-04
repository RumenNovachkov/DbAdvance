namespace ProductsShop.Data.Config
{
    using Microsoft.EntityFrameworkCore;
    using ProductsShop.Models;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class CategoriesProductsConfig : IEntityTypeConfiguration<CategoryProducts>
    {
        public void Configure(EntityTypeBuilder<CategoryProducts> builder)
        {
            builder.HasKey(e => new { e.CategoryId, e.ProductId});
        }
    }
}
