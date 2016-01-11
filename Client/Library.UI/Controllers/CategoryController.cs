using System;
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
            var httpCookie = Request.Cookies["_auth"];
            if (httpCookie == null)
            {
                throw new UnauthorizedAccessException();
            }
            var categoryList = (List<Category>)_categoryServices.GetAll(httpCookie.Value);

            return categoryList.Count <= 0 ? PartialView() : PartialView(categoryList);
        }
        [HttpGet]
        public PartialViewResult GetAll(int page = 1)
        {
            var httpCookie = Request.Cookies["_auth"];
            if (httpCookie == null)
            {
                throw new UnauthorizedAccessException();
            }
            CategoryViewModel bookViewModel = new CategoryViewModel
            {
                Categories = _categoryServices.GetAll(httpCookie.Value)
                    .OrderByDescending(b => b.Name)
                    .Skip((page - 1) * PageSize)
                    .Take(PageSize),

                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = _categoryServices.GetAll(httpCookie.Value).Count()
                }
            };

            return !bookViewModel.Categories.Any()
                ? PartialView("EmptyCategoriesList")
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
            var httpCookie = Request.Cookies["_auth"];
            if (httpCookie == null)
            {
                throw new UnauthorizedAccessException();
            }
            if (!ModelState.IsValid || category == null)
                return PartialView("CreateCategory");

            return PartialView(!_categoryServices.Create(category, httpCookie.Value) ? "ErroActionView" : "SuccessActionView");
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var httpCookie = Request.Cookies["_auth"];
            if (httpCookie == null)
            {
                throw new UnauthorizedAccessException();
            }
            var book = _categoryServices.GetById(id, httpCookie.Value);
            return book == null ? PartialView("ErroActionView") : PartialView("EditCategory", book);
        }
        [HttpPost]
        public ActionResult Edit(Category category)
        {
            var httpCookie = Request.Cookies["_auth"];
            if (httpCookie == null)
            {
                throw new UnauthorizedAccessException();
            }
            if (!ModelState.IsValid || category == null)
                return PartialView("EditCategory", category);

            var result = _categoryServices.Update(category, httpCookie.Value);
            return result == false ? PartialView("ErroActionView") : PartialView("SuccessActionView");
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            var httpCookie = Request.Cookies["_auth"];
            if (httpCookie == null)
            {
                throw new UnauthorizedAccessException();
            }
            return id == 0
                ? PartialView("ErroActionView")
                : PartialView(!_categoryServices.Delete(id, httpCookie.Value)
                    ? "ErroActionView"
                    : "SuccessActionView");
        }
        [HttpPost]
        public ActionResult AddBook(string bookId, string categoryName)
        {
            var httpCookie = Request.Cookies["_auth"];
            if (httpCookie == null)
            {
                throw new UnauthorizedAccessException();
            }
            int _bookId;
            if (!Int32.TryParse(bookId, out _bookId) || categoryName==null)
                return View("ErroActionView");

            var _categoryId = _categoryServices.GetByName(categoryName.Trim(), httpCookie.Value).Id;

            if(_categoryId==0||_bookId == 0)
                return View("ErroActionView");
            try
            {
                return View(!_categoryServices.PutBookToCategory(_categoryId, _bookId, httpCookie.Value) 
                    ? "ErroActionView" 
                    : "SuccessActionView");
            }
            catch
            {
                return View("ErroActionView");
            }
        }
        [HttpPost]
        public ActionResult RemoveBook(int bookId, string categoryName)
        {
            var httpCookie = Request.Cookies["_auth"];
            if (httpCookie == null)
            {
                throw new UnauthorizedAccessException();
            }
            var categoryId = _categoryServices.GetByName(categoryName.Trim(), httpCookie.Value).Id;

            if (categoryId == 0 || bookId == 0)
                return View("ErroActionView");
            try
            {
                return View(!_categoryServices.RemoveBookFromCategory(categoryId, bookId, httpCookie.Value)
                    ? "ErroActionView"
                    : "SuccessActionView");
            }
            catch
            {
                return View("ErroActionView");
            }
        }
    }
}