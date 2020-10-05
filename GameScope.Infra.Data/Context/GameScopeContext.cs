using GameScope.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameScope.Infra.Data.Context
{
    public class GameScopeContext : DbContext
    {
        public GameScopeContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasKey(e => new { e.Id });

            modelBuilder.Entity<Game>().HasKey(e => new { e.Id });

            modelBuilder.Entity<Rating>().HasKey(e => new { e.UserId, e.GameId });

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            modelBuilder.Entity<Game>()
                .HasOne(e => e.User)
                .WithMany(e => e.Games)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Rating>()
                .HasOne(e => e.User)
                .WithMany(e => e.Ratings)
                .IsRequired(true)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Rating>()
                .HasOne(e => e.Game)
                .WithMany(e => e.Ratings)
                .IsRequired(true)
                .HasForeignKey(e => e.GameId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<User>()
                .Property(e => e.UpdatedDate)
                .HasDefaultValueSql(null);

            modelBuilder.Entity<Game>()
                .Property(e => e.ReleaseDate)
                .HasDefaultValueSql(null);

            modelBuilder.Entity<Game>()
                .Property(e => e.UpdatedDate)
                .HasDefaultValueSql(null);

            modelBuilder.Entity<Rating>()
                .Property(e => e.UpdatedDate)
                .HasDefaultValueSql(null);

        }

        // DbSet Collections
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Game> Games { get; set; }
        public virtual DbSet<Rating> Ratings { get; set; }
    }
}
