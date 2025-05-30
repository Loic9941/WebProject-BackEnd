using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        public required string Email { get; set; }
        public required string Salt { get; set; }

        public required string PasswordHash { get; set; }

        public required string Role { get; set; } = "Customer";

        [Required]
        public required string Firstname { get; set; }

        [Required]
        public required string Lastname { get; set; }

        //this one are the invoice linked to the customer
        public List<Invoice> Invoices { get; set; } = [];

        //this one are the invoice Items linked to the artist
        public List<InvoiceItem> InvoiceItems { get; set; } = [];

        //this one are the products linked to the artist
        public List<Product> Products { get; set; } = [];
        public bool IsBlocked { get; set; } = false;
    }
}
