using Core.Entities;
using Core.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastacture.Data
{
    public class OrderSpecification : BaseSpecifications<Order>
    {
        public OrderSpecification(string userId) : base
                    (x => x.UserId == userId)
        {
            //AddInclude(x => x.Stock);
        }
    }
}
