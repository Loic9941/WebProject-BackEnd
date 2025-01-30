

using System.ComponentModel.DataAnnotations;

namespace DAL.Models
{
    public class Contact
    {
        [Key]
        public int Id { get; set; }

        public string Firstname { get; set; }

        public string Lastname { get; set; }

        public List<Rating> Ratings { get; set; }

        public List<Product> Products { get; set; }
    }
}
