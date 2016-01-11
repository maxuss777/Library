using System;
using System.Collections.Generic;
using Library.API.Common.BooksObjects;

namespace Library.API.Business.Abstract
{
    public interface IBookServices
    {
        Book CreateBook(Book book);
        Book GetBookById(int bookId);
        IEnumerable<Book> GetAllBooks();
        Book UpdateBook(Book book);
        IEnumerable<Book> GetBooksByCategoryName(string categoryName);
        Boolean DeleteBook(int bookId);
    }
}
