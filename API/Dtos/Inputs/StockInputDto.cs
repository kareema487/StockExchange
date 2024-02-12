using System.ComponentModel.DataAnnotations;

namespace API.Dtos.Inputs
{
    public class StockInputDto
    {
        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "The value must be greater than 0.")]
        public decimal Price { get; set; }
        [Required]
        public string Symbol { get; set; } = null!;
    }
}
