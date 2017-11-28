namespace BusTicketsSystem.Data.Configurations
{
    using System;
    using Microsoft.EntityFrameworkCore;
    using BusTicketsSystem.Data.Models;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class BusStationConfig : IEntityTypeConfiguration<BusStation>
    {
        public void Configure(EntityTypeBuilder<BusStation> builder)
        {
            builder.HasKey(e => e.BusStationId);

            builder.Property(e => e.BusStationName)
                .IsRequired()
                .IsUnicode()
                .HasMaxLength(50);

            builder.HasMany(e => e.DepartureTrips)
                .WithOne(dt => dt.OriginBusStation)
                .HasForeignKey(e => e.OriginBusStationId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(e => e.ArrivingTrips)
                .WithOne(at => at.DestinationBusStation)
                .HasForeignKey(e => e.DestinationBusStationId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
