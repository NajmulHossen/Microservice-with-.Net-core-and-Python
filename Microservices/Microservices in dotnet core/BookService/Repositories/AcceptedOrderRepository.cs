using BookService.Data;
using ModelClassLibrary;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BookService.Repositories
{
    public class AcceptedOrderRepository : IAcceptedOrderRepository
    {
        private readonly BookDbContext _context;

        public AcceptedOrderRepository(BookDbContext Context)
        {
            _context = Context;
        }
        public bool AcceptedOrderExits(int id)
        {
            return _context.AcceptedOrders.Any(e => e.Id == id);
        }

        public void CreateAcceptedOrder(AcceptedOrder ActdOrder)
        {
            if (ActdOrder == null)
            {
                throw new ArgumentNullException(nameof(ActdOrder));
            }
            _context.AcceptedOrders.Add(ActdOrder);
        }

        public string DeleteAcceptedOrder(int id)
        {
            var acceptedOrder = _context.AcceptedOrders.Find(id);
            if (acceptedOrder != null)
            {
                _context.AcceptedOrders.Remove(acceptedOrder);
                _context.SaveChanges();
            }
            return "" + acceptedOrder.OrderName + " Deleted Successfully";
        }

        public AcceptedOrder GetAcceptedOrderById(int id)
        {
            return _context.AcceptedOrders.FirstOrDefault(p => p.Id == id);
        }

        public IEnumerable<AcceptedOrder> GetAllAcceptedOrder()
        {
            return _context.AcceptedOrders.ToList();
        }

        public AcceptedOrder PostAcceptedOrder(AcceptedOrder ActdOrder)
        {
            _context.AcceptedOrders.Add(ActdOrder);
            _context.SaveChanges();
            return ActdOrder;
        }

        public AcceptedOrder PutAcceptedOrder(AcceptedOrder ActdOrder)
        {
            _context.Entry(ActdOrder).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
            return ActdOrder;
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }
    }
}
