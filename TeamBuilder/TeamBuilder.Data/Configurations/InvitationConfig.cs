namespace TeamBuilder.Data.Configurations
{
	using Microsoft.EntityFrameworkCore;
	using System;
	using TeamBuilder.Models;
	using Microsoft.EntityFrameworkCore.Metadata.Builders;

	public class InvitationConfig : IEntityTypeConfiguration<Invitation>
	{
		public void Configure(EntityTypeBuilder<Invitation> builder)
		{
			builder.HasKey(e => e.Id);
			builder.HasOne(e => e.InvitedUser).WithOne(e => e.Invitation).HasForeignKey<Invitation>(e => e.InvitedUserId);
			builder.HasOne(e => e.Team).WithOne(e => e.Invitation).HasForeignKey<Invitation>(e => e.TeamId);
		}
	}
}