namespace BLL.DTOs
{
    public class RegisterDTO
    {
        public required string Password { get; set; }
        public required string Email { get; set; }
        public required string Role { get; set; }
        public required string Firstname { get; set; }
        public required string Lastname { get; set; }
    }
}
