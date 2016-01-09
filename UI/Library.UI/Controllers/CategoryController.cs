using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Library.UI.Abstract;
using Library.UI.Models;

namespace Library.UI.Controllers
{
    public class CategoryController : Controller
    {
        private const int PageSize = 5;
        private readonly ICategoryServices _categoryServices;

        public CategoryController(ICategoryServices catServ)
        {
            _categoryServices = catServ;
        }
        public PartialViewResult Menu()
        {
            var categoryList = (List<Category>)_categoryServices.GetAll();

            return categoryList.Count <= 0 ? PartialView() : PartialView(categoryList);
        }

        [HttpGet]
        public PartialViewResult GetAll(int page = 1)
        {
            CategoryViewModel bookViewModel = new CategoryViewModel
            {
                Categories = _categoryServices.GetAll()
                    .OrderByDescending(b => b.Name)
                    .Skip((page - 1) * PageSize)
                    .Take(PageSize),

                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = _categoryServices.GetAll().Count()
                }
            };

            return !bookViewModel.Categories.Any()
                ? PartialView()
                : PartialView("CategoriesList", bookViewModel);
        }
        [HttpGet]
        public PartialViewResult Create()
        {
            return PartialView("CreateCategory");
        }

        [HttpPost]
        public ActionResult Create(Category category)
        {
            if (!ModelState.IsValid || category == null)
                return PartialView("CreateCategory");

            return PartialView(!_categoryServices.Create(category) ? "ErroActionView" : "SuccessActionView");
        }
    }
}
