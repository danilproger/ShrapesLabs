using System;
using System.Collections.Generic;
using System.Linq;
using CS_lab.BehaviorDatabase.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CS_lab.BehaviorDatabase
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext()
        {
        }

        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
        }

        public DbSet<Behavior> Behavior { get; set; }
        public DbSet<FoodPosition> FoodPositions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql(
                    "Host=localhost;Port=5432;Database=WormsBehavior;Username=behaviorer_reader;Password=reader");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new BehaviorConfiguration());
            modelBuilder.ApplyConfiguration(new FoodPositionConfiguration());
        }

        public List<FoodPosition> GetFoodListByBehavior(string behaviorName)
        {
            var behavior = Behavior.Where(p => p.Name == behaviorName).ToList();

            if (behavior.Count == 0)
            {
                return new List<FoodPosition>();
            }

            var foods = FoodPositions.Where(p => p.BehaviorId == behavior[0].Id).OrderBy(p => p.GameStep).ToList();

            return foods;
        }
    }

    public class BehaviorConfiguration : IEntityTypeConfiguration<Behavior>
    {
        public void Configure(EntityTypeBuilder<Behavior> builder)
        {
            builder.ToTable("behavior");
            builder.Property(p => p.Id).HasColumnName("id");
            builder.Property(p => p.Name).HasColumnName("name").IsRequired().HasMaxLength(256);
        }
    }

    public class FoodPositionConfiguration : IEntityTypeConfiguration<FoodPosition>
    {
        public void Configure(EntityTypeBuilder<FoodPosition> builder)
        {
            builder.ToTable("food_positions");
            builder.Property(p => p.Id).HasColumnName("id");
            builder.Property(p => p.BehaviorId).HasColumnName("behavior_id").IsRequired();
            builder.Property(p => p.GameStep).HasColumnName("game_step").IsRequired();
            builder.Property(p => p.X).HasColumnName("x").IsRequired();
            builder.Property(p => p.Y).HasColumnName("y").IsRequired();
        }
    }
}