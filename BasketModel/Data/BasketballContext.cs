using MAP_Ionescu_Serban_Andrei.Models;
using Microsoft.EntityFrameworkCore;

namespace MAP_Ionescu_Serban_Andrei.Data_WebAPI
{
    public class BasketballContext : DbContext
    {
        public BasketballContext(DbContextOptions<BasketballContext> options) : base(options)
        {

        }

        public DbSet<Baller> Ballers { get; set; }
        public DbSet<Coach> Coaches { get; set; }
        public DbSet<GamePlan> GamePlans { get; set; }
        public DbSet<Match> Matches { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<PersonalStats> PersonalStats { get; set; } 

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Baller>().ToTable("Baller");
            modelBuilder.Entity<Coach>().ToTable("Coach");
            modelBuilder.Entity<GamePlan>().ToTable("GamePlan");
            modelBuilder.Entity<Match>().ToTable("Match");
            modelBuilder.Entity<Position>().ToTable("Position");
            modelBuilder.Entity<Team>().ToTable("Team");
            modelBuilder.Entity<PersonalStats>().ToTable("PersonalStats");

            modelBuilder.Entity<Position>()
                .HasKey(p => new { p.BallerID, p.MatchID });

            modelBuilder.Entity<Baller>().HasOne(t => t.Team).WithMany(b => b.Ballers).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
