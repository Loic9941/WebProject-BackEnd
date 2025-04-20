using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class Comment
    {
        [Key]
        public int Id { get; set; }
        public required int RatingId { get; set; } 
        public virtual Rating Rating { get; set; } = null!;
        public required string Text { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
