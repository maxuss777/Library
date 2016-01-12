namespace Library.UI.Abstract
{
    using System.Collections.Generic;
    using System.Web.Mvc;
    using Library.UI.Models.Books;

    public interface IBookService
    {
        List<Book> GetAll(string ticket);

        List<Book> GetByCategory(string category, string ticket);

        Book GetById(int id, string ticket);

        bool Create(Book book, string ticket);

        bool Delete(int bookId, string ticket);

        bool Update(Book book, string ticket);

        List<SelectListItem> BooksAsListItems(List<Book> books);
    }
}