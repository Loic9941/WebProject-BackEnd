using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class Rating
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public required int ProductId { get; set; }
        public virtual Product Product { get; set; } = null!;

        public int Stars { get; set; }

        [Required]
        public required int ContactId { get; set; }
        public virtual Contact Contact { get; set; } = null!;
    }
}
