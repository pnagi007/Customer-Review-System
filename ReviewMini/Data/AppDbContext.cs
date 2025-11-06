using Microsoft.EntityFrameworkCore;
using ReviewMini.Models;

namespace ReviewMini.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Product> Products { get; set; } = null!;
        public DbSet<Review> Reviews { get; set; } = null!;
        public DbSet<Customer> Customers { get; set; } = null!;
    }
}
