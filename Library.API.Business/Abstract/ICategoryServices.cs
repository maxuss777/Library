using System;
using System.Collections.Generic;
using Library.API.Common.Book;
using Library.API.Common.Category;

namespace Library.API.Business.Abstract
{
    public interface ICategoryServices
    {
        CategoryInfo CreateCategory(CategoryInfo category);
        CategoryObject GetCategoryById(int categoryId);
        IEnumerable<CategoryObject> GetAllCategories();
        CategoryObject UpdateCategory(CategoryObject category);
        IEnumerable<BookInfo> GetCategoriesBooks(int categoryId);
        Boolean DeleteCategory(int categoryId);
        Boolean PutBookToCategory(int categoryId, int bookId);
        Boolean RemoveBookFromCategory(int categoryId, int bookId);
    }
}
