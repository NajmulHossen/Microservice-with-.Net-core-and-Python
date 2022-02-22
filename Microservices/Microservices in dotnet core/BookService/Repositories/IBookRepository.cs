using ModelClassLibrary;
using System.Collections.Generic;

namespace BookService.Repositories
{
    public interface IBookRepository
    {
        bool SaveChanges();
        string DeleteBook(int id);
        Book GetBook(int id);
        List<Book> GetBooks();
        Book PostBook(Book book);
        Book PutBook(Book book);
    }
}