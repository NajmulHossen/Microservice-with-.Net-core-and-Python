using ModelClassLibrary;
using OrderService.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OrderService.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly OrderDbContext _dbContext;
        

        public OrderRepository(OrderDbContext OrderDbContext
            )
        {
            _dbContext = OrderDbContext;
            
        }
        public string DeleteOrder(int id)
        {
            var order = _dbContext.Orders.Find(id);
            if (order != null)
            {
                _dbContext.Orders.Remove(order);
                _dbContext.SaveChanges();
            }
            return "" + order.OrderName + " Deleted Successfully";
        }

        public Order GetOrder(int id)
        {
            try
            {
                return _dbContext.Orders.SingleOrDefault(c => c.Id == id);
            }
            catch(Exception)
            {
                throw new System.NotImplementedException();
            }
        }

        public List<Order> GetOrders()
        {
            try
            {
                var orders = _dbContext.Orders.ToList();
                return orders;
            }
            catch (Exception)
            {
                throw new System.NotImplementedException();
            }
        }

        public Order PostOrder(Order order)
        {

            _dbContext.Orders.Add(order);
            _dbContext.SaveChanges();
            return order;
        }

        public Order PutOrder(Order order)
        {
            
            _dbContext.Entry(order).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _dbContext.SaveChanges();
            return order;
        }
    }
}
