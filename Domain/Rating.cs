using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class Rating
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public required int ProductId { get; set; }
        public virtual Product? Product { get; set; } = null!;

        public int Rate { get; set; }

        public string? Comment { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Required]
        public required int UserId { get; set; }
        public virtual User? User { get; set; } = null!;
    }
}
