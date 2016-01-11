
using System.Collections.Generic;
using System.Web.Mvc;
using Library.UI.Models;

namespace Library.UI.Abstract
{
    public interface IBookServices
    {
        IEnumerable<Book> GetAll(string ticket);
        IEnumerable<Book> GetByCategory(string category, string ticket);
        Book GetById(int id, string ticket);
        bool Create(Book book, string ticket);
        bool Delete(int bookId, string ticket);
        bool Update(Book book, string ticket);
        List<SelectListItem> BooksAsListItems(IEnumerable<Book> books);
    }
}
