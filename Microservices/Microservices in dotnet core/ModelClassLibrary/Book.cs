using System;
using System.Collections.Generic;

namespace ModelClassLibrary
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty ;
        public string Description { get; set; }= string.Empty ;
        public uint Quantity { get; set; } = 0;
        public int AuthorId { get; set; }
        public Author Author { get; set; }
    }

    public class Books
    {
        public List<Book> BookList { get; set; }
    }
}
