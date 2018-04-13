namespace AllInOffice.Data.Config
{
    using AllInOffice.Data.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class AddressConfig : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.HasKey(e => e.AddressId);

            builder
                .Property(e => e.Location)
                .IsRequired()
                .IsUnicode()
                .HasMaxLength(250);
            
            builder
                .HasOne(e => e.City)
                .WithMany(c => c.Addresses)
                .HasForeignKey(e => e.CityId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
