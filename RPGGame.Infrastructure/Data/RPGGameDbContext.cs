using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
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
                var applicationAssembly = AppDomain.CurrentDomain.GetAssemblies()
                    .FirstOrDefault(x => x.FullName!.Contains(nameof(RPGGame)));

                var connectionString = new ConfigurationBuilder()
                    .AddUserSecrets(applicationAssembly)
                    .Build()
                    .GetConnectionString("RPGGameConnectionString");

                optionsBuilder
                    .UseSqlServer(connectionString);
            }
        }
    }
}
