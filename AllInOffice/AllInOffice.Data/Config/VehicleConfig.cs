namespace AllInOffice.Data.Config
{
    using AllInOffice.Data.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class VehicleConfig : IEntityTypeConfiguration<Vehicle>
    {
        public void Configure(EntityTypeBuilder<Vehicle> builder)
        {
            builder.HasKey(e => e.VehicleId);

            builder
            .Property(e => e.RegNumber)
            .IsRequired()
            .IsUnicode(false)
            .HasMaxLength(8);

            builder
            .Property(e => e.Brand)
            .IsRequired()
            .IsUnicode()
            .HasMaxLength(15);

            builder
            .Property(e => e.Model)
            .IsRequired()
            .IsUnicode()
            .HasMaxLength(15);
            
            builder
            .Property(e => e.DateRegistrated)
            .HasDefaultValueSql("GETDATE()")
            .HasColumnType("DATETIME2");
        }
    }
}
