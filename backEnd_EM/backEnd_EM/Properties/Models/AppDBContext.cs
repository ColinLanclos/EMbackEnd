using backEnd_EM.Migrations;
using backEnd_EM.Properties.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;


namespace backEnd_EM.Models
{
    public class AppDBContext : DbContext
    {
        protected readonly IConfiguration Configuration;

        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
        {
        }

        public DbSet<Analyse> Analyses { get; set; }
        public DbSet<Appointments> Appointments { get; set; }
        public DbSet<Athletes> Athletes { get; set; }

        public DbSet<Guardians> Guardians { get; set; }
        public DbSet<ProgressTracker> ProgressTrackers { get; set; }

        public DbSet<Analytics> Analytics { get; set; }
    }
}