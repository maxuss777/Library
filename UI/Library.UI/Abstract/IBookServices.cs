
using System.Collections.Generic;
using System.Web.Mvc;
using Library.UI.Models;

namespace Library.UI.Abstract
{
    public interface IBookServices
    {
        IEnumerable<Book> GetAll();
        IEnumerable<Book> GetByCategory(string category);
        Book GetById(int id);
        bool Create(Book book);
        bool Delete(int bookId);
        bool Update(Book book);
        List<SelectListItem> BooksAsListItems(IEnumerable<Book> books);
    }
}
