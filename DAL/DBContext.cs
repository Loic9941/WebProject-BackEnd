using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL
{
    public class DBContext : DbContext
    {
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<InvoiceItem> InvoiceItems { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Rating> Ratings { get; set; }


        public DBContext(DbContextOptions<DBContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            


        }
    }
}
