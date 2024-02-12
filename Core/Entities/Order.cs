using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Order:BaseEntity
    {
        [Required]
        public string UserId { get; set; } 
        public IdentityUser? User { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public Stock? Stock { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.UtcNow;
        public string OrderType { get; set; } = null!;
    }
}
