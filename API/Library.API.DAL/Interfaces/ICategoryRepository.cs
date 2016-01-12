namespace Library.API.DataAccess.Interfaces
{
    using System.Collections.Generic;
    using Library.API.Model;

    public interface ICategoryRepository
    {
        Category Create(Category category);

        Category GetById(int categoryId);

        Category GetByName(string category);

        List<Category> GetAll();

        Category Update(Category category);

        bool Delete(int categoryId);

        bool PutBookToCategory(int categoryId, int bookId);

        bool RemoveBookFromCategory(int categoryId, int bookId);
    }
}