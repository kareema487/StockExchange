using System.ComponentModel.DataAnnotations;

namespace API.Dtos
{
    public class StockDto
    {
        public decimal Price { get; set; }
        public string Symbol { get; set; } = null!;
        public DateTime Date { get; set; } = DateTime.UtcNow;
    }
}
