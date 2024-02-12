using API.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Services
{
    public interface IStockManager
    {
        Task CreateStock(StockDto stock);
        Task UpdateStock(StockDto stock);
        Task UpdateStockHistory(StockDto stock);
    }
}
