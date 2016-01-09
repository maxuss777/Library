using System.Collections.Generic;
using Library.API.Common.Book;
using Library.API.Common.Category;

namespace Library.API.DAL.Abstract
{
    public interface ICategoryRepository
    {
        Category Create(Category category);
        Category Get(int categoryId);
        IEnumerable<Category> GetAll();
        IEnumerable<Book> GetCategoriesBooks(int categoryId);
        Category Update(Category category);
        bool Delete(int categoryId);
        bool PutBookToCategory(int categoryId, int bookId);
        bool RemoveBookFromCategory(int categoryId, int bookId);
    }
}
