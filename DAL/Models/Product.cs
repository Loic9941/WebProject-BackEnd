using System.ComponentModel.DataAnnotations.Schema;


namespace DAL.Models
{
    public class Product
    {
        int Id { get; set; }

        string Name { get; set; }
        public string Description { get; set; }

        [Column(TypeName = "money")]
        public decimal Price { get; set; }

        public int AuthorId { get; set; }
        public virtual Contact Author { get; set; }
    }
}
