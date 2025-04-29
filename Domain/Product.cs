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

        [Column(TypeName = "money")]
        public decimal Price { get; set; }

        [Required]
        public required int UserId { get; set; }
        public virtual User? User { get; set; }

        public virtual List<InvoiceItem> InvoiceItems { get; set; } = [];

        public required string Category { get; set; } = null!;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public Boolean Available { get; set; } = true;

        public string? Image { get; set; }
    }
}
