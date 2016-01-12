namespace Library.API.DataAccess.Interfaces
{
    using System.Collections.Generic;
    using Library.API.Model;

    public interface IBookRepository
    {
        Book Create(Book book);

        Book GetById(int id);

        int IfCategoryExists(string categoryName);

        List<Book> GetAll();

        List<Book> GetBooksByCategoryId(int categoryId);

        Book Update(Book book);

        bool Delete(int bookId);
    }
}