namespace BusTicketsSystem.Data.Configurations
{
    using System;
    using Microsoft.EntityFrameworkCore;
    using BusTicketsSystem.Data.Models;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class TownConfig : IEntityTypeConfiguration<Town>
    {
        public void Configure(EntityTypeBuilder<Town> builder)
        {
            builder.HasKey(e => e.TownId);

            builder.Property(e => e.TownName)
                .IsRequired()
                .IsUnicode()
                .HasMaxLength(25);

            builder.Property(e => e.Country)
                .IsRequired()
                .IsUnicode()
                .HasMaxLength(25);

            //builder.HasMany(e => e.BusStations)
            //    .WithOne(bs => bs.Town)
            //    .HasForeignKey(e => e.BusStationId);

            //builder.HasMany(e => e.Customers)
            //    .WithOne(c => c.HomeTown)
            //    .HasForeignKey(e => e.CustomerId);
        }
    }
}
