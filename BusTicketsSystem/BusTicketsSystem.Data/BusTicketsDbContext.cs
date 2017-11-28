namespace BusTicketsSystem.Data
{
    using BusTicketsSystem.Data.Configurations;
    using BusTicketsSystem.Data.Models;
    using Microsoft.EntityFrameworkCore;

    public class BusTicketsDbContext : DbContext
    {
        public BusTicketsDbContext()
        {
        }

        public BusTicketsDbContext(DbContextOptions options)
            :base(options)
        {
        }

        public DbSet<BankAccount> BankAccounts { get; set; }
        public DbSet<BusCompany> BusCompanies { get; set; }
        public DbSet<BusStation> BusStations { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<Town> Towns { get; set; }
        public DbSet<Trip> Trips { get; set; }
        public DbSet<ArrivedTrip> ArrivedTrips { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(ServerConfig.ConnectionString);
            }
        }
        
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new BankAccountConfig());

            builder.ApplyConfiguration(new BusStationConfig());

            builder.ApplyConfiguration(new BusCompanyConfig());

            builder.ApplyConfiguration(new CustomerConfig());

            builder.ApplyConfiguration(new ReviewConfig());

            builder.ApplyConfiguration(new TicketConfig());

            builder.ApplyConfiguration(new TownConfig());

            builder.ApplyConfiguration(new TripConfig());

            builder.ApplyConfiguration(new ArrivedTripConfig());
        }
    }
}
