﻿namespace Stations.Data
{
    using Microsoft.EntityFrameworkCore;
    using Stations.Data.Config;
    using Stations.Models;

    public class StationsDbContext : DbContext
	{
		public StationsDbContext()
		{
		}

		public StationsDbContext(DbContextOptions options)
			: base(options)
		{
		}

        public DbSet<CustomerCard> CustomersCards { get; set; }
        public DbSet<SeatingClass> SeatingClasses { get; set; }
        public DbSet<Station> Stations { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<Train> Trains { get; set; }
        public DbSet<TrainSeat> TrainSeats { get; set; }
        public DbSet<Trip> Trips { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			if (!optionsBuilder.IsConfigured)
			{
				optionsBuilder.UseSqlServer(Configuration.ConnectionString);
			}
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
            modelBuilder.ApplyConfiguration(new CustomerCardConfig());
            modelBuilder.ApplyConfiguration(new SeatingClassConfig());
            modelBuilder.ApplyConfiguration(new StationConfig());
            modelBuilder.ApplyConfiguration(new TicketConfig());
            modelBuilder.ApplyConfiguration(new TrainConfig());
            modelBuilder.ApplyConfiguration(new TrainSeatConfig());
            modelBuilder.ApplyConfiguration(new TripConfig());
		}
	}
}