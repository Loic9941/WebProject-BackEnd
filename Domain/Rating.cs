using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    public class Rating
    {
        [Key]
        public int Id { get; set; }

        public int? InvoiceItemId { get; set; } // Optional foreign key property
        public virtual InvoiceItem? InvoiceItem { get; set; } = null!;

        public int Rate { get; set; }

        public string? Comment { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Required]
        public required int UserId { get; set; }
        public virtual User? User { get; set; } = null!;
    }
}
