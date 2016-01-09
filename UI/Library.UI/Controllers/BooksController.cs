using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Library.UI.Abstract;
using Library.UI.Models;

namespace Library.UI.Controllers
{
    public class BooksController : Controller
    {
        public const int PageSize = 5;
        
        private IBookServices _booksServices;

        public BooksController(IBookServices bookServ)
        {
            _booksServices = bookServ;
        }

        public PartialViewResult AllBooksList(int page = 1)
        {
            BookViewModel bookViewModel = new BookViewModel
            {
                Books = _booksServices.GetAll()
                    .Skip((page - 1)*PageSize)
                    .Take(PageSize),

                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = _booksServices.GetAll().Count()
                }
            };

            return !bookViewModel.Books.Any() ? PartialView("EmptyBooksList") : PartialView(bookViewModel);
        }

        public PartialViewResult FilterBookByCatName(int categoryName , int page = 1)
        {
            BookViewModel bookViewModel = new BookViewModel
            {
                Books = _booksServices.GetAll()
                    .Skip((page - 1) * PageSize)
                    .Take(PageSize),

                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = _booksServices.GetAll().Count()
                }
            };

            return !bookViewModel.Books.Any() ? PartialView("EmptyBooksList") : PartialView(bookViewModel);
        }


	}
}