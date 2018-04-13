using AllInOffice.Data.Config;
using AllInOffice.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace AllInOffice.Data
{
    public class AllInOfficeDbContext : DbContext
    {
        public AllInOfficeDbContext()
        {
        }

        public AllInOfficeDbContext(DbContextOptions options)
            :base(options)
        {
        }

        public DbSet<Address> Addresses { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<PhoneNumber> PhoneNumbers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Configuration.ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new AddressConfig());

            builder.ApplyConfiguration(new BrandConfig());

            builder.ApplyConfiguration(new CityConfig());

            builder.ApplyConfiguration(new ClientConfig());

            builder.ApplyConfiguration(new DepartmentConfig());

            builder.ApplyConfiguration(new EmployeeConfig());

            builder.ApplyConfiguration(new OrderConfig());

            builder.ApplyConfiguration(new PhonenumberConfig());

            builder.ApplyConfiguration(new ProductConfig());

            builder.ApplyConfiguration(new VehicleConfig());
        }
    }
}
