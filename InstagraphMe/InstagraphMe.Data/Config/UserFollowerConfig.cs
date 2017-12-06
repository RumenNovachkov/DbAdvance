namespace InstagraphMe.Data.Config
{
    using InstagraphMe.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class UserFollowerConfig : IEntityTypeConfiguration<UserFollower>
    {
        public void Configure(EntityTypeBuilder<UserFollower> builder)
        {
            builder.HasKey(e => new { e.FollowerId, e.UserId});

            builder.HasOne(e => e.Follower)
                .WithMany(f => f.UsersFollower)
                .HasForeignKey(e => e.FollowerId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(e => e.User)
                .WithMany(u => u.Followers)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
