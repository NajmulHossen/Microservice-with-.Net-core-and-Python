using ModelClassLibrary;
using System.Collections.Generic;

namespace OrderService.Repositories
{
    public interface IMovieOrderRepository
    {
        List<MovieOrder> GetMovieOrders();
        MovieOrder GetMovieOrder(int id);
        MovieOrder PostMovieOrder(MovieOrder movieorder);
        MovieOrder PutMovieOrder(MovieOrder movieorder);
        string DeleteMovieOrder(int id);
    }
}
