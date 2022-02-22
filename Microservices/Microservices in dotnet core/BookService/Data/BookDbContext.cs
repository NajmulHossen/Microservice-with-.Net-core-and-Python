using Microsoft.EntityFrameworkCore;
using ModelClassLibrary;

namespace BookService.Data
{
    public class BookDbContext : DbContext
    {
        public BookDbContext(DbContextOptions<BookDbContext> options) : base(options) { }

        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<AcceptedOrder> AcceptedOrders { get; set; }
    }
}
