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
        
    }
}