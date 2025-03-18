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
        public int ContactId { get; set; }
        public virtual Contact Contact{ get; set; } = null!;
        public List<Rating> Ratings { get; set; } = [];

    }
}
