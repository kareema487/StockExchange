using API.Dtos;
using API.Dtos.Inputs;
using API.Services;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Infrastacture.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class StocksController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IStockManager _stockManager;
        public StocksController(IUnitOfWork unitOfWork
            , IMapper mapper,
IStockManager stockManager)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _stockManager = stockManager;
        }
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<StockDto>>> GetStocks()
        {
            var stocks = await _unitOfWork.Repository<Stock>().GetAllAsync();
            var stocksDto = _mapper.Map<IReadOnlyList<StockDto>>(stocks);
            return Ok(stocksDto);
        }
        [HttpGet("{symbol}/history")]
        public async Task<ActionResult<IReadOnlyList<StockDto>>> GetStockHistory(string symbol)
        {
            var stockHistorySpec = new StockHistorySpecification(symbol);
            var stocks = await _unitOfWork.Repository<StockHistory>().ListAsync(stockHistorySpec);
            var stocksDto = _mapper.Map<IReadOnlyList<StockDto>>(stocks);
            return Ok(stocksDto);
        }
        [HttpPost]
        public async Task<ActionResult<StockDto>> CreateStock(StockInputDto stockInput)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var stock = await _unitOfWork.Repository<Stock>().GetEntity(e => e.Symbol == stockInput.Symbol);
            if (stock is not null) return BadRequest("stock with the same symbol already exists");
            stock = _mapper.Map<Stock>(stockInput);
            _unitOfWork.Repository<Stock>().Add(stock);
            var stockHistory = _mapper.Map<StockHistory>(stockInput);
            _unitOfWork.Repository<StockHistory>().Add(stockHistory);
            await _unitOfWork.Complete();
            var stockDto = _mapper.Map<StockDto>(stock);
            await _stockManager.CreateStock(stockDto);
            await _stockManager.UpdateStockHistory(stockDto);
            return Ok(stockDto);
        }
        [HttpPut]
        public async Task<ActionResult<Stock>> UpdateStock(StockInputDto stockInput)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var stock = await _unitOfWork.Repository<Stock>().GetEntity(e => e.Symbol == stockInput.Symbol);
            if (stock is null) return BadRequest("No stock with the same symbol");
            stock = _mapper.Map<Stock>(stockInput);
            _unitOfWork.Repository<Stock>().Update(stock);
            var stockHistory = _mapper.Map<StockHistory>(stockInput);
            _unitOfWork.Repository<StockHistory>().Add(stockHistory);
            await _unitOfWork.Complete();
            var stockDto = _mapper.Map<StockDto>(stock);
            await _stockManager.UpdateStock(stockDto);
            await _stockManager.UpdateStockHistory(stockDto);
            return Ok(stockDto);
        }
        [HttpDelete("{symbol}")]
        public async Task<ActionResult<bool>> DeleteStock(string symbol)
        {
            var stock = await _unitOfWork.Repository<Stock>().GetEntity(e=>e.Symbol == symbol);
            if (stock == null) return BadRequest(false);
            _unitOfWork.Repository<Stock>().Delete(stock);
            await _unitOfWork.Complete();
            return Ok(true);
        }
    }
}
