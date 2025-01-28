using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }

        [Column(TypeName = "money")]
        public decimal Price { get; set; }

        public int ContactId { get; set; }
        public virtual Contact Contact{ get; set; }
        public List<Rating> Ratings { get; set; }

    }
}
