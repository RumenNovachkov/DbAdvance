namespace Stations.Data.Config
{
    using Microsoft.EntityFrameworkCore;
    using Stations.Models;
    using System;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class TripConfig : IEntityTypeConfiguration<Trip>
    {
        public void Configure(EntityTypeBuilder<Trip> builder)
        {
            builder.HasKey(e => e.Id);

            builder.HasOne(e => e.OriginStation)
                .WithMany(os => os.TripsFrom)
                .HasForeignKey(e => e.OriginStationId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(e => e.DestinationStation)
                .WithMany(ds => ds.TripsTo)
                .HasForeignKey(e => e.DestinationStationId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(e => e.DepartureTime)
                .IsRequired()
                .HasColumnType("DATETIME2");

            builder.Property(e => e.ArrivalTime)
                .IsRequired()
                .HasColumnType("DATETIME2");

            builder.HasOne(e => e.Train)
                .WithMany(t => t.Trips)
                .HasForeignKey(e => e.TrainId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(e => e.Status)
                .HasDefaultValue(TripStatus.OnTime);
        }
    }
}
