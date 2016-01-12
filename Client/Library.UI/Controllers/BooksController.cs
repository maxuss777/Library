using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Library.UI.Abstract;
using Library.UI.Filters;
using Library.UI.Models;

namespace Library.UI.Controllers
{
    [MyAuthorizeAttribute]
    public class BooksController : Controller
    {
        private const int PageSize = 5;
        private readonly IBookServices _booksServices;

        public BooksController(IBookServices bookServ)
        {
            _booksServices = bookServ;
        }

        [HttpGet]
        public ActionResult GetAll(int page = 1)
        {
            var books = _booksServices.GetAll(Request.Cookies["_auth"].Value);
            if (books == null || !books.Any())
                return PartialView("EmptyAllBooksList");

            BookViewModel bookViewModel = new BookViewModel
            {
                Books = books
                .OrderByDescending(b => b.Id)
                .Skip((page - 1)*PageSize)
                .Take(PageSize),

                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = _booksServices.GetAll(Request.Cookies["_auth"].Value).Count()
                }
            };

            return PartialView("FilledAllBooksList", bookViewModel);
        }

        [HttpGet]
        public ActionResult GetFiltered(string category, int page = 1)
        {
            BookViewModel bookViewModel = new BookViewModel();
            IEnumerable<Book> books = _booksServices.GetByCategory(category, Request.Cookies["_auth"].Value);
            ViewBag.Books = _booksServices.BooksAsListItems(_booksServices.GetAll(Request.Cookies["_auth"].Value));
            ViewBag.CurrentCategory = category;

            try
            {
                if (books == null || !books.Any())
                    return PartialView("EmptyFilteredBookList");

                bookViewModel.Books = books
                    .OrderByDescending(b=>b.Id)
                    .Skip((page - 1) * PageSize)
                    .Take(PageSize);
                bookViewModel.PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = category == null
                        ? _booksServices.GetAll(Request.Cookies["_auth"].Value).Count()
                        : _booksServices.GetByCategory(category, Request.Cookies["_auth"].Value).Count()
                };


                return !bookViewModel.Books.Any()
                    ? PartialView("EmptyFilteredBookList")
                    : PartialView("FilledFilteredBooksList", bookViewModel);
            }
            catch
            {
                return PartialView("EmptyFilteredBookList");
            }

        }

        [HttpGet]
        public ActionResult Create()
        {
            return PartialView("CreateBook");
        }

        [HttpPost]
        public ActionResult Create(Book book)
        {
            if (!ModelState.IsValid || book == null)
                return PartialView("CreateBook");

            if (_booksServices.Create(book, Request.Cookies["_auth"].Value))
            {
                TempData["succ_message"] = "The Book has been created successfully!";
            }
            else
            {
                TempData["fail_message"] = "The Book has been created unsuccessfully!";
            }
            return RedirectToAction("GetAll","Books");
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            if (id == 0 || !_booksServices.Delete(id, Request.Cookies["_auth"].Value))
            {
                TempData["fail_message"] = "The Book has been deleted unsuccessfully!";
            }
            else
            {
                TempData["succ_message"] = "The Book has been deleted successfully!";
            }
            return RedirectToAction("GetAll", "Books");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var book = _booksServices.GetById(id, Request.Cookies["_auth"].Value);
            if (book != null) return PartialView("EditBook", book);
            TempData["fail_message"] = "The Book has not been found!";
            return RedirectToAction("GetAll", "Books");
        }

        [HttpPost]
        public ActionResult Edit(Book book)
        {
            if (!ModelState.IsValid || book == null)
                return PartialView("EditBook", book);

            if (!_booksServices.Update(book, Request.Cookies["_auth"].Value))
            {
                TempData["fail_message"] = "The Book has been edited unsuccessfully!";
            }
            else
            {
                TempData["succ_message"] = "The Book has been edited successfully!";
            }
            return RedirectToAction("GetAll", "Books");
        }

        public IEnumerable<Book> FilterByCategory(string category)
        {
            return _booksServices.GetByCategory(category, Request.Cookies["_auth"].Value);
        }
    }
}