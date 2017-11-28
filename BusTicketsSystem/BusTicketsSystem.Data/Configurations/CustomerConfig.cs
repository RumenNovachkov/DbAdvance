namespace BusTicketsSystem.Data.Configurations
{
    using System;
    using Microsoft.EntityFrameworkCore;
    using BusTicketsSystem.Data.Models;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class CustomerConfig : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.HasKey(e => e.CustomerId);

            builder.Property(e => e.FirstName)
                .IsRequired()
                .IsUnicode()
                .HasMaxLength(20);

            builder.Property(e => e.FirstName)
                .IsRequired()
                .IsUnicode()
                .HasMaxLength(20);

            builder.Property(e => e.DateOfBirth)
                .HasColumnType("DATETIME2")
                .IsRequired();

            builder.HasMany(e => e.Tickets)
                .WithOne(t => t.Customer)
                .HasForeignKey(e => e.CustomerId);

            builder.Ignore(e => e.BankAccount);

            builder.Ignore(e => e.TicketId);

            //builder.HasMany(e => e.Reviews)
            //    .WithOne(r => r.Customer)
            //    .HasForeignKey(e => e.ReviewId);
        }
    }
}
