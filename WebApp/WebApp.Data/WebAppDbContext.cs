using Microsoft.EntityFrameworkCore;
using WebApp.Model;

namespace WebApp.Data
{
    public class WebAppDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("connection-string");
        }

        public DbSet<Sample> Sample { get; set; }
    }
}