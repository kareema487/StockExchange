using API.Dtos;
using API.Dtos.Inputs;
using AutoMapper;
using Core.Consts;
using Core.Entities;
using Core.Interfaces;
using Infrastacture.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OrdersController : ControllerBase
    {

        private readonly UserManager<IdentityUser> _userManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public OrdersController(IUnitOfWork unitOfWork,
                                IMapper mapper,
                                UserManager<IdentityUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userManager = userManager;
        }
        [HttpPost]
        public async Task<IActionResult> CreateOrder(OrderInputDto orderInput)
        {
            if (orderInput.OrderType != OrderTypes.Buy && orderInput.OrderType != OrderTypes.Sell) return BadRequest("order type should be Sell or Buy");
            var email = User.FindFirstValue(ClaimTypes.Email)!;
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null) return Unauthorized();
            var stock = await _unitOfWork.Repository<Stock>().GetEntity(e => e.Symbol == orderInput.Symbol);
            if (stock is null) return NotFound();
            Order order = _mapper.Map<Order>(orderInput);
            order.UserId = user.Id;
            order.Price = stock.Price;
            _unitOfWork.Repository<Order>().Add(order);
            await _unitOfWork.Complete();
            order.Stock = stock;
            var orderDto = _mapper.Map<OrderDto>(order);
            orderDto.TotalPrice = stock.Price * orderInput.Quantity;
            return Ok(orderDto);
        }
        [HttpGet]
        public async Task<IActionResult> GetOrders()
        {
            var email = User.FindFirstValue(ClaimTypes.Email)!;
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null) return Unauthorized();
            var spec = new OrderSpecification(user.Id);
            var orders = await _unitOfWork.Repository<Order>().ListAsync(spec);
            var ordersDto = _mapper.Map<List<OrderDto>>(orders);
            return Ok(ordersDto);
        }
    }
}
