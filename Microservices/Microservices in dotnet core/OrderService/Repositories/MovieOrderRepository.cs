using ModelClassLibrary;
using OrderService.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OrderService.Repositories
{
    public class MovieOrderRepository : IMovieOrderRepository
    {
        private readonly OrderDbContext _dbContext;

        public MovieOrderRepository(OrderDbContext OrderDbContext)
        {
            _dbContext = OrderDbContext;
        }
        public string DeleteMovieOrder(int id)
        {
            var movieorder = _dbContext.MovieOrders.Find(id);
            if (movieorder != null)
            {
                _dbContext.MovieOrders.Remove(movieorder);
                _dbContext.SaveChanges();
            }
            return "" + movieorder.OrderName + " Deleted Successfully";
        }

        public MovieOrder GetMovieOrder(int id)
        {
            try
            {
                return _dbContext.MovieOrders.SingleOrDefault(c => c.Id == id);
            }
            catch (Exception)
            {
                throw new System.NotImplementedException();
            }
        }

        public List<MovieOrder> GetMovieOrders()
        {
            try
            {
                var movieorders = _dbContext.MovieOrders.ToList();
                return movieorders;
            }
            catch (Exception)
            {
                throw new System.NotImplementedException();
            }
        }

        public MovieOrder PostMovieOrder(MovieOrder movieorder)
        {

            _dbContext.MovieOrders.Add(movieorder);
            _dbContext.SaveChanges();
            return movieorder;
        }

        public MovieOrder PutMovieOrder(MovieOrder movieorder)
        {

            _dbContext.Entry(movieorder).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _dbContext.SaveChanges();
            return movieorder;
        }
    }
}
