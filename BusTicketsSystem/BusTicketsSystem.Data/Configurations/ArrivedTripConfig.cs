namespace BusTicketsSystem.Data.Configurations
{
    using BusTicketsSystem.Data.Models;
    using Microsoft.EntityFrameworkCore;
    using System;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class ArrivedTripConfig : IEntityTypeConfiguration<ArrivedTrip>
    {
        public void Configure(EntityTypeBuilder<ArrivedTrip> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.DateTimeArrived)
                .HasColumnType("DATETIME2");

            //builder.Property(e => e.DestinationArrived)
            
        }
    }
}
