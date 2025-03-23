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

        public List<Rating> Ratings { get; set; } = [];

        public List<Product> Products { get; set; } = [];

        public List<Invoice> Invoices { get; set; } = [];
    }
}
