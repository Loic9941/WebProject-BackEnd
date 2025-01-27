using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL
{
    public class DBContext : DbContext
    {
        public DbSet<Order> Orders { get; set; }

        public DBContext(DbContextOptions<DBContext> options) : base(options)
        {
        }
    }
}
