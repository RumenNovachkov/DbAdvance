namespace BusTicketsSystem.Data.Configurations
{
    using System;
    using Microsoft.EntityFrameworkCore;
    using BusTicketsSystem.Data.Models;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class TripConfig : IEntityTypeConfiguration<Trip>
    {
        public void Configure(EntityTypeBuilder<Trip> builder)
        {
            builder.HasKey(e => e.TripId);

            builder.Property(e => e.DepartureTime)
                .HasColumnType("DATETIME2")
                .IsRequired();

            builder.Property(e => e.ArrivalTime)
                .HasColumnType("DATETIME2")
                .IsRequired();

            builder.Property(e => e.Status)
                .IsRequired();

            //builder.HasMany(e => e.Tickets)
            //    .WithOne(t => t.Trip)
            //    .HasForeignKey(e => e.TicketId);
        }
    }
}
