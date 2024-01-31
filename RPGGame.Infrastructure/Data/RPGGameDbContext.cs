using Microsoft.EntityFrameworkCore;
using RPGGame.Infrastructure.Models;

namespace RPGGame.Infrastructure.Data
{
    public class RPGGameDbContext : DbContext
    {
        public RPGGameDbContext()
        {
        }

        public RPGGameDbContext(DbContextOptions<RPGGameDbContext> options) : base(options)
        {
        }

        public DbSet<Player>? Players { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder
                    .UseSqlServer("Server=DESKTOP-1FK883M\\SQLEXPRESS;Database=RPGGame;Trusted_Connection=True;");
            }
        }
    }
}
