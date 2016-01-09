using System.Collections.Generic;
using System.Web.Mvc;
using Library.UI.Abstract;
using Library.UI.Models;

namespace Library.UI.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryServices _categoryServices;

        public CategoryController(ICategoryServices catServ)
        {
            _categoryServices = catServ;
        }
        public PartialViewResult Menu(string category = null)
        {
            var categoryList = (List<Category>)_categoryServices.GetAll();

            return categoryList.Count <= 0 ? PartialView() : PartialView(categoryList);
        }
    }
}
