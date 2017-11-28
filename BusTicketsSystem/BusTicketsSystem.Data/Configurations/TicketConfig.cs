namespace BusTicketsSystem.Data.Configurations
{
    using System;
    using Microsoft.EntityFrameworkCore;
    using BusTicketsSystem.Data.Models;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class TicketConfig : IEntityTypeConfiguration<Ticket>
    {
        public void Configure(EntityTypeBuilder<Ticket> builder)
        {
            builder.HasKey(e => e.TicketId);

            builder.Property(e => e.Price)
                .HasColumnType("DECIMAL(8, 2)")
                .IsRequired();

            builder.Property(e => e.Seat)
                .IsRequired();
    }
    }
}
