namespace BLL.DTOs.OutputDTOs
{
    public class UserOutputDTO
    {
        public required int Id { get; set; }

        public required string Email { get; set; }

        public required string Role { get; set; }

        public required string Firstname { get; set; }

        public required string Lastname { get; set; }
    }
}
