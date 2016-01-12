namespace Library.API.Business.Interfaces
{
    using System;
    using System.Collections.Generic;
    using Library.API.Model;

    public interface ICategoryService
    {
        
        Category CreateCategory(Category category);
        
        Category GetCategoryById(int categoryId);
        
        Category GetCategoryByName(string categoryName);
        
        List<Category> GetAllCategories();
        
        Category UpdateCategory(Category category);
        
        Boolean DeleteCategory(int categoryId);
        
        Boolean PutBookToCategory(int categoryId, int bookId);
        
        Boolean RemoveBookFromCategory(int categoryId, int bookId);
    }
}
