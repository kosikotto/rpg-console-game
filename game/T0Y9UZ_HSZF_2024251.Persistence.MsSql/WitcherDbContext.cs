using Microsoft.EntityFrameworkCore;
using T0Y9UZ_HSZF_2024251.Model.Entities;

namespace T0Y9UZ_HSZF_2024251.Persistence.MsSql
{
    public class WitcherDbContext : DbContext
    {
        public DbSet<Hero> HeroTable { get; set; }
        public DbSet<GameTask> GameTaskTable { get; set; }
        public DbSet<Monster> MonsterTable { get; set; }
        public DbSet<Resources> ResourcesTable { get; set; }
        public DbSet<DaysPassed> DaysTable { get; set; }

        public WitcherDbContext()
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connStr = @"Data Source=(LocalDB)\MSSQLLocalDB;Initial Catalog=WitcherDb;Integrated Security=True;MultipleActiveResultSets=true";
            optionsBuilder.UseSqlServer(connStr);
            base.OnConfiguring(optionsBuilder);
        }
    }
}
