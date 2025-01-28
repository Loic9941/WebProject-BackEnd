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
            modelBuilder.Entity<Rating>()
                .HasOne(r => r.Product)
                .WithMany(p => p.Ratings)
                .HasForeignKey(r => r.ProductId)
                .OnDelete(deleteBehavior: DeleteBehavior.Cascade);
            modelBuilder.Entity<Product>()
                .HasMany(p => p.Ratings)
                .WithOne(r => r.Product)
                .HasForeignKey(r => r.ProductId)
                .OnDelete(deleteBehavior: DeleteBehavior.Cascade);
            modelBuilder.Entity<Rating>()
                .HasOne(r => r.Author)
                .WithMany(c => c.Ratings)
                .HasForeignKey(r => r.AuthorId)
                .OnDelete(deleteBehavior: DeleteBehavior.Cascade);
            modelBuilder.Entity<Contact>()
                .HasMany(c => c.Ratings)
                .WithOne(r => r.Author)
                .HasForeignKey(r => r.AuthorId)
                .OnDelete(deleteBehavior: DeleteBehavior.Cascade);
        }
    }
}
