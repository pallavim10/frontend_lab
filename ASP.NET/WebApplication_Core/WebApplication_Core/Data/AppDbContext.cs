using Microsoft.EntityFrameworkCore;
using WebApplication_Core.Models;
namespace WebApplication_Core.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) 
        {
        
        }
        public DbSet<Staff> Staff { get; set; }
    }
}
