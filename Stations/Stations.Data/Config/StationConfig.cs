using Microsoft.EntityFrameworkCore;
using Stations.Models;
using System;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Stations.Data.Config
{
    public class StationConfig : IEntityTypeConfiguration<Station>
    {
        public void Configure(EntityTypeBuilder<Station> builder)
        {
            builder.HasKey(e => e.Id);

            builder.HasAlternateKey(e => e.Name);

            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(e => e.Town)
                .IsRequired()
                .HasMaxLength(50);
        }
    }
}
