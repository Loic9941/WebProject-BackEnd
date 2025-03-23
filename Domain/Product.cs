using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        public required string Name { get; set; }
        public string? Description { get; set; }

        public byte[] Image { get; set; } = [];

        [Column(TypeName = "money")]
        public decimal Price { get; set; }

        [Required]
        public int UserId { get; set; }
        public virtual User? User { get; set; }
        public virtual List<Rating>? Ratings { get; set; } = [];

        public virtual List<InvoiceItem>? InvoiceItems { get; set; } = [];

    }
}
