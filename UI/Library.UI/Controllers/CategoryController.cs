using System.Web.Mvc;
using Library.UI.Abstract;

namespace Library.UI.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryServices _categoryServices;

        public CategoryController(ICategoryServices catServ)
        {
            _categoryServices = catServ;
        }
        
    }
}
