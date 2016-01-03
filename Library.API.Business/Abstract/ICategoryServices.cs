using System;
using System.Collections.Generic;
using Library.API.Common.Book;
using Library.API.Common.Category;

namespace Library.API.Business.Abstract
{
    public interface ICategoryServices
    {
        CategoryObject CreateCategory(CategoryObject category);
        CategoryObject GetCategoryById(int categoryId);
        IEnumerable<CategoryObject> GetAllCategories();
        CategoryObject UpdateCategory(CategoryObject category);
        IEnumerable<BookObject> GetCategoriesBooks(int categoryId);
        Boolean DeleteCategory(int categoryId);
    }
}
