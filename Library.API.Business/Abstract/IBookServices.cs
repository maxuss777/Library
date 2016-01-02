using System;
using System.Collections.Generic;
using Library.API.Common.Book;
using Library.API.Common.Category;

namespace Library.API.Business.Abstract
{
    public interface IBookServices
    {
        BookObject CreateBook(BookObject book);
        BookObject GetBookById(int bookId);
        IEnumerable<BookObject> GetAllBooks();
        BookObject UpdateBook(BookObject book);
        IEnumerable<CategoryObject> GetBooksCategories(int bookId);
        Boolean DeleteBook(int bookId);
        Boolean PutBookToCategory(int bookId);
    }
}
