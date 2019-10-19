using Microsoft.EntityFrameworkCore;
using Netways.EntityFramworkCore.Migrations.Seed;
using Netways.EntityFramworkCore.Model;

namespace Netways.EntityFramworkCore.DBContext
{

    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options) { }
        public DbSet<Country> Countries { get; set; }
        public DbSet<User> Users { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            SeedHelper.Fill(modelBuilder);
        }

    }
}
