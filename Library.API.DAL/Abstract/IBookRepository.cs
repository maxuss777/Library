using System.Collections.Generic;
using Library.API.Common.Book;
using Library.API.Common.Category;

namespace Library.API.DAL.Abstract
{
    public interface IBookRepository
    {
        BookInfo Create(BookInfo book);
        BookObject Get(int bookId);
        IEnumerable<BookObject> GetAll();
        IEnumerable<CategoryInfo> GetBooksCategories(int bookId);
        BookObject Update(BookObject book);
        bool Delete(int bookId);
        
    }
}
