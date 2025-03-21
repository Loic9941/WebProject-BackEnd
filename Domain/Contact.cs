using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class Contact
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public required string Firstname { get; set; }

        [Required]
        public required string Lastname { get; set; }

        public List<Rating> Ratings { get; set; } = [];

        public List<Product> Products { get; set; } = [];

        public List<Invoice> Invoices { get; set; } = [];

        public virtual User? user { get; set; }
    }
}
