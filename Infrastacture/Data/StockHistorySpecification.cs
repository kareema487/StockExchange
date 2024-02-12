using Core.Entities;
using Core.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastacture.Data
{
    public class StockHistorySpecification : BaseSpecifications<StockHistory>
    {
        public StockHistorySpecification(string symbol) : base
                    (x => x.Symbol == symbol)
        {

        }
    }
}
