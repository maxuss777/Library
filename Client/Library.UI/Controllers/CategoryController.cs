using Library.UI.Interfaces;

namespace Library.UI.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using Library.UI.Models;
    using Library.UI.Models.Categories;

    public class CategoryController : Controller
    {
        private const int PageSize = 5;
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public ActionResult Menu()
        {
            List<Category> categories = _categoryService.GetAll();

            return categories.Count == 0 ? PartialView() : PartialView(categories);
        }

        [HttpGet]
        public ActionResult GetAll(int page = 1)
        {
            List<Category> categories = _categoryService.GetAll();

            if (categories.Count == 0)
            {
                return View("EmptyCategoriesList");
            }

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
                    TotalItems = _categoryService.GetAll().Count()
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

            bool isCreated = _categoryService.Create(category);
            if (isCreated)
            {
                TempData["succ_message"] = "The Category has been created successfully!";
            }
            else
            {
                TempData["fail_message"] = "The Category has been created unsuccessfully!";
            }

            return RedirectToAction("GetAll", "Category");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            Category category = _categoryService.GetById(id);
            if (category != null)
            {
                return PartialView("EditCategory", category);
            }

            TempData["fail_message"] = "The Category has not been found!";

            return RedirectToAction("GetAll", "Category");
        }

        [HttpPost]
        public ActionResult Edit(Category category)
        {
            if (!ModelState.IsValid || category == null)
            {
                return PartialView("EditCategory", category);
            }

            bool isUpdated = _categoryService.Update(category);
            if (isUpdated)
            {
                TempData["succ_message"] = "The Category has been edited successfully!";
            }
            else
            {
                TempData["fail_message"] = "The Category has been edited unsuccessfully!";
            }

            return RedirectToAction("GetAll", "Category");
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            bool isDeleted = _categoryService.Delete(id);
            if (id == 0 || !isDeleted)
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
            int numericBookId;

            if (!Int32.TryParse(bookId, out numericBookId) || categoryName == null)
            {
                TempData["fail_message"] = "The book has added to this category unsuccessfully";

                return RedirectToAction("GetFiltered", "Books", new {category = categoryName});
            }

            Category category = _categoryService.GetByName(categoryName.Trim());
            if (category == null || category.Id == 0 || numericBookId == 0)
            {
                TempData["fail_message"] = "The book has added to this category unsuccessfully";

                return RedirectToAction("GetFiltered", "Books", new {category = categoryName});
            }

            bool categoryIsAssigned = _categoryService.PutBookToCategory(category.Id, numericBookId);
            if (!categoryIsAssigned)
            {
                TempData["fail_message"] = "The book has added to this category unsuccessfully";

                return RedirectToAction("GetFiltered", "Books", new {category = categoryName});
            }

            TempData["succ_message"] = "The book has added to this category successfully";

            return RedirectToAction("GetFiltered", "Books", new {category = categoryName});
        }

        [HttpPost]
        public ActionResult RemoveBook(int bookId, string categoryName)
        {
            Category category = _categoryService.GetByName(categoryName.Trim());
            if (category == null || category.Id == 0 || bookId == 0)
            {
                TempData["fail_message"] = "The book has removed from this category unsuccessfully";

                return RedirectToAction("GetFiltered", "Books", new {category = categoryName});
            }

            bool isRemoved = _categoryService.RemoveBookFromCategory(category.Id, bookId);
            if (!isRemoved)
            {
                TempData["fail_message"] = "The book has removed from this category unsuccessfully";

                return RedirectToAction("GetFiltered", "Books", new {category = categoryName});
            }

            TempData["succ_message"] = "The book has removed from this category successfully";

            return RedirectToAction("GetFiltered", "Books", new {category = categoryName});
        }
    }
}