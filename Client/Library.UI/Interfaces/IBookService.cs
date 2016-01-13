using System.Collections.Generic;
using System.Web.Mvc;
using Library.UI.Models.Books;

namespace Library.UI.Interfaces
{
    public interface IBookService
    {
        List<Book> GetAll();

        List<Book> GetByCategory(string category);

        Book GetById(int id);

        bool Create(Book book);

        bool Delete(int bookId);

        bool Update(Book book);

        List<SelectListItem> BooksAsListItems(List<Book> books);
    }
}