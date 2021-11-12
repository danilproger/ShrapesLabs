using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WorldBehavior.Models;

namespace WorldBehavior
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
                    "Host=localhost;Port=5432;Database=WormsBehavior;Username=behaviorer_writer;Password=writer");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new BehaviorConfiguration());
            modelBuilder.ApplyConfiguration(new FoodPositionConfiguration());
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