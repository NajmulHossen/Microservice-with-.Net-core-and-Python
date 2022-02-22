using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelClassLibrary;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OrderService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IRequestClient<Book> _client;
        private readonly IRequestClient<Books> _clientBookList;

        public BookController(
            IRequestClient<Book> client,
            IRequestClient<Books> clientBookList)
        {
            _client = client;
            _clientBookList = clientBookList;
        }
        [HttpGet("GetBook/{id}")]
        public async Task<ActionResult> Get(string id)
        {
            var response = await _client.GetResponse<Book>(new { Id = id });
            return Ok(response.Message);
        }

        [HttpGet("GetBooks")]
        public async Task<List<Book>> GetBooks()
        {
            var response = await _clientBookList.GetResponse<Books>(new Books());
            return await Task.FromResult(response.Message.BookList);
        }
        
    }
}
