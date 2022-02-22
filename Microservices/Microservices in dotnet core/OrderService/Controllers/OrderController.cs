using Automatonymous;using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelClassLibrary;
using OrderService.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OrderService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IRequestClient<Order> _clientOrder;
        private readonly IOrderRepository _repository;

        public OrderController(
            IRequestClient<Order> clientOrder,
            IOrderRepository repository
            )
        {
            _clientOrder = clientOrder;
            _repository = repository;
        }

        [HttpPost("PostBookOrder")]
        public async Task<ActionResult<Order>> PostBookOrder(Order order)
        {
            try{
                order.State = OrderState.Submitted;
                var response = await _clientOrder.GetResponse<Order>(order);
                return await Task.FromResult(_repository.PostOrder(response.Message));
            }
            catch
            {
                order.State = OrderState.UnSubmitted;
                _repository.PostOrder(order);
                return BadRequest(order);
            }
            
        }
        [HttpPut("PutBookOrder")]
        public async Task<ActionResult<Order>> PutBookOrder(Order order)
        {
            try
            {
                order.State = OrderState.Submitted;
                var response = await _clientOrder.GetResponse<Order>(order);
                return await Task.FromResult(_repository.PutOrder(response.Message));
            }
            catch
            {
                order.State = OrderState.UnSubmitted;
                _repository.PutOrder(order);
                return BadRequest(order);
            }
        }

    }
    
}
