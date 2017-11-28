namespace BusTicketsSystem.Data.Configurations
{
    using BusTicketsSystem.Data.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class BankAccountConfig : IEntityTypeConfiguration<BankAccount>
    {
        public void Configure(EntityTypeBuilder<BankAccount> builder)
        {
            builder.HasKey(e => e.AccountNumber);

            builder.Property(e => e.Balance)
                .IsRequired()
                .HasColumnType("DECIMAL(16, 2)");

            builder.HasOne(e => e.Customer)
                .WithOne(c => c.BankAccount)
                .HasForeignKey<BankAccount>(e => e.CustomerId);
        }
    }
}
