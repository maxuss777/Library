
using System.Collections.Generic;
using Library.UI.Models;

namespace Library.UI.Abstract
{
    public interface IBookServices
    {
        IEnumerable<Book> GetAll();
    }
}
