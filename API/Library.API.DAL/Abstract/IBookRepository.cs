using System.Collections.Generic;
using Library.API.Common.BooksObjects;

namespace Library.API.DAL.Abstract
{
    public interface IBookRepository
    {
        Book Create(Book book);
        Book Get(int bookId);
        int IfCategoryExist(string categoryName);
        IEnumerable<Book> GetAll();
        IEnumerable<Book> GetBooksByCategoryId(int categoryId);
        Book Update(Book book);
        bool Delete(int bookId);
    }
}
