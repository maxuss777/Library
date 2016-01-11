using System.Collections.Generic;
using Library.UI.Models;

namespace Library.UI.Abstract
{
    public interface ICategoryServices
    {
        IEnumerable<Category> GetAll(string ticket);
        bool Create(Category category, string ticket);
        bool Update(Category category, string ticket);
        Category GetById(int id, string ticket);
        Category GetByName(string categoryName, string ticket);
        bool Delete(int categoryId, string ticket);
        bool PutBookToCategory(int categoryId, int bookId, string ticket);
        bool RemoveBookFromCategory(int categoryId, int bookId, string ticket);
    }
}
