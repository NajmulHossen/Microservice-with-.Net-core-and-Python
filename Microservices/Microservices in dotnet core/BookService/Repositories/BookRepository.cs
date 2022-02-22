using BookService.Data;
using Microsoft.EntityFrameworkCore;
using ModelClassLibrary;
using System.Collections.Generic;
using System.Linq;

namespace BookService.Repositories
{
    public class BookRepository : IBookRepository
    {
        public readonly BookDbContext _dbContext;
        public BookRepository(BookDbContext BookDbContext)
        {
            _dbContext = BookDbContext;
        }
        
        public List<Book> GetBooks()
        {
            var books = _dbContext.Books.Include(c => c.Author).ToList();
            return books;
        }

        public Book GetBook(int id)
        {
            return _dbContext.Books.SingleOrDefault(c => c.Id == id);
        }

        public Book PostBook(Book book)
        {
            _dbContext.Books.Add(book);
            _dbContext.SaveChanges();
            return book;
        }

        public Book PutBook(Book book)
        {
            _dbContext.Entry(book).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _dbContext.SaveChanges();
            return book;
        }

        public string DeleteBook(int id)
        {
            var book = _dbContext.Books.Find(id);
            if (book != null)
            {
                _dbContext.Books.Remove(book);
                _dbContext.SaveChanges();
            }
            return "" + book.Title + " Deleted Successfully";
        }

        public bool SaveChanges()
        {
            return (_dbContext.SaveChanges() >= 0);
        }
    }
}
