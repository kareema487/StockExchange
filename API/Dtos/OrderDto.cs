using System.ComponentModel.DataAnnotations;

namespace API.Dtos
{
    public class OrderDto
    {
        public decimal Price { get; set; }
        public string UserEmail { get; set; } = null!;
        public string Symbol { get; set; } = null!;
        public int Quantity { get; set; }
        public string OrderType { get; set; } = null!;
        public decimal TotalPrice { get; set; }
        public DateTime OrderDate { get; set; }
    }
}
