using System;
using System.Collections.Generic;
using Library.API.Common.Book;
using Library.API.Common.Category;

namespace Library.API.Business.Abstract
{
    public interface IBookServices
    {
        BookInfo CreateBook(BookInfo book);
        BookObject GetBookById(int bookId);
        IEnumerable<BookObject> GetAllBooks();
        BookObject UpdateBook(BookObject book);
        IEnumerable<CategoryInfo> GetBooksCategories(int bookId);
        Boolean DeleteBook(int bookId);
    }
}
