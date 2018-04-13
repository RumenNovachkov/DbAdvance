namespace AllInOffice.Data.Config
{
    using AllInOffice.Data.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class ClientConfig : IEntityTypeConfiguration<Client>
    {
        public void Configure(EntityTypeBuilder<Client> builder)
        {
            builder.HasKey(e => e.ClientId);

            builder
            .Property(e => e.FirstName)
            .IsRequired()
            .IsUnicode()
            .HasMaxLength(50);

            builder
            .Property(e => e.LastName)
            .IsRequired()
            .IsUnicode()
            .HasMaxLength(20);

            builder
            .Property(e => e.Email)
            .IsRequired()
            .IsUnicode()
            .HasMaxLength(50);

            builder
            .Property(e => e.Password)
            .IsRequired()
            .IsUnicode(false)
            .HasMaxLength(20);

            builder
            .Property(e => e.CompanyName)
            .IsRequired()
            .IsUnicode()
            .HasMaxLength(50);

            builder
            .Property(e => e.BirthDate)
            .HasColumnType("CHAR(20)");

            builder
            .Property(e => e.IBan)
            .IsRequired(false)
            .HasColumnType("CHAR(20)");

            builder
            .HasMany(e => e.Orders)
            .WithOne(o => o.Client)
            .HasForeignKey(e => e.ClientId)
            .OnDelete(DeleteBehavior.Restrict);
            
            builder
            .HasMany(e => e.PhoneNumbers)
            .WithOne(p => p.Client)
            .HasForeignKey(e => e.ClientId)
            .OnDelete(DeleteBehavior.Restrict);

            builder
                .Property(e => e.ResponsiveEmployeeId)
                .IsRequired(false);

            builder
            .Property(e => e.IsDeleted)
            .HasDefaultValue(false);
        }
    }
}
