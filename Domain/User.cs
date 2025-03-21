namespace Domain
{
    public class User
    {
        private string passwordHash;

        public User(string username, string passwordHash, string salt)
        {
            Username = username;
            this.passwordHash = passwordHash;
            Salt = salt;
        }

        public string Username { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }

        public virtual Contact? contact { get; set; } = null!;
        public int? UserId { get; set; }


    }
}
