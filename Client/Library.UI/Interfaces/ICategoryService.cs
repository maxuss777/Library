using System.Collections.Generic;
using Library.UI.Models.Categories;

namespace Library.UI.Interfaces
{
    public interface ICategoryService
    {
        List<Category> GetAll();
        bool Create(Category category);
        bool Update(Category category);
        Category GetById(int id);
        Category GetByName(string categoryName);
        bool Delete(int categoryId);
        bool PutBookToCategory(int categoryId, int bookId);
        bool RemoveBookFromCategory(int categoryId, int bookId);
    }
}