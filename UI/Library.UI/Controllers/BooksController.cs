using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Library.UI.Abstract;
using Library.UI.Models;

namespace Library.UI.Controllers
{
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
            BookViewModel bookViewModel = new BookViewModel
            {
                Books = _booksServices.GetAll()
                    .OrderByDescending(b => b.Id)
                    .Skip((page - 1) * PageSize)
                    .Take(PageSize),

                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = _booksServices.GetAll().Count()
                }
            };

            return !bookViewModel.Books.Any()
                ? PartialView("EmptyBooksList")
                : PartialView("AllBooksList", bookViewModel);
        }

        [HttpGet]
        public PartialViewResult GetFiltered(string category, int page = 1)
        {
            BookViewModel bookViewModel = new BookViewModel();
            IEnumerable<Book> books = _booksServices.GetByCategory(category);
            ViewBag.Books = _booksServices.BooksAsListItems(_booksServices.GetAll());
            ViewBag.CurrentCategory = category;

            try
            {
                if (books == null || !books.Any())
                    return PartialView("EmptyBooksList");

                bookViewModel.Books = books
                    .Skip((page - 1) * PageSize)
                    .Take(PageSize);
                bookViewModel.PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = category == null
                        ? _booksServices.GetAll().Count()
                        : _booksServices.GetByCategory(category).Count()
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
            if (!ModelState.IsValid || book == null)
                return PartialView("CreateBook");

            return PartialView(!_booksServices.Create(book) ? "ErroActionView" : "SuccessActionView");
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            return id == 0
                ? PartialView("ErroActionView")
                : PartialView(!_booksServices.Delete(id)
                    ? "ErroActionView"
                    : "SuccessActionView");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var book = _booksServices.GetById(id);
            return book == null ? PartialView("ErroActionView") : PartialView("EditBook", book);
        }
        [HttpPost]
        public ActionResult Edit(Book book)
        {
            if (!ModelState.IsValid || book == null)
                return PartialView("EditBook", book);

            var result = _booksServices.Update(book);
            return result == false ? PartialView("ErroActionView") : PartialView("SuccessActionView");
        }

        public IEnumerable<Book> SelectBook(string category)
        {
            return _booksServices.GetByCategory(category);
        }
    }
}