using System.Collections.Generic;
using System.Web.Mvc;
using Library.UI.Abstract;
using Library.UI.Models;

namespace Library.UI.Controllers
{
    public class NavigationMenuController : Controller
    {
        private ICategoryServices _categoryServices;

        public NavigationMenuController(ICategoryServices categoryServ)
        {
            _categoryServices = categoryServ;
        }
        public PartialViewResult Menu()
        {
            var categoryList = (List<Category>)_categoryServices.GetAll();

            return categoryList.Count <= 0 ? PartialView() : PartialView(categoryList);
        }
    }
}