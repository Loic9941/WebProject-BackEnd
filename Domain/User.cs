using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class User
    {

        public User(string username, string passwordHash, string salt)
        {
            Username = username;
            this.passwordHash = passwordHash;
            Salt = salt;
        }
        [Key]
        public int Id { get; set; }

        public string Username { get; set; }
        public string Salt { get; set; }

        public string passwordHash { get; set; }

        public virtual Contact? contact { get; set; } = null!;
        public int? ContactId { get; set; }


    }
}
