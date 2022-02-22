using BookService.Repositories;
using MassTransit;
using ModelClassLibrary;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookService.Consumers
{
    public class OrderConsumer : IConsumer<Book>, IConsumer<Books>,IConsumer<Order>
    {
        private readonly IBookRepository _bookRepository;
        private readonly IAcceptedOrderRepository _acceptedOrderRepository;

        public OrderConsumer(IBookRepository bookRepository,
            IAcceptedOrderRepository acceptedOrderRepository)
        {
            _bookRepository = bookRepository;
            _acceptedOrderRepository = acceptedOrderRepository;
        }
        public async Task Consume(ConsumeContext<Book> context)
        {
            var book = context.Message;

            List<Book> books = _bookRepository.GetBooks();
            var msg = books.Find(c => c.Id == book.Id);

            await context.RespondAsync(msg);
        }

        public async Task Consume(ConsumeContext<Books> context)
        {
            Books msg = new();
            msg.BookList = _bookRepository.GetBooks();
            await context.RespondAsync(msg);
        }

        public Task Consume(ConsumeContext<Order> context)
        {
            var order = context.Message;
            
            var msg = _bookRepository.GetBook(order.ProductId);
            if(msg.Quantity>0 && order.Quantity>0 && msg.Quantity >= order.Quantity)
            {
                msg.Quantity -= order.Quantity;
                _bookRepository.PutBook(msg);
                order.State = OrderState.Accepted;
                AcceptedOrder acceptedOrder = new();
                acceptedOrder.Id = order.Id;
                acceptedOrder.OrderName = order.OrderName;
                acceptedOrder.Quantity = order.Quantity;
                acceptedOrder.ProductId = order.ProductId;
                acceptedOrder.ProductName = msg.Title;
                _acceptedOrderRepository.PostAcceptedOrder(acceptedOrder);
                return context.RespondAsync(order);
            }
            else
            {
                order.State = OrderState.Unaccepted;
                return context.RespondAsync(order);
            }
            
            
        }

        //private static List<Book> GetBooks()
        //{
        //    List<Book> books = new()
        //    {
        //        new Book() { Id = 1, Title = "Title1", Category = "Category", AuthorId = 1 },
        //        new Book() { Id = 2, Title = "Title2", Category = "Category", AuthorId = 1 },
        //        new Book() { Id = 3, Title = "Title3", Category = "Category", AuthorId = 1 },
        //        new Book() { Id = 4, Title = "Title4", Category = "Category", AuthorId = 1 },
        //        new Book() { Id = 5, Title = "Title5", Category = "Category", AuthorId = 1 },
        //        new Book() { Id = 6, Title = "Title6", Category = "Category", AuthorId = 1 },
        //    };
        //    return books;
        //}
    }
}
