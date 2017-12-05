namespace TeamBuilder.Data
{
    using Microsoft.EntityFrameworkCore;
    using TeamBuilder.Data.Configurations;
    using TeamBuilder.Models;

    public class TeamBuilderContext : DbContext
    {
        public TeamBuilderContext()
        {
        }

        public TeamBuilderContext(DbContextOptions options)
            :base(options)
        {
        }

        public DbSet<Event> Events { get; set; }
        public DbSet<Invitation> Invitations { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<TeamEvent> TeamsEvents { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserTeam> UsersTeams { get; set; }

        protected internal virtual void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(ServerConfig.ConnectionString);
            }
        }
        protected internal virtual void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new EventConfig());

            builder.ApplyConfiguration(new InvitationConfig());

            builder.ApplyConfiguration(new TeamConfig());

            builder.ApplyConfiguration(new TeamEventConfig());

            builder.ApplyConfiguration(new UserConfig());

            builder.ApplyConfiguration(new UserTeamConfig());
        }
    }
}
