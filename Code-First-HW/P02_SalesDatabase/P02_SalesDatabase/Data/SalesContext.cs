namespace P03_SalesDatabase.Data
{
    using Microsoft.EntityFrameworkCore;
    using P03_SalesDatabase.Data.Models;

    public class SalesContext : DbContext
    {
        public SalesContext()
        {
        }

        public SalesContext(DbContextOptions options)
            :base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Store> Stores { get; set; }
        public DbSet<Sale> Sales { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionBuilder)
        {
            if (!optionBuilder.IsConfigured)
            {
                optionBuilder.UseSqlServer(Configuration.ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>(entity =>
            {
                entity
                .HasKey(e => e.ProductId);

                entity
                .Property(e => e.Name)
                .HasColumnType("NVARCHAR(50)");
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.HasKey(e => e.CustomerId);

                entity
                .Property(e => e.Name)
                .HasColumnType("NVARCHAR(100)");

                entity
                .Property(e => e.Email)
                .HasColumnType("VARCHAR(80)");
            });

            modelBuilder.Entity<Store>(entity =>
            {
                entity
                .HasKey(e => e.StoreId);

                entity
                .Property(e => e.Name)
                .HasColumnType("NVARCHAR(80)");
            });

            modelBuilder.Entity<Sale>(entity =>
            {
                entity
                .HasKey(e => e.SaleId);

                entity
                .Property(e => e.Date)
                .HasColumnType("DATETIME")
                .HasDefaultValueSql("GETDATE()");

                entity
                .HasOne(e => e.Product)
                .WithMany(s => s.Sale)
                .HasForeignKey(e => e.ProductId);

                entity
                .HasOne(e => e.Customer)
                .WithMany(s => s.Sale)
                .HasForeignKey(e => e.CustomerId);

                entity
                .HasOne(e => e.Store)
                .WithMany(s => s.Sale)
                .HasForeignKey(e => e.StoreId);
            });
        }
    }
}
