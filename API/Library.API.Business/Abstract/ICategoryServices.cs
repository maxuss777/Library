using System;
using System.Collections.Generic;
using Library.API.Common.Book;
using Library.API.Common.Category;

namespace Library.API.Business.Abstract
{
    public interface ICategoryServices
    {
        Category CreateCategory(Category category);
        Category GetCategoryById(int categoryId);
        Category GetCategoryByName(string categoryName);
        IEnumerable<Category> GetAllCategories();
        Category UpdateCategory(Category category);
        IEnumerable<Book> GetCategoriesBooks(int categoryId);
        Boolean DeleteCategory(int categoryId);
        Boolean PutBookToCategory(int categoryId, int bookId);
        Boolean RemoveBookFromCategory(int categoryId, int bookId);
    }
}
