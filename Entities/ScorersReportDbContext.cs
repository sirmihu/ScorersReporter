using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using ScorersReporter.Models;

namespace ScorersReporter.Entities
{
    public class ScorersReportDbContext : DbContext
    {
        private readonly AppSettings _appSettings;
        /*private readonly string _connectionString;*/

        public ScorersReportDbContext(IOptions<AppSettings> options, IConfiguration configuration)
        {
            _appSettings = options.Value;
           /* var x = configuration.GetSection("ConnectionStrings:DatabaseAddress").Value;*/
        }

        public DbSet<Scorer> Scorers { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Scorer>()
            .HasKey(a => a.Id);

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_appSettings.DatabaseAddress);
        }
    }
}