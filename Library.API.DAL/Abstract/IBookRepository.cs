using System.Collections.Generic;
using Library.API.Common.Book;
using Library.API.Common.Category;

namespace Library.API.DAL.Abstract
{
    public interface IBookRepository
    {
        BookObject Create(BookObject book);
        BookObject Get(int bookId);
        IEnumerable<BookObject> GetAll();
        IEnumerable<CategoryObject> GetBooksCategories(int bookId);
        BookObject Update(BookObject book);
        bool Delete(int bookId);
        bool PutBookToCategory(int bookId);
    }
}
