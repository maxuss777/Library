namespace Library.UI.Abstract
{
    using System.Collections.Generic;
    using Library.UI.Models;
    using Library.UI.Models.Categories;

    public interface ICategoryService
    {
        List<Category> GetAll(string ticket);
        bool Create(Category category, string ticket);
        bool Update(Category category, string ticket);
        Category GetById(int id, string ticket);
        Category GetByName(string categoryName, string ticket);
        bool Delete(int categoryId, string ticket);
        bool PutBookToCategory(int categoryId, int bookId, string ticket);
        bool RemoveBookFromCategory(int categoryId, int bookId, string ticket);
    }
}