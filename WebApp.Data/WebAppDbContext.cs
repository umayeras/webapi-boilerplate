using Microsoft.EntityFrameworkCore;
using WebApp.Model.Entities;

namespace WebApp.Data
{
    public class WebAppDbContext : DbContext
    {
        public WebAppDbContext(DbContextOptions<WebAppDbContext> options)
            : base(options)
        {
        }
        
        public DbSet<Sample> Samples { get; set; }
        public DbSet<Status> Statuses { get; set; }
    }
}