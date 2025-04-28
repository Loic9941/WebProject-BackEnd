using Microsoft.AspNetCore.Mvc;

namespace BLL.DTOs.InputDTOs
{
    public class ProductFiltersDTO
    {
        [FromQuery]
        public string? Search { get; set; }

        [FromQuery]
        public string? Category { get; set; }

        [FromQuery]
        public decimal? MinPrice { get; set; }

        [FromQuery]
        public decimal? MaxPrice { get; set; }

        [FromQuery]
        public string? OrderBy { get; set; }
    }
}
