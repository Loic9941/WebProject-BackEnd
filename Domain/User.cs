using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class User
    {

        [Key]
        public int Id { get; set; }

        public required string Email { get; set; }
        public required string Salt { get; set; }

        public required string PasswordHash { get; set; }

        public required string Role { get; set; } = "User";

        public virtual Contact? contact { get; set; } = null!;
        public int? ContactId { get; set; }

    }
}
