using ModelClassLibrary;
using System.Collections.Generic;

namespace BookService.Repositories
{
    public interface IAcceptedOrderRepository
    {
        bool SaveChanges();
        IEnumerable<AcceptedOrder> GetAllAcceptedOrder();
        AcceptedOrder GetAcceptedOrderById(int id);
        void CreateAcceptedOrder(AcceptedOrder ActdOrder);
        bool AcceptedOrderExits(int id);
        AcceptedOrder PostAcceptedOrder(AcceptedOrder ActdOrder);
        AcceptedOrder PutAcceptedOrder(AcceptedOrder ActdOrder);
        string DeleteAcceptedOrder(int id);
    }
}
