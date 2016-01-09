using System.Collections.Generic;
using Library.UI.Models;

namespace Library.UI.Abstract
{
    public interface ICategoryServices
    {
        IEnumerable<Category> GetAll();
        bool Create(Category category);
    }
}
