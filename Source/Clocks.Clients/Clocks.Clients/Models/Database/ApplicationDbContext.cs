using Microsoft.EntityFrameworkCore;

namespace Clocks.Clients.Core.Models.Database
{
    /// <inheritdoc />
    /// <summary>
    /// Контекст данных для взаимодействия с базой данных SQLite посредствам Entity Framework ORM
    /// </summary>
    public class ApplicationDbContext : DbContext
    {
        private readonly string _databasePath;

        public DbSet<ClockType> ClockTypes { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Clock> Clocks { get; set; }

        public ApplicationDbContext(string databasePath)
        {
            _databasePath = databasePath;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Filename={_databasePath}");
        }
    }
}
