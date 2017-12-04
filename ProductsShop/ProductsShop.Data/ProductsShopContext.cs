namespace ProductsShop.Data
{
    using Microsoft.EntityFrameworkCore;
    using ProductsShop.Data.Config;
    using ProductsShop.Models;

    public class ProductsShopContext : DbContext
    {
        public ProductsShopContext()
        {
        }

        public ProductsShopContext(DbContextOptions options)
            :base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<CategoryProducts> CategoriesProducts { get; set; }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(ServerConfig.ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new UserConfig());
            builder.ApplyConfiguration(new ProductConfig());
            builder.ApplyConfiguration(new CategoryConfig());
            builder.ApplyConfiguration(new CategoriesProductsConfig());
        }
    }
}
