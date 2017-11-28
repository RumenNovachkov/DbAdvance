namespace BusTicketsSystem.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using BusTicketsSystem.Data.Models;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class ReviewConfig : IEntityTypeConfiguration<Review>
    {
        public void Configure(EntityTypeBuilder<Review> builder)
        {
            builder.HasKey(e => e.ReviewId);

            builder.Property(e => e.Content)
                .IsRequired()
                .IsUnicode()
                .HasMaxLength(255);

            builder.Property(e => e.DateAndTimeOfPublishing)
                .HasColumnType("DATETIME2")
                .HasDefaultValueSql("GETDATE()");
        }
    }
}
