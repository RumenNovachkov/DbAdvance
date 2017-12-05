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
 			
 			builder.Property(e => e.Name)
                .IsRequired()
                .IsUnicode()
                .HasMaxLength(25);
 
 			builder.Property(e => e.Description)
                .IsUnicode()
                .HasMaxLength(250);
            
	    }
    }
}
