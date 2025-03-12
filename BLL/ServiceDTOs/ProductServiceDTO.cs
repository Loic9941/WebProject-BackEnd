namespace BLL.ServiceDTOs
{
    public class ProductServiceDTO
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }

    }
}
