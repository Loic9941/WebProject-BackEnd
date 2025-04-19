using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class Rating
    {
        [Key]
        public int Id { get; set; }

        public required int InvoiceItemId { get; set; } 
        public virtual InvoiceItem InvoiceItem { get; set; } = null!;

        public int Rate { get; set; }

        public string? Text { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public Comment? Comment { get; set; } = null!;
    }
}
