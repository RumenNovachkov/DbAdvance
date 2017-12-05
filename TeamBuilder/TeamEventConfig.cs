namespace TeamBuilder.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using System;
    using TeamBuilder.Models;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class TeamEventConfig : IEntityTypeConfiguration<TeamEvent>
    {
        public void Configure(EntityTypeBuilder<TeamEvent> builder)
        {
            builder.HasOne(e => e.Team).WithMany(e => e.Events).HasForeignKey(e => e.EventId);

            builder.HasOne(e => e.Event).WithMany(e => e.Teams).HasForeignKey(e => e.TeamId);
        }
    }
}
