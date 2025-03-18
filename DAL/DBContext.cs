using Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DAL
{
    public class DBContext :DbContext
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

            base.OnModelCreating(modelBuilder);

            //Product
            modelBuilder.Entity<Rating>()
                .HasOne(r => r.Product)
                .WithMany(p => p.Ratings)
                .HasForeignKey(r => r.ProductId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Product>()
                .HasMany(p => p.Ratings)
                .WithOne(r => r.Product)
                .HasForeignKey(r => r.ProductId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Product>()
                .HasOne(p => p.Contact)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.ContactId)
                .OnDelete(DeleteBehavior.Cascade);

            //Rating
            modelBuilder.Entity<Rating>()
                .HasOne(r => r.Contact)
                .WithMany(c => c.Ratings)
                .HasForeignKey(r => r.ContactId)
                .OnDelete(DeleteBehavior.Restrict);

            //Contact
            modelBuilder.Entity<Contact>()
                .HasMany(c => c.Ratings)
                .WithOne(r => r.Contact)
                .HasForeignKey(r => r.ContactId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Contact>()
                .HasMany(c => c.Products)
                .WithOne(p => p.Contact)
                .HasForeignKey(p => p.ContactId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
