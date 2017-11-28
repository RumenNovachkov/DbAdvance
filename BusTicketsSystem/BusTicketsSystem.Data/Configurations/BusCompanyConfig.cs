namespace BusTicketsSystem.Data.Configurations
{
    using BusTicketsSystem.Data.Models;
    using Microsoft.EntityFrameworkCore;
    using System;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class BusCompanyConfig : IEntityTypeConfiguration<BusCompany>
    {
        public void Configure(EntityTypeBuilder<BusCompany> builder)
        {
            builder.HasKey(e => e.BusCompanyId);

            builder.Property(e => e.BusCompanyName)
                .IsRequired()
                .IsUnicode()
                .HasMaxLength(50);

            builder.Property(e => e.Nationality)
                .IsRequired()
                .IsUnicode(false)
                .HasMaxLength(20);

            //builder.HasMany(e => e.Trips)
            //    .WithOne(t => t.BusCompany)
            //    .HasForeignKey(e => e.TripId);

            //builder.HasMany(e => e.Reviews)
            //    .WithOne(r => r.BusCompany)
            //    .HasForeignKey(e => e.ReviewId);
        }
    }
}
