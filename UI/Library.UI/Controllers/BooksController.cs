using System;
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
                    .Skip((page - 1)*PageSize)
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
            try
            {
                BookViewModel bookViewModel = new BookViewModel();
               
                    bookViewModel.Books = _booksServices.GetByCategory(category)
                        .Skip((page - 1)*PageSize)
                        .Take(PageSize);

                    bookViewModel.PagingInfo = new PagingInfo
                    {
                        CurrentPage = page,
                        ItemsPerPage = PageSize
                    };
                bookViewModel.PagingInfo.TotalItems = category == null
                    ? _booksServices.GetAll().Count()
                    : bookViewModel.Books.Count();

                return !bookViewModel.Books.Any()
                    ? PartialView("EmptyBooksList")
                    : PartialView("AllBooksList", bookViewModel);
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
    }
}