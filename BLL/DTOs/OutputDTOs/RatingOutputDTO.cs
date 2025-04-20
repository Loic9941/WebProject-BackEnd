using Domain;
namespace BLL.DTOs.OutputDTOs
{
    public class RatingOutputDTO
    {
        public required int Id { get; set; }

        public required int Rate { get; set; }

        public string? Text { get; set; }

        public required string FirstName { get; set; }

        public required string LastName { get; set; }

        public required string ProductName { get; set; }

        public required DateTime CreatedAt { get; set; }

        public CommentOutputDTO? Comment { get; set; } = null!;
    }
}
