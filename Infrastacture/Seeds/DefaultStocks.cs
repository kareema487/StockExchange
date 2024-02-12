using Core.Consts;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Infrastacture.Data;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastacture.Seeds
{
    public class DefaultStocks
    {
        public static async Task SeedStocksAsync(IUnitOfWork unitOfWork)
        {
            List<Stock> stocks = new List<Stock>()
            {
                new()
                {
                    Price=10,
                    Symbol="AAPL"
                },
                new()
                {
                    Price=11,
                    Symbol="GOOGL"
                },new()
                {
                    Price=12,
                    Symbol="AMZN"
                },new()
                {
                    Price=13,
                    Symbol="TSLA"
                }
            };
            var spec = new BaseSpecifications<Stock>();
            var count = await unitOfWork.Repository<Stock>().CountAsync(spec);
            if (count ==0)
            {
                foreach(var stock in stocks)
                {
                    unitOfWork.Repository<Stock>().Add(stock);
                }
                await unitOfWork.Complete();
            }
        }
    }
}
