using System.ComponentModel.DataAnnotations;

namespace API.Dtos.Inputs
{
    public class OrderInputDto
    {
        [Required]
        public string Symbol { get; set; } = null!;
        [Required]
        public int Quantity { get; set; }
        [Required]
        public string OrderType { get; set; } = null!;
    }
}
