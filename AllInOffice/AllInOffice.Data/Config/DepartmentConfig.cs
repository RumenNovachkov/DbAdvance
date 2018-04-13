namespace AllInOffice.Data.Config
{
    using AllInOffice.Data.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class DepartmentConfig : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {
            builder
                .Property(e => e.Name)
                .IsRequired()
                .IsUnicode()
                .HasMaxLength(20);

            builder
            .HasMany(e => e.Employees)
            .WithOne(em => em.Department)
            .HasForeignKey(e => e.EmployeeId);
        }
    }
}
