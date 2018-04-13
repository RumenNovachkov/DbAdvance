using AllInOffice.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AllInOffice.Data.Config
{
    public class PhonenumberConfig : IEntityTypeConfiguration<PhoneNumber>
    {
        public void Configure(EntityTypeBuilder<PhoneNumber> builder)
        {
            builder.HasKey(e => e.PnoneNumberId);

            builder
            .Property(e => e.PnoneNumber)
            .IsRequired()
            .HasColumnType("CHAR(10)");

            builder
                .Property(e => e.ClientId)
                .IsRequired(false);

            builder
                .Property(e => e.EmployeeId)
                .IsRequired(false);

            builder
                .HasOne(e => e.Employee)
                .WithOne(em => em.PhoneNumber)
                .HasForeignKey<PhoneNumber>(e => e.EmployeeId);
        }
    }
}
