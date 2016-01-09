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

        public PartialViewResult GetAll(int page = 1)
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

            return !bookViewModel.Books.Any() ? PartialView("EmptyBooksList") : PartialView("AllBooksList",bookViewModel);
        }

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
	}
}