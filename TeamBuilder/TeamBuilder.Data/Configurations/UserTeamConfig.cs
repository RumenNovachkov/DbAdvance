namespace TeamBuilder.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using System;
    using TeamBuilder.Models;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class UserTeamConfig : IEntityTypeConfiguration<UserTeam>
    {
        public void Configure(EntityTypeBuilder<UserTeam> builder)
        {
            builder.HasKey(e => new { e.TeamId, e.UserId });

            builder.HasOne(e => e.User)
                .WithMany(u => u.Teams)
                .HasForeignKey(e => e.UserId);

            builder.HasOne(e => e.Team)
                .WithMany(t => t.Users)
                .HasForeignKey(e => e.TeamId);
        }
    }
}
