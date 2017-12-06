﻿namespace InstagraphMe.Data.Config
{
    using InstagraphMe.Models;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class UserConfig : Microsoft.EntityFrameworkCore.IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(e => e.Id);

            builder.HasAlternateKey(e => e.Username);

            builder.Property(e => e.Username)
                .IsRequired()
                .HasMaxLength(30);

            builder.Property(e => e.Password)
                .IsRequired()
                .HasMaxLength(20);

            builder.HasOne(e => e.ProfilePicture)
                .WithMany(pp => pp.Users)
                .HasForeignKey(e => e.ProfilePictureId);
        }
    }
}
