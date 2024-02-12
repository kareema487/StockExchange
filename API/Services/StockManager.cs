using API.Dtos;
using API.Hubs;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace API.Services
{
    public class StockManager:IStockManager
    {
        public IHubContext<StockHub> _hubContext;
        public StockManager(IHubContext<StockHub> hubContext)
        {
            _hubContext = hubContext;
        }
        public Task CreateStock(StockDto stock)
        {
            return _hubContext.Clients.All.SendAsync("CreateStock", stock);
        }
        public Task UpdateStock(StockDto stock)
        {
            return _hubContext.Clients.All.SendAsync("UpdateStock", stock);
        }
        public Task UpdateStockHistory(StockDto stock)
        {
            return _hubContext.Clients.Group(stock.Symbol).SendAsync("UpdateStockHistory", stock);
        }
    }
}
