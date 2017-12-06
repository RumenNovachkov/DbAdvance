namespace InstagraphMe.Data
{
    using InstagraphMe.Data.Config;
    using InstagraphMe.Models;
    using Microsoft.EntityFrameworkCore;

    public class InstagraphMeContext : DbContext
    {
        public InstagraphMeContext()
        {
        }

        public InstagraphMeContext(DbContextOptions options)
            :base(options)
        {

        }

        public DbSet<Comment> Comments { get; set; }
        public DbSet<Picture> Pictures { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserFollower> UsersFollowers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(ServerConfig.ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new CommentConfig());

            builder.ApplyConfiguration(new PictureConfig());

            builder.ApplyConfiguration(new PostConfig());

            builder.ApplyConfiguration(new UserConfig());

            builder.ApplyConfiguration(new UserFollowerConfig());
        }
    }
}
