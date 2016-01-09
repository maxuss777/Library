
using System.Collections.Generic;
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
    }
}
