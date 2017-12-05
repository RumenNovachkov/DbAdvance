namespace TeamBuilder.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using System;
    using TeamBuilder.Models;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class TeamConfig : IEntityTypeConfiguration<Team>
    {
        public void Configure(EntityTypeBuilder<Team> builder)
        {
 +			builder.HasKey(e => e.Id);
 +			
 +			builder.Property(e => e.Name).IsRequired().IsUnicode().HasMaxLength(25);
 +
 +			builder.Property(e => e.Description).IsUnicode().HasMaxLength(250);
 +
 +			builder.Property(e => e.StartDate).HasColumnType("DATETIME2");// {dd/MM/yyyy HH:mm} How?
 +			builder.Property(e => e.EndDate).HasColumnType("DATETIME2");//{dd/MM/yyyy HH:mm} Must be after StartDate
 +			builder.HasOne(e => e.Creator).WithMany(e => e.Events).HasForeignKey(e => e.CreatorId);
 
			builder.Ignore(e => e.Invitation);
	}
    }
}
