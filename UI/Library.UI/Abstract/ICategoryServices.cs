using System.Collections.Generic;
using Library.UI.Models;

namespace Library.UI.Abstract
{
    public interface ICategoryServices
    {
        IEnumerable<Category> GetAll();
        bool Create(Category category);
        bool Update(Category category);
        Category GetById(int id);
        Category GetByName(string categoryName);
        bool Delete(int categoryId);
        bool PutBookToCategory(int categoryId, int bookId);
        bool RemoveBookFromCategory(int categoryId, int bookId);
    }
}
