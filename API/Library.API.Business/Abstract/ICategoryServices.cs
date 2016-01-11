using System;
using System.Collections.Generic;
using Library.API.Common.CategoriesObjects;

namespace Library.API.Business.Abstract
{
    public interface ICategoryServices
    {
        Category CreateCategory(Category category);
        Category GetCategoryById(int categoryId);
        Category GetCategoryByName(string categoryName);
        IEnumerable<Category> GetAllCategories();
        Category UpdateCategory(Category category);
        Boolean DeleteCategory(int categoryId);
        Boolean PutBookToCategory(int categoryId, int bookId);
        Boolean RemoveBookFromCategory(int categoryId, int bookId);
    }
}
