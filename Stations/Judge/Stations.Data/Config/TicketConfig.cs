namespace Stations.Data.Config
{
    using Microsoft.EntityFrameworkCore;
    using Stations.Models;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class TicketConfig : IEntityTypeConfiguration<Ticket>
    {
        public void Configure(EntityTypeBuilder<Ticket> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Price)
                .IsRequired()
                .HasColumnType("DECIMAL");

            builder.Property(e => e.SeatingPlace)
                .IsRequired()
                .HasMaxLength(8);

            builder.HasOne(e => e.CustomerCard)
                .WithMany(cc => cc.BoughtTickets)
                .HasForeignKey(e => e.CustomerCardId);
        }
    }
}
