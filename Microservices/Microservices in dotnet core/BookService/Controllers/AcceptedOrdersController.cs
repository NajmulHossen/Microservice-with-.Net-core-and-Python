using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookService.Data;
using ModelClassLibrary;
using BookService.Repositories;

namespace BookService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AcceptedOrdersController : ControllerBase
    {
        private readonly IAcceptedOrderRepository _context;

        public AcceptedOrdersController(IAcceptedOrderRepository context)
        {
            _context = context;
        }

        // GET: api/AcceptedOrders
        [HttpGet]
        public ActionResult<IEnumerable<AcceptedOrder>> GetAcceptedOrders()
        {
            var AllOrder = _context.GetAllAcceptedOrder();
            return Ok(AllOrder);
        }

        // GET: api/AcceptedOrders/5
        [HttpGet("{id}",Name = "GetAcceptedOrder")]
        public ActionResult<AcceptedOrder> GetAcceptedOrder(int id)
        {
            var acceptedOrder =  _context.GetAcceptedOrderById(id);

            if (acceptedOrder == null)
            {
                return NotFound();
            }

            return acceptedOrder;
        }

        // PUT: api/AcceptedOrders/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public IActionResult PutAcceptedOrder( AcceptedOrder acceptedOrder)
        {

            try
            {
                if (!_context.AcceptedOrderExits(acceptedOrder.Id))
                {
                    return NotFound();
                }
                else
                {
                    var Order = _context.PutAcceptedOrder(acceptedOrder);
                    return Ok(Order);
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine("Message: {0}",ex.Message);
            }

            return NoContent();
        }

        // POST: api/AcceptedOrders
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public ActionResult<AcceptedOrder> PostAcceptedOrder(AcceptedOrder acceptedOrder)
        {
            _context.PostAcceptedOrder(acceptedOrder);
            _context.SaveChanges();

            return CreatedAtRoute(nameof(GetAcceptedOrder), new { Id = acceptedOrder.Id }, acceptedOrder);
        }

        // DELETE: api/AcceptedOrders/5
        [HttpDelete("{id}")]
        public ActionResult DeleteAcceptedOrder(int id)
        {

            try
            {
                if (!_context.AcceptedOrderExits(id))
                {
                    return NotFound();
                }
                else
                {
                    var Order = _context.DeleteAcceptedOrder(id);
                    return Ok(Order);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Message: {0}", ex.Message);
            }
            
            return NoContent();
        }

    }
}
