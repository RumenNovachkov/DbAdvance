namespace AllInOffice.Data.Config
{
    using AllInOffice.Data.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class EmployeeConfig : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.HasKey(e => e.EmployeeId);

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
            .Property(e => e.Salary)
            .HasColumnType("DECIMAL(16, 2)");

            builder
            .Property(e => e.BirthDate)
            .IsRequired()
            .HasColumnType("DATETIME2");

            builder
            .Property(e => e.Position)
            .IsRequired()
            .IsUnicode()
            .HasMaxLength(20);

            builder
            .Property(e => e.Email)
            .IsRequired()
            .IsUnicode()
            .HasMaxLength(50);

            builder
            .Property(e => e.HireDate)
            .IsRequired()
            .HasDefaultValueSql("GETDATE()")
            .HasColumnType("DATETIME2");

            builder
            .HasMany(e => e.OrdersServiced)
            .WithOne(os => os.EmployeeServicing)
            .HasForeignKey(e => e.EmployeeServecingId)
            .OnDelete(DeleteBehavior.Restrict);

            builder
                .Property(e => e.PhoneNumberId)
                .IsRequired(false);

            builder
                .Property(e => e.VehicleId)
                .IsRequired(false);

            builder
            .HasMany(e => e.Clients)
            .WithOne(c => c.ResponsiveEmployee)
            .HasForeignKey(e => e.ResponsiveEmployeeId);

            builder
            .Property(e => e.IsFired)
            .HasDefaultValue(false);
        }
    }
}
