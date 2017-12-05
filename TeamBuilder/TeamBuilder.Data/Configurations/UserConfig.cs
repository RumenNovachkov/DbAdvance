namespace TeamBuilder.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using System;
    using TeamBuilder.Models;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
 -            builder.HasKey(e => e.Id);
 -
 -            builder.HasAlternateKey(e => e.Username);
 -
 -            builder.Property(e => e.Username)
 -                .IsRequired()
 -                .HasMaxLength(25);
 -
 -            builder.Property(e => e.FirstName)
 -                .IsRequired()
 -                .HasMaxLength(25);
 -
 -            builder.Property(e => e.LastName)
 -                .IsRequired()
 -                .HasMaxLength(25);
 -
 -            builder.Property(e => e.Password);
 
 	      builder.Ignore(e => e.Invitation);
	}
    }
}
