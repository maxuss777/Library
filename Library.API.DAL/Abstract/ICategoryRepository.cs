using System.Collections.Generic;
using Library.API.Common.Book;
using Library.API.Common.Category;

namespace Library.API.DAL.Abstract
{
    public interface ICategoryRepository
    {
        CategoryObject Create(CategoryObject category);
        CategoryObject Get(int categoryId);
        IEnumerable<CategoryObject> GetAll();
        IEnumerable<BookObject> GetCategoriesBooks(int categoryId);
        CategoryObject Update(CategoryObject category);
        bool Delete(int categoryId);
    }
}
