﻿namespace P03_FootballBetting.Data
{
    using Microsoft.EntityFrameworkCore;
    using P03_FootballBetting.Data.Models;

    public class FootballBettingContext : DbContext
    {
        public FootballBettingContext()
        {
        }

        public FootballBettingContext(DbContextOptions options)
            :base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Bet> Bets { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Color> Colors { get; set; }
        public DbSet<Town> Towns { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<PlayerStatistic> PlayerStatistics { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Configuration.ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            //Team
            builder.Entity<Team>(entity =>
            {
                entity.HasKey(e => e.TeamId);

                entity
                .Property(e => e.Name)
                .IsRequired()
                .IsUnicode()
                .HasMaxLength(80);

                entity
                .Property(e => e.Initials)
                .HasColumnType("NCHAR(3)");

                entity
                .Property(e => e.LogoUrl)
                .IsUnicode(false);

                entity
                .HasOne(t => t.PrimaryKitColor)
                .WithMany(c => c.PrimaryKitTeams)
                .HasForeignKey(e => e.PrimaryKitColorId)
                .OnDelete(DeleteBehavior.Restrict);

                entity
                .HasOne(t => t.SecondaryKitColor)
                .WithMany(c => c.SecondaryKitTeams)
                .HasForeignKey(e => e.SecondaryKitColorId)
                .OnDelete(DeleteBehavior.Restrict);

                entity
                .HasOne(t => t.Town)
                .WithMany(tw => tw.Teams)
                .HasForeignKey(t => t.TownId);
            });

            //Colors
            builder.Entity<Color>(entity =>
            {
                entity
                .HasKey(e => e.ColorId);

                entity
                .Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(40);
            });

            //Town
            builder.Entity<Town>(entity =>
            {
                entity
                .HasKey(e => e.TownId);

                entity
                .Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(80);

                entity
                .HasOne(t => t.Country)
                .WithMany(c => c.Towns)
                .HasForeignKey(t => t.CountryId);
            });

            //Country
            builder.Entity<Country>(entity =>
            {
                entity
                .HasKey(e => e.CountryId);

                entity
                .Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(80);
            });

            //Player
            builder.Entity<Player>(entity =>
            {
                entity
                .HasKey(e => e.PlayerId);

                entity
                .Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(100);

                entity
                .Property(e => e.IsInjured)
                .HasDefaultValue(false);

                entity
                .HasOne(e => e.Team)
                .WithMany(t => t.Players)
                .HasForeignKey(e => e.TeamId);

                entity
                .HasOne(e => e.Position)
                .WithMany(p => p.Players)
                .HasForeignKey(e => e.PositionId);
            });

            //Position
            builder.Entity<Position>(entity =>
            {
                entity
                .HasKey(e => e.PositionId);

                entity
                .Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(30);
            });

            //PlayerStatistics
            builder.Entity<PlayerStatistic>(entity =>
            {
                entity
                .HasKey(e => new { e.PlayerId, e.GameId });

                entity
                .HasOne(e => e.Game)
                .WithMany(g => g.PlayerStatistics)
                .HasForeignKey(e => e.GameId);

                entity
                .HasOne(e => e.Player)
                .WithMany(p => p.PlayerStatistics)
                .HasForeignKey(e => e.PlayerId);
            });

            //Game
            builder.Entity<Game>(entity =>
            {
                entity
                .HasKey(e => e.GameId);

                entity
                .HasOne(e => e.HomeTeam)
                .WithMany(h => h.HomeGames)
                .HasForeignKey(e => e.HomeTeamId)
                .OnDelete(DeleteBehavior.Restrict);

                entity
                .HasOne(e => e.AwayTeam)
                .WithMany(a => a.AwayGames)
                .HasForeignKey(e => e.AwayTeamId)
                .OnDelete(DeleteBehavior.Restrict);
            });

            //Bet
            builder.Entity<Bet>(entity =>
            {
                entity
                .HasKey(e => e.BetId);

                entity
                .HasOne(e => e.Game)
                .WithMany(g => g.Bets)
                .HasForeignKey(e => e.GameId);

                entity
                .HasOne(e => e.User)
                .WithMany(u => u.Bets)
                .HasForeignKey(e => e.UserId);
            });

            //User
            builder.Entity<User>(entity =>
            {
                entity
                .HasKey(e => e.UserId);

                entity
                .Property(e => e.Username)
                .IsRequired()
                .HasMaxLength(30);

                entity
                .Property(e => e.Password)
                .IsRequired()
                .HasMaxLength(20);

                entity
                .Property(e => e.Email)
                .IsRequired()
                .HasMaxLength(60);
                
                entity
                .Property(e => e.Name)
                .HasMaxLength(100);

                entity
                .Property(e => e.Balance)
                .HasDefaultValue(0.0m);
            });
        }
    }
}
