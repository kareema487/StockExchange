using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Stock:BaseEntity
    {
        [Range(0.01, double.MaxValue, ErrorMessage = "The value must be greater than 0.")]
        public decimal Price { get; set; }
        public List<StockHistory> StockHistory { get; set; } = [];
        public List<Order> StockOrders { get; set; } = [];
    }
}
