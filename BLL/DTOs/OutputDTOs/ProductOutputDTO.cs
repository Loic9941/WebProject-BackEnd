namespace BLL.DTOs.OutputDTOs
{
    public class ProductOutputDTO
    {
        public required int Id { get; set; }

        public required string Name { get; set; }
        public required string? Description { get; set; }

        public required decimal Price { get; set; }

        public required int? ArtisteId;

        public required string? ArtistFullName;

        public required string Category { get; set; }

        public DateTime CreatedAt { get; set; }

        public required bool Available { get; set; }

        public string? Image { get; set; }
    }
}
