using ModelClassLibrary;
using System.Collections.Generic;

namespace OrderService.Repositories
{
    public interface IOrderRepository
    {
        string DeleteOrder(int id);
        Order GetOrder(int id);
        List<Order> GetOrders();
        Order PostOrder(Order order);
        Order PutOrder(Order order);
    }
}