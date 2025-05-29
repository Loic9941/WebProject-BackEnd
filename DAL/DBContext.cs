using Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using static System.Net.Mime.MediaTypeNames;

namespace DAL
{
    public class DBContext :DbContext
    {
        public DbSet<User> Users { get; set; }
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
            modelBuilder.Entity<Product>()
                .HasOne(p => p.User)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Product>()
                .Property(p => p.Price)
                .HasColumnType("money");
            modelBuilder.Entity<Product>()
                .HasMany(p => p.InvoiceItems)
                .WithOne(ii => ii.Product)
                .OnDelete(DeleteBehavior.SetNull);

            //Rating
          
            modelBuilder.Entity<User>()
                .HasMany(c => c.Products)
                .WithOne(p => p.User)
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            //Invoice
            modelBuilder.Entity<Invoice>()
                .HasOne(i => i.User)
                .WithMany(c => c.Invoices)
                .HasForeignKey(i => i.UserId)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Invoice>()
                .HasMany(i => i.InvoiceItems)
                .WithOne(ii => ii.Invoice)
                .HasForeignKey(ii => ii.InvoiceId)
                .OnDelete(DeleteBehavior.Cascade);

            //InvoiceItem
            modelBuilder.Entity<InvoiceItem>()
                .HasOne(ii => ii.Invoice)
                .WithMany(i => i.InvoiceItems)
                .HasForeignKey(ii => ii.InvoiceId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<InvoiceItem>()
                .HasOne(ii => ii.Product)
                .WithMany(p => p.InvoiceItems)
                .HasForeignKey(ii => ii.ProductId)
                .OnDelete(DeleteBehavior.SetNull);
            modelBuilder.Entity<InvoiceItem>()
                .HasOne(ii => ii.User)
                .WithMany(c => c.InvoiceItems)
                .HasForeignKey(ii => ii.UserId)
                .OnDelete(DeleteBehavior.ClientCascade);
            modelBuilder.Entity<InvoiceItem>()
                .Property(p => p.UnitPrice)
                .HasColumnType("money");


            //Data migration
            modelBuilder.Entity<User>(u =>
            {
                u.HasData(
                    new User { Id = 1, Email = "client1@test.be", Salt = "Thursday", PasswordHash = "D6B1EEBBA6E4556DA0E2", Role = "Customer", Firstname = "Bernard", Lastname = "Lacheteur" },
                    new User { Id = 2, Email = "client2@test.be", Salt = "Thursday", PasswordHash = "D6B1EEBBA6E4556DA0E2", Role = "Customer", Firstname = "Fred", Lastname = "Duclient" },
                    new User { Id = 3, Email = "admin@test.be", Salt = "Thursday", PasswordHash = "D6B1EEBBA6E4556DA0E2", Role = "Administrator", Firstname = "Gérard", Lastname = "Ladmin" },
                    new User { Id = 4, Email = "dhl@test.be", Salt = "Thursday", PasswordHash = "D6B1EEBBA6E4556DA0E2", Role = "DeliveryPartner", Firstname= "DHL", Lastname = "DHL"},
                    new User { Id = 5, Email = "bpost@test.be", Salt = "Thursday", PasswordHash = "D6B1EEBBA6E4556DA0E2", Role = "DeliveryPartner", Firstname = "Bpost", Lastname = "Bpost" },
                    new User { Id = 6, Email = "artiste1@test.be", Salt = "Thursday", PasswordHash = "D6B1EEBBA6E4556DA0E2", Role = "Artisan", Firstname = "Rodin", Lastname = " " },
                    new User { Id = 7, Email = "artiste2@test.be", Salt = "Thursday", PasswordHash = "D6B1EEBBA6E4556DA0E2", Role = "Artisan", Firstname = "Leonard", Lastname = "De vinci" },
                    new User { Id = 8, Email = "artiste3@test.be", Salt = "Thursday", PasswordHash = "D6B1EEBBA6E4556DA0E2", Role = "Artisan", Firstname = "Salvador", Lastname = "Dali" }
                );
            });

            modelBuilder.Entity<Product>(p =>
            {
                p.HasData(
                    new Product
                    {
                        Id = 1,
                        UserId = 6,
                        Description = "Peinture sur toile datant de 1920",
                        Name = "Coucher de soleil sur le fleuve",
                        Price = 500,
                        Category = "Peinture",
                        Available = true,
                        Image = "/images/491eea5e-ba08-46b5-8404-f73bce32f872_A1QdHDA29ML._AC_UF1000,1000_QL80_.jpg",
                        CreatedAt = new DateTime(2025, 5, 29, 14, 46, 41)
                    },
                    new Product
                    {
                        Id = 2,
                        UserId = 6,
                        Description = "Aquarelle datant de 1963",
                        Name = "Bord de mer",
                        Price = 245,
                        Category = "Peinture",
                        Available = true,
                        Image = "/images/3bcd32f9-5452-427a-8219-2f863051a08f_images.jpg",
                        CreatedAt = new DateTime(2025, 5, 29, 16, 43, 43)
                    },
                    new Product
                    {
                        Id = 3,
                        UserId = 6,
                        Description = "Peinture abstraite aux tonalités bleues inspirantes",
                        Name = "Peinture abstraite",
                        Price = 300,
                        Category = "Peinture",
                        Available = true,
                        Image = "/images/8067df12-c43a-4ed6-92d1-c691d1be1954_peinture abstraite moderne bleue.jpg",
                        CreatedAt = new DateTime(2025, 5, 29, 17, 43, 43)
                    },
                    new Product
                    {
                        Id = 4,
                        UserId = 8,
                        Description = "Pot en céramique émaillée noir artisanal. Parfait pour mettre vos plantest",
                        Name = "Pot en céramique émaillée",
                        Price = 75,
                        Category = "Poterie",
                        Available = true,
                        Image = "/images/393a53fa-20ae-4ff9-a781-83407db2b29b_pot-en-ceramique-emaillee-noir-o-39-x-31-cm.jpg",
                        CreatedAt = new DateTime(2025, 5, 29, 18, 43, 43)
                    },
                    new Product
                    {
                        Id = 5,
                        UserId = 8,
                        Description = "Pot en terre cuite avec motifs",
                        Name = "Pot antique",
                        Price = 65,
                        Category = "Poterie",
                        Available = true,
                        Image = "/images/bd71d69e-9734-4425-9fea-469dc5628a6c_images (1).jpg",
                        CreatedAt = new DateTime(2025, 5, 29, 19, 43, 43)
                    },
                    new Product
                    {
                        Id = 6,
                        UserId = 8,
                        Description = "Pot de la couleur du soleil. Parfait pour apporter de la lumière dans votre maison",
                        Name = "Poterie soleil",
                        Price = 50,
                        Category = "Poterie",
                        Available = true,
                        Image = "/images/26d077dd-4efb-43e4-8741-c483e6111be6_images (2).jpg",
                        CreatedAt = new DateTime(2025, 5, 29, 20, 43, 43)
                    },
                    new Product
                    {
                        Id = 7,
                        UserId = 7,
                        Description = "Meuble de salon fait main en chêne véritable",
                        Name = "Meuble de salon",
                        Price = 1200,
                        Category = "Menuiserie",
                        Available = true,
                        Image = "/images/edbb4f5f-f889-41d8-b394-22791ea8e198_110.jpg",
                        CreatedAt = new DateTime(2025, 5, 29, 21, 43, 43)
                    },
                    new Product
                    {
                        Id = 8,
                        UserId = 7,
                        Description = "Armoire à vin en bois de cèdre assemblée à la main. Parfait pour stocker vos bouteilles.",
                        Name = "Armoire à vin",
                        Price = 800,
                        Category = "Menuiserie",
                        Available = true,
                        Image = "/images/61ddf700-3b6b-4771-bca3-ffbd415adf1a_550x366.jpg",
                        CreatedAt = new DateTime(2025, 5, 29, 22, 43, 43)
                    },
                    new Product
                    {
                        Id = 9,
                        UserId = 7,
                        Description = "Meuble TV en bois de placage de frene naturel. Teinte noisette",
                        Name = "Meuble TV",
                        Price = 900,
                        Category = "Menuiserie",
                        Available = true,
                        Image = "/images/2abf68ee-d6a6-4789-b9a7-b28d319f453e_meuble-tv-en-bois-de-placage-de-frene-naturel-en-teinte-noisette-en-plusieurs-tailles.jpg",
                        CreatedAt = new DateTime(2025, 5, 29, 23, 43, 43)
                    },
                    new Product
                    {
                        Id = 10,
                        UserId = 7,
                        Description = "Sculpture ours polaire de 1m de hauteur",
                        Name = "Sculpture ours polaire",
                        Price = 1500,
                        Category = "Sculpture",
                        Available = true,
                        Image = "/images/4011b3dc-8808-4f91-8ac7-20f1c6a9c410_images (3).jpg",
                        CreatedAt = new DateTime(2025, 5, 30, 21, 43, 43)
                    }
                );
            });
        }
    }
}
