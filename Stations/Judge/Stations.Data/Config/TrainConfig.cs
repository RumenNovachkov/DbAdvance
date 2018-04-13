namespace Stations.Data.Config
{
    using Microsoft.EntityFrameworkCore;
    using Stations.Models;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class TrainConfig : IEntityTypeConfiguration<Train>
    {
        public void Configure(EntityTypeBuilder<Train> builder)
        {
            builder.HasKey(e => e.Id);

            builder.HasAlternateKey(e => e.TrainNumber);

            builder.Property(e => e.TrainNumber)
                .IsRequired()
                .HasMaxLength(10);
        }
    }
}
