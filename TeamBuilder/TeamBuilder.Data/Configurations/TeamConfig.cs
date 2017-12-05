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
            builder.HasKey(e => e.Id);
            
            builder.HasAlternateKey(e => e.Name);
            
            builder.Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(25);
            
            builder.Property(e => e.Description)
            .HasMaxLength(32);
            
            builder.Property(e => e.Acronym)
            .HasColumnType("CHAR(3)")
            .IsRequired();
        }
    }
}
