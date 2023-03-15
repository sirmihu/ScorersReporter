using Microsoft.EntityFrameworkCore;
using ScorersReporter.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ScorersReporter.Entities
{
    public class ScorersReportDbContext : DbContext
    {
        private string _connectionString =
            "Server=(localdb)\\mssqllocaldb;DataBase=ScorersDb;Trusted_Connection=True;";
        public DbSet<Scorer> Scorers { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Scorer>()
            .HasKey(a => a.Id);

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
           
            optionsBuilder.UseSqlServer(_connectionString);
        }
    }
}