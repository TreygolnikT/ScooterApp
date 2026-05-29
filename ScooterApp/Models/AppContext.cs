using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ScooterApp.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ScooterApp
{
    internal class AppContext : DbContext
    {
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Level> Levels { get; set; }
        public DbSet<Scooter> Scooters { get; set; }
        public DbSet<RentalHistory> RentalHistories { get; set; }
        public DbSet<Coord> Coords { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=Scooters.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>(
                entity =>
                {
                    entity.HasKey(r => r.RoleId);
                    entity.Property(r => r.Name).IsRequired();
                    entity.HasIndex(r => r.Name).IsUnique();
                });
            modelBuilder.Entity<RentalHistory>(
                entity =>
                {
                    entity.ToTable(r => r.HasCheckConstraint("CK_RentalHistory_Length", "Length > 0"));
                    entity.ToTable(r => r.HasCheckConstraint("CK_RentalHistory_Price", "Price > 0"));
                    entity.HasKey(r => r.RentalHistoryId);
                    entity.HasOne(r => r.Scooter).WithMany(s => s.RentalHistories).OnDelete(DeleteBehavior.SetNull); 
                });
            modelBuilder.Entity<Level>(
                entity =>
                {
                    entity.HasKey(l => l.LevelId);
                    entity.Property(l => l.Name).IsRequired();
                    entity.HasIndex(l => l.Name).IsUnique();
                });
            modelBuilder.Entity<User>(
                entity =>
                {
                    entity.HasKey(u => u.UserId);
                    entity.Property(u => u.Username).IsRequired()
                                                    .HasMaxLength(15);
                    entity.HasIndex(u => u.Username).IsUnique();
                    entity.Property(u => u.Password).IsRequired()
                                                    .HasMaxLength(20);
                    entity.Property(u => u.Points).HasDefaultValue(0);
                    entity.ToTable(u => u.HasCheckConstraint("CK_Balance_NotNegative", "Money >= 0"));
                    entity.ToTable(u => u.HasCheckConstraint("CK_Points_NotNegative", "Points >= 0"));
                });
            modelBuilder.Entity<Scooter>(
                entity =>
                {
                    entity.HasKey(s => s.ScooterId);
                    entity.Property(s => s.Model).IsRequired();
                    entity.ToTable(s => s.HasCheckConstraint("CK_Charge_NotNegative", "Charge >= 0"));
                });
        }

        public AppContext()
        {
            Database.EnsureCreated();
        }
    }
}
