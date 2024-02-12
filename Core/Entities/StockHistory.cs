using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class StockHistory:BaseEntity
    {
        public DateTime Date { get; set; } = DateTime.UtcNow;
        public Stock? Stock { get; set; }
        public decimal Price { get; set; }
    }
}
