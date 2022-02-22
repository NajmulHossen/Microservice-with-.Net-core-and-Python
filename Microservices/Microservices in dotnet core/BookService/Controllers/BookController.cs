using BookService.Data;
using BookService.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ModelClassLibrary;
using System.Collections.Generic;
using System.Linq;

namespace BookService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        public readonly IBookRepository _repository;
        public BookController(IBookRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("GetBooks")]
        public List<Book> GetBooks()
        {
            return _repository.GetBooks();
        }


        [HttpGet("GetBook/{id}")]
        public Book GetBook(int id)
        {
            return _repository.GetBook(id);
        }


        [HttpPost("PostBook")]
        public Book PostBook(Book book)
        {
            return _repository.PostBook(book);
        }

        [HttpPut("PutBook")]
        public Book PutBook(Book book)
        {
            return _repository.PutBook(book);
        }

        [HttpDelete("DeleteBook")]
        public string DeleteBook(int id)
        {
            return _repository.DeleteBook(id);
        }
    }
}
