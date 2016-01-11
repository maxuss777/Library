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

        private IBookServices _booksServices;

        public BooksController(IBookServices bookServ)
        {
            _booksServices = bookServ;
        }

        [HttpGet]
        public PartialViewResult GetAll(int page = 1)
        {
            var httpCookie = Request.Cookies["_auth"];
            if (httpCookie == null)
            {
                throw new UnauthorizedAccessException();
            }

            BookViewModel bookViewModel = new BookViewModel
            {
                Books = _booksServices.GetAll(httpCookie.Value)
                    .OrderByDescending(b => b.Id)
                    .Skip((page - 1) * PageSize)
                    .Take(PageSize),

                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = _booksServices.GetAll(httpCookie.Value).Count()
                }
            };

            return !bookViewModel.Books.Any()
                ? PartialView("EmptyBooksList")
                : PartialView("AllBooksList", bookViewModel);
        }

        [HttpGet]
        public PartialViewResult GetFiltered(string category, int page = 1)
        {
            var httpCookie = Request.Cookies["_auth"];
            if (httpCookie == null)
            {
                throw new UnauthorizedAccessException();
            }
            BookViewModel bookViewModel = new BookViewModel();
            IEnumerable<Book> books = _booksServices.GetByCategory(category,httpCookie.Value);
            ViewBag.Books = _booksServices.BooksAsListItems(_booksServices.GetAll(httpCookie.Value));
            ViewBag.CurrentCategory = category;

            try
            {
                if (books == null || !books.Any())
                    return PartialView("EmptyBooksList");

                bookViewModel.Books = books
                    .OrderByDescending(b=>b.Id)
                    .Skip((page - 1) * PageSize)
                    .Take(PageSize);
                bookViewModel.PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = category == null
                        ? _booksServices.GetAll(httpCookie.Value).Count()
                        : _booksServices.GetByCategory(category, httpCookie.Value).Count()
                };


                return !bookViewModel.Books.Any()
                    ? PartialView("EmptyBooksList")
                    : PartialView("FilteredBookslist", bookViewModel);
            }
            catch
            {
                return PartialView("EmptyBooksList");
            }

        }

        [HttpGet]
        public PartialViewResult Create()
        {
            return PartialView("CreateBook");
        }

        [HttpPost]
        public ActionResult Create(Book book)
        {
            var httpCookie = Request.Cookies["_auth"];
            if (httpCookie == null)
            {
                throw new UnauthorizedAccessException();
            }
            if (!ModelState.IsValid || book == null)
                return PartialView("CreateBook");

            return PartialView(!_booksServices.Create(book, httpCookie.Value) ? "ErroActionView" : "SuccessActionView");
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
                : PartialView(!_booksServices.Delete(id, httpCookie.Value)
                    ? "ErroActionView"
                    : "SuccessActionView");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var httpCookie = Request.Cookies["_auth"];
            if (httpCookie == null)
            {
                throw new UnauthorizedAccessException();
            }
            var book = _booksServices.GetById(id, httpCookie.Value);
            return book == null ? PartialView("ErroActionView") : PartialView("EditBook", book);
        }
        [HttpPost]
        public ActionResult Edit(Book book)
        {
            var httpCookie = Request.Cookies["_auth"];
            if (httpCookie == null)
            {
                throw new UnauthorizedAccessException();
            }
            if (!ModelState.IsValid || book == null)
                return PartialView("EditBook", book);

            var result = _booksServices.Update(book, httpCookie.Value);
            return result == false ? PartialView("ErroActionView") : PartialView("SuccessActionView");
        }

        public IEnumerable<Book> SelectBook(string category)
        {
            var httpCookie = Request.Cookies["_auth"];
            if (httpCookie == null)
            {
                throw new UnauthorizedAccessException();
            }
            return _booksServices.GetByCategory(category, httpCookie.Value);
        }
    }
}