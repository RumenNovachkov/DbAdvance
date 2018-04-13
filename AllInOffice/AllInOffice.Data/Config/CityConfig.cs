namespace AllInOffice.Data.Config
{
    using AllInOffice.Data.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class CityConfig : IEntityTypeConfiguration<City>
    {
        public void Configure(EntityTypeBuilder<City> builder)
        {
            builder.HasKey(e => e.CityId);

            builder
            .Property(e => e.CityName)
            .IsRequired()
            .IsUnicode()
            .HasMaxLength(50);
        }
    }
}
