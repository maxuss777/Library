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

        public ActionResult Menu()
        {
            var categoryList = (List<Category>) _categoryServices.GetAll(Request.Cookies["_auth"].Value);
            return categoryList==null || !categoryList.Any() ? PartialView() : PartialView(categoryList);
        }

        [HttpGet]
        public ActionResult GetAll(int page = 1)
        {
            
            var categories = _categoryServices.GetAll(Request.Cookies["_auth"].Value);
            if (categories == null || !categories.Any())
                return View("EmptyCategoriesList");

            CategoryViewModel categoryViewModel = new CategoryViewModel
            {
                Categories = categories
                    .OrderByDescending(b => b.Name)
                    .Skip((page - 1)*PageSize)
                    .Take(PageSize),

                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = _categoryServices.GetAll(Request.Cookies["_auth"].Value).Count()
                }
            };

            return View("CategoriesList", categoryViewModel);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return PartialView("CreateCategory");
        }

        [HttpPost]
        public ActionResult Create(Category category)
        {
            if (!ModelState.IsValid || category == null)
            {
                return PartialView("CreateCategory");
            }
            if (!_categoryServices.Create(category, Request.Cookies["_auth"].Value))
            {
                TempData["fail_message"] = "The Category has been created unsuccessfully!";
            }
            else
            {
                TempData["succ_message"] = "The Category has been created successfully!";
            }
            return RedirectToAction("GetAll", "Category");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var book = _categoryServices.GetById(id, Request.Cookies["_auth"].Value);
            return book == null ? PartialView("ErroActionView") : PartialView("EditCategory", book);
        }

        [HttpPost]
        public ActionResult Edit(Category category)
        {
            if (!ModelState.IsValid || category == null)
                return PartialView("EditCategory", category);

            if (!_categoryServices.Update(category, Request.Cookies["_auth"].Value))
            {
                TempData["fail_message"] = "The Category has been edited unsuccessfully!";
            }
            else
            {
                TempData["succ_message"] = "The Category has been edited successfully!";
            }
            return RedirectToAction("GetAll", "Category");
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            if (id == 0 || !_categoryServices.Delete(id, Request.Cookies["_auth"].Value))
            {
                TempData["fail_message"] = "The Category has been deleted unsuccessfully!";
            }
            else
            {
                TempData["succ_message"] = "The Category has been deleted successfully!";
            }
            return RedirectToAction("GetAll", "Category");
        }

        [HttpPost]
        public ActionResult AddBook(string bookId, string categoryName)
        {
            int _bookId;
            if (!Int32.TryParse(bookId, out _bookId) || categoryName == null)
            {
                TempData["fail_message"] = "The book has added to this category unsuccessfully";
                return RedirectToAction("GetFiltered", "Books", new { category = categoryName });
            }

            var _categoryId = _categoryServices.GetByName(categoryName.Trim(), Request.Cookies["_auth"].Value).Id;

            if (_categoryId == 0 || _bookId == 0)
            {
                TempData["fail_message"] = "The book has added to this category unsuccessfully";
                return RedirectToAction("GetFiltered", "Books", new { category = categoryName });
            }

            if (!_categoryServices.PutBookToCategory(_categoryId, _bookId, Request.Cookies["_auth"].Value))
            {
                TempData["fail_message"] = "The book has added to this category unsuccessfully";
                return RedirectToAction("GetFiltered", "Books", new {category = categoryName});
            }
            TempData["succ_message"] = "The book has added to this category successfully";
            return RedirectToAction("GetFiltered", "Books", new { category = categoryName });
        }

        [HttpPost]
        public ActionResult RemoveBook(int bookId, string categoryName)
        {
            var categoryId = _categoryServices.GetByName(categoryName.Trim(), Request.Cookies["_auth"].Value).Id;
            if (categoryId == 0 || bookId == 0)
            {
                TempData["fail_message"] = "The book has removed from this category unsuccessfully";
                return RedirectToAction("GetFiltered", "Books", new { category = categoryName });
            }
            if (!_categoryServices.RemoveBookFromCategory(categoryId, bookId, Request.Cookies["_auth"].Value))
            {
                TempData["fail_message"] = "The book has removed from this category unsuccessfully";
                return RedirectToAction("GetFiltered", "Books", new {category = categoryName});
            }
            TempData["succ_message"] = "The book has removed from this category successfully";
            return RedirectToAction("GetFiltered", "Books", new { category = categoryName });
        }
    }
}