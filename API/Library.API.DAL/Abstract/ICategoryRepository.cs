using System.Collections.Generic;
using Library.API.Common.Book;
using Library.API.Common.Category;

namespace Library.API.DAL.Abstract
{
    public interface ICategoryRepository
    {
        CategoryInfo Create(CategoryInfo category);
        CategoryObject Get(int categoryId);
        IEnumerable<CategoryObject> GetAll();
        IEnumerable<BookInfo> GetCategoriesBooks(int categoryId);
        CategoryObject Update(CategoryObject category);
        bool Delete(int categoryId);
        bool PutBookToCategory(int categoryId, int bookId);
        bool RemoveBookFromCategory(int categoryId, int bookId);
    }
}
