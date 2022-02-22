using Microsoft.EntityFrameworkCore;
using ModelClassLibrary;

namespace OrderService.Data
{
    public class OrderDbContext : DbContext
    {
        public OrderDbContext(DbContextOptions<OrderDbContext> options) : base(options) { }

        public DbSet<Order> Orders { get; set; }
        public DbSet<MovieOrder> MovieOrders { get; set; }
    }
}
